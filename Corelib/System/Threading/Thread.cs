using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using TinyDotNet;

namespace System.Threading;

public delegate void ParameterizedThreadStart(object obj);

public delegate void ThreadStart();

public sealed class Thread
{

    // State associated with starting new thread
    private sealed class StartHelper
    {
        internal int _maxStackSize;
        internal Delegate _start;
        internal object? _startArg;
        // internal CultureInfo? _culture;
        // internal CultureInfo? _uiCulture;
        internal ExecutionContext? _executionContext;

        internal StartHelper(Delegate start)
        {
            _start = start;
        }

        internal static readonly ContextCallback s_threadStartContextCallback = new ContextCallback(Callback);

        private static void Callback(object? state)
        {
            Debug.Assert(state != null);
            ((StartHelper)state).RunWorker();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] // avoid long-lived stack frame in many threads
        internal void Run()
        {
            if (_executionContext != null && !_executionContext.IsDefault)
            {
                ExecutionContext.RunInternal(_executionContext, s_threadStartContextCallback, this);
            }
            else
            {
                RunWorker();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] // avoid long-lived stack frame in many threads
        private void RunWorker()
        {
            InitializeCulture();

            Delegate start = _start;
            _start = null!;

            if (start is ThreadStart threadStart)
            {
                threadStart();
            }
            else
            {
                ParameterizedThreadStart parameterizedThreadStart = (ParameterizedThreadStart)start;

                object? startArg = _startArg;
                _startArg = null;

                parameterizedThreadStart(startArg);
            }
        }

        private void InitializeCulture()
        {
            // if (_culture != null)
            // {
            //     CultureInfo.CurrentCulture = _culture;
            //     _culture = null;
            // }
            //
            // if (_uiCulture != null)
            // {
            //     CultureInfo.CurrentUICulture = _uiCulture;
            //     _uiCulture = null;
            // }
        }
    }

    
    // itay: @StaticSaga choose a value for OptimalMaxSpinWaitsPerSpinIteration 
    // StaticSaga: 1000
    public static int OptimalMaxSpinWaitsPerSpinIteration => 1000;
    
    public static Thread CurrentThread => NativeHost.ThreadGetCurrentThread();

    public bool IsAlive => (NativeHost.ThreadGetNativeThreadState(_threadHandle) & 0xFFF) < 5;

    private string _name = null;
    public string Name
    {
        get => _name;
        set
        {
            if (_name != null)
                throw new InvalidOperationException("the Name property has already been set.");
            
            _name = value;
            NativeHost.ThreadSetNativeThreadName(_threadHandle, _name);
        }
    }

    public ThreadState ThreadState
    {
        get
        {
            var state = NativeHost.ThreadGetNativeThreadState(_threadHandle);
            var threadState = state switch
            {
                0 => ThreadState.Unstarted,
                1 => ThreadState.Running,
                2 => ThreadState.Running,
                // TODO: we need a way to figure out how to set the thread 
                //       as suspended if we entered the wait from the suspended
                //       state, we could always just track it in here
                3 => ThreadState.WaitSleepJoin,
                4 => ThreadState.Suspended,
                5 => ThreadState.Stopped,
                _ => throw new ArgumentOutOfRangeException(nameof(state))
            };
            
            // we can track the suspend request state
            threadState |= (state & 0x1000) == 0 ? 0 : ThreadState.SuspendRequested;
            
            return threadState;
        }
    }
    
    public bool IsThreadPoolThread { get; internal set; }
    
    // TODO: do we want to do something with this?
    public bool IsBackground { get; internal set; }

    private StartHelper _startHelper = null;
    internal ExecutionContext _executionContext;
    private ulong _threadHandle;

    public Thread(ParameterizedThreadStart start)
    {
        if (start == null)
            throw new ArgumentNullException(nameof(start));

        _startHelper = new StartHelper(start);
        _threadHandle = NativeHost.ThreadCreateNativeThread(StartCallback, this);
    }

    public Thread(ThreadStart start)
    {
        if (start == null)
            throw new ArgumentNullException(nameof(start));

        _startHelper = new StartHelper(start);
        _threadHandle = NativeHost.ThreadCreateNativeThread(StartCallback, this);
    }

    // Called from the runtime
    private void StartCallback()
    {
        StartHelper? startHelper = _startHelper;
        Debug.Assert(startHelper != null);
        _startHelper = null;

        startHelper.Run();
    }

    ~Thread()
    {
        NativeHost.ThreadReleaseNativeThread(_threadHandle);
    }

    /// <summary>Causes the operating system to change the state of the current instance to <see cref="ThreadState.Running"/>, and optionally supplies an object containing data to be used by the method the thread executes.</summary>
    /// <param name="parameter">An object that contains data to be used by the method the thread executes.</param>
    /// <exception cref="ThreadStateException">The thread has already been started.</exception>
    /// <exception cref="OutOfMemoryException">There is not enough memory available to start this thread.</exception>
    /// <exception cref="InvalidOperationException">This thread was created using a <see cref="ThreadStart"/> delegate instead of a <see cref="ParameterizedThreadStart"/> delegate.</exception>
    public void Start(object? parameter) => Start(parameter, captureContext: true);

    /// <summary>Causes the operating system to change the state of the current instance to <see cref="ThreadState.Running"/>, and optionally supplies an object containing data to be used by the method the thread executes.</summary>
    /// <param name="parameter">An object that contains data to be used by the method the thread executes.</param>
    /// <exception cref="ThreadStateException">The thread has already been started.</exception>
    /// <exception cref="OutOfMemoryException">There is not enough memory available to start this thread.</exception>
    /// <exception cref="InvalidOperationException">This thread was created using a <see cref="ThreadStart"/> delegate instead of a <see cref="ParameterizedThreadStart"/> delegate.</exception>
    /// <remarks>
    /// Unlike <see cref="Start"/>, which captures the current <see cref="ExecutionContext"/> and uses that context to invoke the thread's delegate,
    /// <see cref="UnsafeStart"/> explicitly avoids capturing the current context and flowing it to the invocation.
    /// </remarks>
    public void UnsafeStart(object? parameter) => Start(parameter, captureContext: false);

    private void Start(object? parameter, bool captureContext)
    {
        StartHelper? startHelper = _startHelper;

        // In the case of a null startHelper (second call to start on same thread)
        // StartCore method will take care of the error reporting.
        if (startHelper != null)
        {
            if (startHelper._start is ThreadStart)
            {
                // We expect the thread to be setup with a ParameterizedThreadStart if this Start is called.
                throw new InvalidOperationException("The thread was created with a ThreadStart delegate that does not accept a parameter.");
            }

            startHelper._startArg = parameter;
            startHelper._executionContext = captureContext ? ExecutionContext.Capture() : null;
        }

        NativeHost.ThreadStartNativeThread(_threadHandle, parameter);
    }

    /// <summary>Causes the operating system to change the state of the current instance to <see cref="ThreadState.Running"/>.</summary>
    /// <exception cref="ThreadStateException">The thread has already been started.</exception>
    /// <exception cref="OutOfMemoryException">There is not enough memory available to start this thread.</exception>
    public void Start() => Start(captureContext: true);

    /// <summary>Causes the operating system to change the state of the current instance to <see cref="ThreadState.Running"/>.</summary>
    /// <exception cref="ThreadStateException">The thread has already been started.</exception>
    /// <exception cref="OutOfMemoryException">There is not enough memory available to start this thread.</exception>
    /// <remarks>
    /// Unlike <see cref="Start"/>, which captures the current <see cref="ExecutionContext"/> and uses that context to invoke the thread's delegate,
    /// <see cref="UnsafeStart"/> explicitly avoids capturing the current context and flowing it to the invocation.
    /// </remarks>
    public void UnsafeStart() => Start(captureContext: false);

    private void Start(bool captureContext)
    {
        StartHelper? startHelper = _startHelper;

        // In the case of a null startHelper (second call to start on same thread)
        // StartCore method will take care of the error reporting.
        if (startHelper != null)
        {
            startHelper._startArg = null;
            startHelper._executionContext = captureContext ? ExecutionContext.Capture() : null;
        }

        NativeHost.ThreadStartNativeThread(_threadHandle, null);
    }

    public static void Sleep(int millisecondsTimeout)
    {
        if (millisecondsTimeout < -1)
            throw new ArgumentOutOfRangeException(nameof(millisecondsTimeout), "Number must be either non-negative and less than or equal to Int32.MaxValue or -1.");
        
        Sleep(new TimeSpan(millisecondsTimeout * TimeSpan.TicksPerMillisecond));
    }

    public static void Sleep(TimeSpan timeout)
    {
        var waitable = NativeHost.WaitableAfter(timeout.Ticks);
        try
        {
            NativeHost.WaitableWait(waitable, true);
        }
        finally
        {
            NativeHost.ReleaseWaitable(waitable);
        }
    }

    public static void SpinWait(int iterations)
    {
        for (var i = 0; i < iterations; i++)
        {
            X86Base.Pause();
        }
    }

    public static int GetCurrentProcessorId() => NativeHost.ThreadGetCurrentProcessorId();

    public static bool Yield() => NativeHost.ThreadYield();

}
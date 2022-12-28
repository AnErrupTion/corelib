using TinyDotNet;

namespace System.Threading;

public static class Monitor
{

    public static void Enter(object obj)
    {
        var lockTaken = false;
        Enter(obj, ref lockTaken);   
    }

    public static void Enter(object obj, ref bool lockTaken)
    {
        if (lockTaken)
            throw new ArgumentException("Argument must be initialized to false", nameof(lockTaken));

        if (obj == null)
            throw new ArgumentNullException(nameof(obj));

        switch (NativeHost.MonitorEnterInternal(obj, ref lockTaken))
        {
            case 0: return;
            case 3: throw new OutOfMemoryException();
            default: throw new SystemException();
        }
    }

    public static void Exit(object obj)
    {
        if (obj == null)
            throw new ArgumentNullException(nameof(obj));

        switch (NativeHost.MonitorExitInternal(obj))
        {
            case 0: return;
            case 3: throw new OutOfMemoryException();
            case 6: throw new SynchronizationLockException();
            default: throw new SystemException();
        }
    }

    public static bool IsEntered(object obj) => NativeHost.MonitorIsEntered(obj);

    public static void Pulse(object obj)
    {
        if (obj == null)
            throw new ArgumentNullException(nameof(obj));

        switch (NativeHost.MonitorPulseInternal(obj))
        {
            case 0: return;
            case 3: throw new OutOfMemoryException();
            case 6: throw new SynchronizationLockException();
            default: throw new SystemException();
        }
    }

    public static void PulseAll(object obj)
    {
        if (obj == null)
            throw new ArgumentNullException(nameof(obj));

        switch (NativeHost.MonitorPulseAllInternal(obj))
        {
            case 0: return;
            case 3: throw new OutOfMemoryException();
            case 6: throw new SynchronizationLockException();
            default: throw new SystemException();
        }
    }

    public static bool Wait(object obj, int millisecondsTimeout)
    {
        if (millisecondsTimeout != -1)
        {
            throw new NotImplementedException();
        }

        return Wait(obj);
    }

    public static bool Wait(object obj)
    {
        if (obj == null)
            throw new ArgumentNullException(nameof(obj));

        switch (NativeHost.MonitorWaitInternal(obj))
        {
            case 0: return true;
            case 3: throw new OutOfMemoryException();
            case 6: throw new SynchronizationLockException();
            default: throw new SystemException();
        }
    }

}
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace TinyDotNet;

/// <summary>
/// A collections of function that are required to be provided by
/// the native host and not by TinyDotNet itself
/// </summary>
internal static class NativeHost
{
    
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern bool WaitableSend(ulong waitable, bool block);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern int WaitableWait(ulong waitable, bool block);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern void WaitableClose(ulong waitable);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern void WaitableOpen(ulong waitable);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern ulong WaitableSelect(ref Span<ulong> waitables, int sendCount, int waitCount, bool block);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern ulong CreateWaitable(int count);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern ulong WaitableAfter(long timeoutTicks);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern ulong PutWaitable(ulong waitable);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern void ReleaseWaitable(ulong waitable);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern long StopwatchGetTscFrequency();

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern long StopwatchGetTimestamp();

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern void GcCollect(int generation, GCCollectionMode mode, bool blocking);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern void GcKeepAlive(object obj);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern void X86Pause();

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern Assembly AssemblyLoadInternal(byte[] rawAssembly, bool reflection); 
    
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern Assembly AssemblyLoadInternal(string rawAssembly, bool reflection);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern bool MonitorIsEntered(object obj);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern int MonitorEnterInternal(object obj, ref bool lockTaken);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern int MonitorExitInternal(object obj);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern int MonitorPulseInternal(object obj);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern int MonitorPulseAllInternal(object obj);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern int MonitorWaitInternal(object obj);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern int InterlockedAdd(ref int location1, int value);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern uint InterlockedAdd(ref uint location1, uint value);
    
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern long InterlockedAdd(ref long location1, long value);
    
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern ulong InterlockedAdd(ref ulong location1, ulong value);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern int InterlockedAnd(ref int location1, int value);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern uint InterlockedAnd(ref uint location1, uint value);
    
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern long InterlockedAnd(ref long location1, long value);
    
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern ulong InterlockedAnd(ref ulong location1, ulong value);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern int InterlockedCompareExchange(ref int location1, int value, int comparand);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern uint InterlockedCompareExchange(ref uint location1, uint value, uint comparand);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern long InterlockedCompareExchange(ref long location1, long value, long comparand);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern ulong InterlockedCompareExchange(ref ulong location1, ulong value, ulong comparand);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern object InterlockedCompareExchange(ref object location1, object value, object comparand);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern int InterlockedDecrement(ref int location);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern long InterlockedDecrement(ref long location);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern uint InterlockedDecrement(ref uint location);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern ulong InterlockedDecrement(ref ulong location);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern int InterlockedExchange(ref int location1, int value);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern uint InterlockedExchange(ref uint location1, uint value);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern long InterlockedExchange(ref long location1, long value);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern ulong InterlockedExchange(ref ulong location1, ulong value);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern object InterlockedExchange(ref object location1, object value);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern int InterlockedIncrement(ref int location);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern long InterlockedIncrement(ref long location);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern uint InterlockedIncrement(ref uint location);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern ulong InterlockedIncrement(ref ulong location);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern void InterlockedMemoryBarrier();

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern int InterlockedOr(ref int location1, int value);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern uint InterlockedOr(ref uint location1, uint value);
    
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern long InterlockedOr(ref long location1, long value);
    
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern ulong InterlockedOr(ref ulong location1, ulong value);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern long InterlockedRead(ref long location);
    
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern ulong InterlockedRead(ref ulong location);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Native)]
    internal static extern int EnvironmentGetProcessorCount();

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Native)]
    internal static extern object ActivatorCreateInstance(Type type, object[] args);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern void BufferMemMove(ref byte destination, ref byte source, nuint elementCount);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern void BufferZeroMemory(ref byte b, nuint byteLength);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern Type TypeGetTypeFromHandle(RuntimeTypeHandle handle);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Native)]
    internal static extern void DebugProviderWriteInternal(string? message);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern double MathAbs(double value);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern float MathAbs(float value);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern double MathAcos(double d);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern double MathAcosh(double d);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern double MathAsin(double d);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern double MathAsinh(double d);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern double MathAtan(double d);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern double MathAtanh(double d);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern double MathAtan2(double y, double x);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern double MathCbrt(double d);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern double MathCos(double d);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern double MathCosh(double value);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern double MathExp(double d);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Native)]
    internal static extern double MathFloor(double d);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern double MathFusedMultiplyAdd(double x, double y, double z);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int MathILogB(double x);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Native)]
    internal static extern double MathLog(double d);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern double MathLog2(double x);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern double MathLog10(double d);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern double MathPow(double x, double y);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern double MathSin(double a);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern double MathSinh(double value);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Native)]
    internal static extern double MathSqrt(double d);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern double MathTan(double a);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern double MathTanh(double value);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern double MathFMod(double x, double y);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Native)]
    internal static extern unsafe double MathModF(double x, double* intptr); 

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Native)]
    internal static extern unsafe void MathSinCos(double x, double* sin, double* cos);

    [MethodImpl(MethodImplOptions.InternalCall)] 
    internal static extern float MathFAcos(float x);

    [MethodImpl(MethodImplOptions.InternalCall)] 
    internal static extern float MathFAcosh(float x);

    [MethodImpl(MethodImplOptions.InternalCall)] 
    internal static extern float MathFAsin(float x);

    [MethodImpl(MethodImplOptions.InternalCall)] 
    internal static extern float MathFAsinh(float x);

    [MethodImpl(MethodImplOptions.InternalCall)] 
    internal static extern float MathFAtan(float x);

    [MethodImpl(MethodImplOptions.InternalCall)] 
    internal static extern float MathFAtanh(float x);

    [MethodImpl(MethodImplOptions.InternalCall)] 
    internal static extern float MathFAtan2(float y, float x);

    [MethodImpl(MethodImplOptions.InternalCall)] 
    internal static extern float MathFCbrt(float x);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Native)]
    internal static extern float MathFCeiling(float x);

    [MethodImpl(MethodImplOptions.InternalCall)] 
    internal static extern float MathFCos(float x);

    [MethodImpl(MethodImplOptions.InternalCall)] 
    internal static extern float MathFCosh(float x);

    [MethodImpl(MethodImplOptions.InternalCall)] 
    internal static extern float MathFExp(float x);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Native)]
    internal static extern float MathFFloor(float x);

    [MethodImpl(MethodImplOptions.InternalCall)] 
    internal static extern float MathFFusedMultiplyAdd(float x, float y, float z);

    [MethodImpl(MethodImplOptions.InternalCall)] 
    internal static extern int MathFILogB(float x);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Native)]
    internal static extern float MathFLog(float x);

    [MethodImpl(MethodImplOptions.InternalCall)] 
    internal static extern float MathFLog2(float x);

    [MethodImpl(MethodImplOptions.InternalCall)] 
    internal static extern float MathFLog10(float x);

    [MethodImpl(MethodImplOptions.InternalCall)] 
    internal static extern float MathFPow(float x, float y);

    [MethodImpl(MethodImplOptions.InternalCall)] 
    internal static extern float MathFSin(float x);

    [MethodImpl(MethodImplOptions.InternalCall)] 
    internal static extern float MathFSinh(float x);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Native)]
    internal static extern float MathFSqrt(float x);

    [MethodImpl(MethodImplOptions.InternalCall)] 
    internal static extern float MathFTan(float x);

    [MethodImpl(MethodImplOptions.InternalCall)] 
    internal static extern float MathFTanh(float x);

    [MethodImpl(MethodImplOptions.InternalCall)] 
    internal static extern float MathFFMod(float x, float y);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Native)] 
    internal static extern unsafe float MathFModF(float x, float* intptr);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Native)] 
    internal static extern unsafe void MathFSinCos(float x, float* sin, float* cos);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern int ThreadGetCurrentProcessorId();

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern bool ThreadYield();

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern int ThreadGetNativeThreadState(ulong thread);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern ulong ThreadCreateNativeThread(Delegate start, Thread managedThread);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern void ThreadStartNativeThread(ulong thread, object parameter);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern void ThreadReleaseNativeThread(ulong thread);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern void ThreadSetNativeThreadName(ulong thread, string name);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    internal static extern Thread ThreadGetCurrentThread();

}
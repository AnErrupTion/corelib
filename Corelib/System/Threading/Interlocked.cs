using System.Runtime.CompilerServices;
using TinyDotNet;

namespace System.Threading;

public static class Interlocked
{

    #region Add

    public static int Add(ref int location1, int value) => NativeHost.InterlockedAdd(ref location1, value);

    public static uint Add(ref uint location1, uint value) => NativeHost.InterlockedAdd(ref location1, value);

    public static long Add(ref long location1, long value) => NativeHost.InterlockedAdd(ref location1, value);

    public static ulong Add(ref ulong location1, ulong value) => NativeHost.InterlockedAdd(ref location1, value);

    #endregion
    
    #region And

    public static int And(ref int location1, int value) => NativeHost.InterlockedAnd(ref location1, value);

    public static uint And(ref uint location1, uint value) => NativeHost.InterlockedAnd(ref location1, value);

    public static long And(ref long location1, long value) => NativeHost.InterlockedAnd(ref location1, value);

    public static ulong And(ref ulong location1, ulong value) => NativeHost.InterlockedAnd(ref location1, value);

    #endregion

    #region Compare Exchange

    public static int CompareExchange(ref int location1, int value, int comparand) => NativeHost.InterlockedCompareExchange(ref location1, value, comparand);

    public static uint CompareExchange(ref uint location1, uint value, uint comparand) => NativeHost.InterlockedCompareExchange(ref location1, value, comparand);

    public static long CompareExchange(ref long location1, long value, long comparand) => NativeHost.InterlockedCompareExchange(ref location1, value, comparand);

    public static ulong CompareExchange(ref ulong location1, ulong value, ulong comparand) => NativeHost.InterlockedCompareExchange(ref location1, value, comparand);

    public static object CompareExchange(ref object location1, object value, object comparand) => NativeHost.InterlockedCompareExchange(ref location1, value, comparand);

    public static T CompareExchange<T>(ref T location1, T value, T comparand)
        where T : class
    {
        var obj = CompareExchange(ref Unsafe.As<T, object>(ref location1), value, comparand);
        return Unsafe.As<T>(obj);
    }

    #endregion

    #region Decrement

    public static int Decrement(ref int location) => NativeHost.InterlockedDecrement(ref location);

    public static long Decrement(ref long location) => NativeHost.InterlockedDecrement(ref location);

    public static uint Decrement(ref uint location) => NativeHost.InterlockedDecrement(ref location);

    public static ulong Decrement(ref ulong location) => NativeHost.InterlockedDecrement(ref location);

    #endregion
    
    #region Exchange

    public static int Exchange(ref int location1, int value) => NativeHost.InterlockedExchange(ref location1, value);

    public static uint Exchange(ref uint location1, uint value) => NativeHost.InterlockedExchange(ref location1, value);

    public static long Exchange(ref long location1, long value) => NativeHost.InterlockedExchange(ref location1, value);

    public static ulong Exchange(ref ulong location1, ulong value) => NativeHost.InterlockedExchange(ref location1, value);

    public static object Exchange(ref object location1, object value) => NativeHost.InterlockedExchange(ref location1, value);

    public static T Exchange<T>(ref T location1, T value)
        where T : class
    {
        var obj = Exchange(ref Unsafe.As<T, object>(ref location1), value);
        return Unsafe.As<T>(obj);
    }

    #endregion

    #region Increment

    public static int Increment(ref int location) => NativeHost.InterlockedIncrement(ref location);

    public static long Increment(ref long location) => NativeHost.InterlockedIncrement(ref location);

    public static uint Increment(ref uint location) => NativeHost.InterlockedIncrement(ref location);

    public static ulong Increment(ref ulong location) => NativeHost.InterlockedIncrement(ref location);

    #endregion

    public static void MemoryBarrier() => NativeHost.InterlockedMemoryBarrier();
    
    #region Or

    public static int Or(ref int location1, int value) => NativeHost.InterlockedOr(ref location1, value);

    public static uint Or(ref uint location1, uint value) => NativeHost.InterlockedOr(ref location1, value);

    public static long Or(ref long location1, long value) => NativeHost.InterlockedOr(ref location1, value);

    public static ulong Or(ref ulong location1, ulong value) => NativeHost.InterlockedOr(ref location1, value);

    #endregion

    #region Read

    public static long Read(ref long location) => NativeHost.InterlockedRead(ref location);

    public static ulong Read(ref ulong location) => NativeHost.InterlockedRead(ref location);

    #endregion
    
}
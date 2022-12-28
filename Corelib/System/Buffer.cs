using System.Runtime.CompilerServices;
using TinyDotNet;

namespace System;

internal static class Buffer
{

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void Memmove<T>(ref T destination, ref T source, nuint elementCount)
        where T : unmanaged
    {
        NativeHost.BufferMemMove(
            ref Unsafe.As<T, byte>(ref destination),
            ref Unsafe.As<T, byte>(ref source),
            elementCount * (nuint)Unsafe.SizeOf<T>());
    }
    
    internal static unsafe void ZeroMemory(byte* dest, nuint len)
    {
        SpanHelpers.ClearWithoutReferences(ref *dest, len);
    }

}
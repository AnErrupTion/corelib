// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using TinyDotNet;

namespace System
{
    internal static partial class SpanHelpers
    {
        internal static void ClearWithoutReferences(ref byte b, nuint byteLength)
        {
            if (byteLength == 0)
                return;

            NativeHost.BufferZeroMemory(ref b, byteLength);
        }
    }
}

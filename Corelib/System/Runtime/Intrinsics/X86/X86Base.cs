using TinyDotNet;

namespace System.Runtime.Intrinsics.X86;

public static class X86Base
{

    public static bool IsSupported => true;

    public static void Pause() => NativeHost.X86Pause();

}
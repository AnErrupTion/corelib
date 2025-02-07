using TinyDotNet;

namespace System;

public static class GC
{

    // we only have two generations in our collector
    //  0 - young objects, collects objects that were allocated since last collection
    //  1 - everything, collects all the objects 
    // so we don't really use generations as GCs usually use but still, it
    // is good enough
    public static int MaxGeneration => 1;
    
    public static void Collect()
    {
        Collect(MaxGeneration, GCCollectionMode.Default, true);
    }

    public static void Collect(int generation, GCCollectionMode mode)
    {
        Collect(generation, mode);
    }

    public static void Collect(int generation, GCCollectionMode mode, bool blocking) =>
        NativeHost.GcCollect(generation, mode, blocking);
    
    // TODO: in theory there is also compacting but we don't have such a feature in our garbage 
    //       collection, so for now I will not implement the method, worst case I will throw an error

    // we are using an internal call here on purpose, it is going to make 
    // the jit assume that something will change and it will keep the call
    // and the object itself until this point 
    public static void KeepAlive(object obj) => NativeHost.GcKeepAlive(obj);

    public static T[] AllocateUninitializedArray<T>(int len)
    {
        // TODO: we could in theory support that but for now its easier to not support it 
        return new T[len];
    }
    
    public static void ReRegisterForFinalize(object obj)
    {
        NativeHost.GcReRegisterForFinalize(obj);
    }
    
    public static void SuppressFinalize(object obj)
    {
        NativeHost.GcSuppressFinalize(obj);
    }

}
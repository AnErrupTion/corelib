using System.Runtime.CompilerServices;
using TinyDotNet;

namespace System;

public static class Activator
{

    public static object CreateInstance(Type type, params object[] args)
    {
        if (type == null)
            throw new ArgumentNullException();

        if (type.ContainsGenericParameters)
            throw new ArgumentException();

        return NativeHost.ActivatorCreateInstance(type, args);
    }
    
    public static T CreateInstance<T>() 
        where T : class
    {
        return Unsafe.As<T>(CreateInstance(typeof(T)));
    }

}
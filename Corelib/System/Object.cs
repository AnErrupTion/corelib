using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using TinyDotNet;

namespace System
{
    [Serializable]
    public class Object
    {
        [NonVersionable]
        public Object()
        {
        }

        [NonVersionable]
#pragma warning disable CA1821 // Remove empty Finalizers
        ~Object()
        {
        }
#pragma warning restore CA1821

        public Type GetType() => NativeHost.ObjectGetType(this);
        
        public virtual bool Equals(object? obj)
        {
            return this == obj;
        }
 
        public static bool Equals(object? objA, object? objB)
        {
            if (objA == objB)
            {
                return true;
            }
            if (objA == null || objB == null)
            {
                return false;
            }
            return objA.Equals(objB);
        }

        public virtual int GetHashCode()
        {
            return RuntimeHelpers.GetHashCode(this);
        }

        protected object MemberwiseClone() => NativeHost.ObjectMemberwiseClone(this);
        
        [NonVersionable]
        public static bool ReferenceEquals(object? objA, object? objB)
        {
            return objA == objB;
        }

        public virtual string? ToString()
        {
            return GetType().ToString();
        }

    }
}
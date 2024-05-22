using System;
using System.Reflection;

namespace Reflection
    {
        public static partial class Extensions
        {
            private const System.Reflection.BindingFlags PnpiFlags = System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance;

            public static ConstructorInfo GetDefaultConstructor(this Type type)
            {
                return type.GetTypeInfo().GetConstructor(PnpiFlags, null, EmptyArray<Type>.Value, null);
            }

            public static object InvokeDefaultConstructor(this Type type)
            {
                return type.GetDefaultConstructor()?.Invoke(EmptyArray<object>.Value);
            }
        }
    }

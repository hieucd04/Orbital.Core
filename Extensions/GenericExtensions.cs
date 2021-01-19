using System;
using System.Linq;

namespace Orbital.Core
{
    public static class GenericExtensions
    {
        public static bool In<T>(this T item, params T[] items) { return items.Contains(item); }

        public static bool IsSubclassOfRawGeneric(this Type type, Type genericType)
        {
            if (!genericType.IsGenericType)
                throw new ArgumentException($"Type [{genericType}] is not generic type.");

            while (type != null && type != typeof(object))
            {
                if (type.IsGenericType)
                {
                    type = type.GetGenericTypeDefinition();
                }

                if (type == genericType)
                {
                    return true;
                }

                type = type.BaseType;
            }

            return false;
        }
    }
}
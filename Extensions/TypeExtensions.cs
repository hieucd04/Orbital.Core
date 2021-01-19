using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Orbital.Core
{
    public static class TypeExtensions
    {
        public static bool IsCompilerGenerated(this Type type) => type.GetCustomAttribute(typeof(CompilerGeneratedAttribute), true) != null;

        public static bool IsNullableEnum(this Type type) { return Nullable.GetUnderlyingType(type)?.IsEnum == true; }

        public static bool IsNumeric(this Type type)
        {
            var numericTypes = new HashSet<Type>
            {
                typeof(byte),
                typeof(sbyte),
                typeof(ushort),
                typeof(uint),
                typeof(ulong),
                typeof(short),
                typeof(int),
                typeof(long),
                typeof(decimal),
                typeof(double),
                typeof(float)
            };
            return numericTypes.Contains(type);
        }

        public static bool IsSqlString(this Type type)
        {
            return type.In(typeof(string), typeof(char[]), typeof(DateTime), typeof(DateTimeOffset), typeof(TimeSpan), typeof(Guid));
        }

        public static string ToSqlDbType(this Type type)
        {
            if (type == typeof(Guid)) return SqlDbType.UniqueIdentifier.ToString();
            if (type == typeof(bool)) return SqlDbType.Bit.ToString();
            if (type == typeof(byte)) return SqlDbType.TinyInt.ToString();
            if (type == typeof(short)) return SqlDbType.SmallInt.ToString();
            if (type == typeof(int)) return SqlDbType.Int.ToString();
            if (type == typeof(long)) return SqlDbType.BigInt.ToString();
            if (type == typeof(float)) return SqlDbType.Real.ToString();
            if (type == typeof(double)) return SqlDbType.Float.ToString();
            if (type == typeof(decimal)) return SqlDbType.Decimal + " (18, 0)";
            if (type == typeof(string)) return SqlDbType.NVarChar + " (MAX)";
            if (type == typeof(char[])) return SqlDbType.NVarChar + " (MAX)";
            if (type == typeof(TimeSpan)) return SqlDbType.Time + " (7)";
            if (type == typeof(DateTime)) return SqlDbType.DateTime2 + " (7)";
            if (type == typeof(DateTimeOffset)) return SqlDbType.DateTimeOffset + " (7)";
            if (type == typeof(byte[])) return SqlDbType.VarBinary + " (MAX)";
            if (type == typeof(object)) return SqlDbType.Variant.ToString();
            throw new ArgumentOutOfRangeException();
        }

        public static bool IsAssignableTo<T>(this Type type) { return typeof(T).IsAssignableFrom(type); }
    }
}
using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Trainova.Infrastructure.DataAccess.Common
{
    public static class EnumMappingExtensions
    {
        public static void MapEnumAsStringIfNotFlags(this PropertyBuilder propertyBuilder, Type enumType)
        {
            if (enumType.IsDefined(typeof(FlagsAttribute), inherit: false))
                return;

            var converterType = typeof(ValueConverter<,>).MakeGenericType(enumType, typeof(string));

            // create delegates
            var toString = typeof(EnumMappingExtensions)
                .GetMethod(nameof(ToStringGeneric), BindingFlags.NonPublic | BindingFlags.Static)!
                .MakeGenericMethod(enumType);

            var fromString = typeof(EnumMappingExtensions)
                .GetMethod(nameof(FromStringGeneric), BindingFlags.NonPublic | BindingFlags.Static)!
                .MakeGenericMethod(enumType);

            var converter = Activator.CreateInstance(converterType, toString.CreateDelegate(typeof(Func<,>).MakeGenericType(enumType, typeof(string))), fromString.CreateDelegate(typeof(Func<,>).MakeGenericType(typeof(string), enumType)), null);

            // propertyBuilder.HasConversion(converter) -> we need to use reflection to call the generic HasConversion if necessary
            var pbType = propertyBuilder.GetType();
            var method = pbType.GetMethod("HasConversion", new[] { converterType });
            if (method != null)
            {
                method.Invoke(propertyBuilder, new[] { converter });
            }
            else
            {
                // fallback: use non-generic HasConversion via dynamic
                try
                {
                    dynamic pb = propertyBuilder;
                    pb.HasConversion(converter);
                }
                catch
                {
                    // ignore
                }
            }
        }

        private static string ToStringGeneric<TEnum>(TEnum value) where TEnum : struct
            => value.ToString()!;

        private static TEnum FromStringGeneric<TEnum>(string value) where TEnum : struct
            => (TEnum)Enum.Parse(typeof(TEnum), value);
    }
}

using Dapper;
using System.Data;
using Trainova.Common.SmartEnums;
using Trainova.Domain.AccessPolicies;
using Trainova.Domain.Common;

namespace Trainova.Infrastructure.DataAccess.DbSettingsObjects
{
    public static class DapperTypeHandlerConfiguration
    {
        public static void Register()
        {
            var enumTypes = typeof(DomainMarker).Assembly
                .GetTypes()
                .Where(t => t.IsEnum)
                .ToList();

            foreach (var type in enumTypes)
            {
                var handlerType = typeof(EnumStringHandler<>).MakeGenericType(type);
                var handler = (SqlMapper.ITypeHandler)Activator.CreateInstance(handlerType)!;
                SqlMapper.AddTypeHandler(type, handler);

                var nullableType = typeof(Nullable<>).MakeGenericType(type);
                var nullableHandlerType = typeof(NullableEnumStringHandler<>).MakeGenericType(type);
                var nullableHandler = (SqlMapper.ITypeHandler)Activator.CreateInstance(nullableHandlerType)!;
                SqlMapper.AddTypeHandler(nullableType, nullableHandler);
            }

        }

        private class EnumStringHandler<T> : SqlMapper.TypeHandler<T> where T : struct, Enum
        {
            public override void SetValue(IDbDataParameter parameter, T value)
            {
                parameter.Value = value.ToString();
                parameter.DbType = DbType.String;
            }

            public override T Parse(object value)
            {
                if (value is string s && Enum.TryParse<T>(s, true, out var parsed))
                    return parsed;

                throw new DataException($"Cannot convert {value} to enum {typeof(T)}");
            }
        }



        private class NullableEnumStringHandler<T> : SqlMapper.TypeHandler<T?> where T : struct, Enum
        {
            public override void SetValue(IDbDataParameter parameter, T? value)
            {
                parameter.Value = value?.ToString() ?? (object)DBNull.Value;
                parameter.DbType = DbType.String;
            }

            public override T? Parse(object value)
            {
                if (value == null || value == DBNull.Value) return null;
                var nonNullHandler = new EnumStringHandler<T>();
                return nonNullHandler.Parse(value);
            }
        }




    }
}

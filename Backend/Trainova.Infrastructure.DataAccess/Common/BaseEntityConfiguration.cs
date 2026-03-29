using System;
using System.Linq;
using System.Reflection;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Trainova.Infrastructure.DataAccess.Common
{
  
    public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T>
        where T : class
    {
        protected const int NameLength = 200;
        protected const int DescriptionLength = 1200;
        protected const int DefaultStringLength = 400;
        protected const int EmailLength = 320;
        protected const int UrlPathLength = 1024;

        public void Configure(EntityTypeBuilder<T> builder)
        {
            ConfigureId(builder);
            ConfigureStringLengths(builder);
            ConfigureEnums(builder);
            ConfigureEntity(builder);
        }

        // Override in derived classes to add entity-specific configuration
        protected virtual void ConfigureEntity(EntityTypeBuilder<T> builder) { }

        protected void ConfigureId(EntityTypeBuilder<T> builder)
        {
            var idProp = typeof(T).GetProperty("Id", BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            if (idProp != null)
            {
                // ensure key exists for simple Id properties
                builder.HasKey("Id");

                // If it's a Guid, mark as ValueGeneratedOnAdd per project convention
                if (idProp.PropertyType == typeof(Guid))
                {
                    builder.Property(typeof(Guid), "Id").ValueGeneratedOnAdd();
                    return;
                }

                // If Id is a composed/complex owned type (value object), map its inner properties to clean column names
                if (idProp.PropertyType.IsClass && idProp.PropertyType != typeof(string))
                {
                    try
                    {
                        var ownedType = idProp.PropertyType;

                        // Use non-generic OwnsOne overload to avoid compile-time type coupling
                        builder.OwnsOne(typeof(object), "Id", id =>
                        {
                            var innerProps = ownedType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                            foreach (var inner in innerProps)
                            {
                                // Map each inner property to a clean column name equal to the inner property name
                                id.Property(inner.PropertyType, inner.Name).HasColumnName(inner.Name);
                            }
                        });
                    }
                    catch
                    {
                        // Swallow any errors - fall back to default EF behavior if we can't map the owned type generically
                    }
                }
            }
        }

        protected void ConfigureStringLengths(EntityTypeBuilder<T> builder)
        {
            var stringProperties = typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
                .Where(p => p.PropertyType == typeof(string));

            foreach (var prop in stringProperties)
            {
                var name = prop.Name;
                var lower = name.ToLowerInvariant();
                var pb = builder.Property(typeof(string), name);

                if (lower.Contains("name") && !lower.Contains("username"))
                {
                    pb.HasMaxLength(NameLength);
                }
                else if (lower.Contains("description") || lower.Contains("note") || lower.Contains("notes"))
                {
                    pb.HasMaxLength(DescriptionLength);
                }
                else if (lower.Contains("email"))
                {
                    pb.HasMaxLength(EmailLength);
                }
                else if (lower.EndsWith("path") || lower.EndsWith("url") || lower.EndsWith("uri"))
                {
                    pb.HasMaxLength(UrlPathLength);
                }
                else
                {
                    pb.HasMaxLength(DefaultStringLength);
                }

                // Do not force IsRequired here; per-entity configs can choose to require fields explicitly.
            }
        }

        protected void ConfigureEnums(EntityTypeBuilder<T> builder)
        {
            var enumProps = typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
                .Where(p => (p.PropertyType.IsEnum) || (Nullable.GetUnderlyingType(p.PropertyType)?.IsEnum ?? false));

            foreach (var p in enumProps)
            {
                var enumType = Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType;

                // If flagged enum, keep numeric representation
                if (enumType.IsDefined(typeof(FlagsAttribute), inherit: false))
                    continue;

                try
                {
                    // Build expressions: enum -> string and string -> enum
                    var paramModel = Expression.Parameter(enumType, "e");
                    var toStringCall = Expression.Call(paramModel, enumType.GetMethod("ToString", Type.EmptyTypes)!);
                    var toStringLambda = Expression.Lambda(toStringCall, paramModel);

                    var paramString = Expression.Parameter(typeof(string), "s");
                    var parseMethod = typeof(Enum).GetMethod("Parse", new[] { typeof(Type), typeof(string) })!;
                    var parseCall = Expression.Call(parseMethod, Expression.Constant(enumType), paramString);
                    var convertFrom = Expression.Convert(parseCall, enumType);
                    var fromStringLambda = Expression.Lambda(convertFrom, paramString);

                    var converterType = typeof(ValueConverter<,>).MakeGenericType(enumType, typeof(string));
                    var converter = Activator.CreateInstance(converterType, toStringLambda, fromStringLambda, null);

                    var propertyBuilder = builder.Property(enumType, p.Name);

                    // Find HasConversion(ValueConverter) method and invoke it
                    var pbType = propertyBuilder.GetType();
                    var hasConv = pbType.GetMethod("HasConversion", new[] { typeof(ValueConverter) });
                    if (hasConv != null)
                    {
                        hasConv.Invoke(propertyBuilder, new[] { converter });
                    }
                    else
                    {
                        // fallback: try dynamic
                        dynamic pb = propertyBuilder;
                        pb.HasConversion((dynamic)converter);
                    }
                }
                catch
                {
                    // If conversion setup fails, skip - EF will use numeric mapping as fallback.
                }
            }
        }
    }
}

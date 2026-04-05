using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection;

namespace Trainova.Infrastructure.DataAccess.Configuration.Common
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
            ConfigureEntity(builder);
        }

        // Override in derived classes to add entity-specific configuration
        protected virtual void ConfigureEntity(EntityTypeBuilder<T> builder,
           bool valueGeneratedOnAdd = true)
        {
            ConfigureId(builder,valueGeneratedOnAdd);
            ConfigureStringLengths(builder);
        }

        protected virtual void ConfigureId(
           EntityTypeBuilder<T> builder,
           bool valueGeneratedOnAdd = true
        )
        {
            var idProp = typeof(T).GetProperty("Id", BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            if (idProp == null) return;

            // Simple Ids (Guid, int, etc.)
            if (idProp.PropertyType == typeof(Guid) || idProp.PropertyType == typeof(int) || idProp.PropertyType == typeof(long))
            {
                builder.HasKey("Id");

                var propBuilder = builder.Property(idProp.PropertyType, "Id");
                if (valueGeneratedOnAdd)
                    propBuilder.ValueGeneratedOnAdd();
                else
                    propBuilder.ValueGeneratedNever();

                return;
            }

            // **مهم:** لو Id مركب أو VO (class غير string) – مش هنعمل mapping تلقائي هنا
            // المسؤولية هتكون على entity-specific config
            if (idProp.PropertyType.IsClass && idProp.PropertyType != typeof(string))
            {
                // EF Core Owned types for Ids should be configured manually in entity config
                return;
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

    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.MedicalHistories;

namespace Trainova.Infrastructure.DataAccess.Configuration.MedicalHistories
{
    public class MedicalHistoryConfiguration : IEntityTypeConfiguration<MedicalHistory>
    {
        public void Configure(EntityTypeBuilder<MedicalHistory> builder)
        {
            builder.ToTable("MedicalHistories");
            builder.HasKey(m => m.Id);

            builder.Property(m => m.CreatedAt).IsRequired();
        }
    }
}

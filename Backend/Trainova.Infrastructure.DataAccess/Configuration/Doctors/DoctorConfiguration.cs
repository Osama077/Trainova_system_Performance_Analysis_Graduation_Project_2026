using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.Doctors;

namespace Trainova.Infrastructure.DataAccess.Configuration.Doctors
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctors");

            builder.HasKey(d => d.UserId);

            builder.HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId);
        }
    }
}

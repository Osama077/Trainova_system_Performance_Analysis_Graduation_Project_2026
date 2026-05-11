using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.Profiles;
using Trainova.Infrastructure.DataAccess.Configuration.Common;

namespace Trainova.Infrastructure.DataAccess.Configuration.Profiles
{
    public class TeamStaffConfiguration
        : BaseEntityConfiguration<TeamStaff>
    {
        protected override void ConfigureEntity(
            EntityTypeBuilder<TeamStaff> builder,
            bool valueGeneratedOnAdd = false)
        {
            base.ConfigureEntity(builder, valueGeneratedOnAdd);

            builder.ToTable("TeamStaffs");



            builder.HasOne(ts => ts.User)
                .WithOne(u => u.TeamStaff)
                .HasForeignKey<TeamStaff>(ts => ts.Id);

            builder
                .Property(ts => ts.Role)
                .HasConversion<string>()
                .HasMaxLength(40);

        }
    }

}

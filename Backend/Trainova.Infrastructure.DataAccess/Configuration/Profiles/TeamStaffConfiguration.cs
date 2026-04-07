using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.Profiles.TeamsStaff;
using Trainova.Domain.UserAuth.Users;
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

            builder
                .HasOne(ts => ts.Team)
                .WithMany(t => t.TeamStaffs)
                .HasForeignKey(ts => ts.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(ts => ts.User)
                .WithMany()
                .HasForeignKey(ts => ts.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(ts => ts.Role)
                .HasConversion<string>()
                .HasMaxLength(40);

            builder.HasIndex(ts => ts.TeamId);
        }
    }

}

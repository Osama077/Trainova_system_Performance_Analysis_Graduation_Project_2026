using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Trainova.Domain.MatchsManagement.Videos;
using Trainova.Infrastructure.DataAccess.Configuration.Common;

namespace Trainova.Infrastructure.DataAccess.Configuration.MatchsManagement
{
    public class MatchVideoConfiguration
        : BaseEntityConfiguration<MatchVideo>
    {
        protected override void ConfigureEntity(
            EntityTypeBuilder<MatchVideo> builder,
            bool valueGeneratedOnAdd = true)
        {
            base.ConfigureEntity(builder, valueGeneratedOnAdd);

            builder.ToTable("MatchVideos");


            builder.HasIndex(mv => mv.RelatedMatchId);
        }
    }

}

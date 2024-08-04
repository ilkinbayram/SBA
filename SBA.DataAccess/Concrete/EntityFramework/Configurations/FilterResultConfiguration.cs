using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBA.DataAccess.Concrete.EntityFramework.Configurations.BaseConfig;
using SBA.DataAccess.Extensions;

namespace SBA.DataAccess.Concrete.EntityFramework.Configurations
{
    public class FilterResultConfiguration : BaseEntityTypeConfig<FilterResult>
    {
        public void Configure(EntityTypeBuilder<FilterResult> builder)
        {
            builder.ToTable("FilterResults");

            builder.Property(x => x.HomeCornerCount).HasDefaultValue(-1);
            builder.Property(x => x.AwayCornerCount).HasDefaultValue(-1);
            builder.Property(x => x.HomePossesion).HasDefaultValue(-1);
            builder.Property(x => x.AwayPossesion).HasDefaultValue(-1);
            builder.Property(x => x.HomeShotCount).HasDefaultValue(-1);
            builder.Property(x => x.AwayShotCount).HasDefaultValue(-1);
            builder.Property(x => x.HomeShotOnTargetCount).HasDefaultValue(-1);
            builder.Property(x => x.AwayShotOnTargetCount).HasDefaultValue(-1);
            builder.Property(x => x.HomeFtGoalCount).HasDefaultValue(-1);
            builder.Property(x => x.HomeHtGoalCount).HasDefaultValue(-1);
            builder.Property(x => x.HomeShGoalCount).HasDefaultValue(-1);
            builder.Property(x => x.AwayFtGoalCount).HasDefaultValue(-1);
            builder.Property(x => x.AwayHtGoalCount).HasDefaultValue(-1);
            builder.Property(x => x.AwayShGoalCount).HasDefaultValue(-1);

            builder.Property(x => x.IsCornerFound).HasDefaultValue(false);
            builder.Property(x => x.IsPossesionFound).HasDefaultValue(false);
            builder.Property(x => x.IsShotFound).HasDefaultValue(false);
            builder.Property(x => x.IsShotOnTargetFound).HasDefaultValue(false);

            builder.HasIndex(x => x.SerialUniqueID).IsClustered(false).HasDatabaseName("IX_SerialUniqueID");
        }
    }
}

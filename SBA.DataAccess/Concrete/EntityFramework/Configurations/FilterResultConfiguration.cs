using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBA.DataAccess.Extensions;

namespace SBA.DataAccess.Concrete.EntityFramework.Configurations
{
    public class FilterResultConfiguration : IEntityTypeConfiguration<FilterResult>
    {
        public void Configure(EntityTypeBuilder<FilterResult> builder)
        {
            builder.ToTable("FilterResults");
            builder.HasKey(k => k.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.HomeCornerCount).HasDefaultValue(-1);
            builder.Property(x => x.AwayCornerCount).HasDefaultValue(-1);
            builder.Property(x => x.HomePossesion).HasDefaultValue(-1);
            builder.Property(x => x.AwayPossesion).HasDefaultValue(-1);
            builder.Property(x => x.HomeShotCount).HasDefaultValue(-1);
            builder.Property(x => x.AwayShotCount).HasDefaultValue(-1);
            builder.Property(x => x.HomeShotOnTargetCount).HasDefaultValue(-1);
            builder.Property(x => x.AwayShotOnTargetCount).HasDefaultValue(-1);

            builder.Property(x => x.IsCornerFound).HasDefaultValue(false);
            builder.Property(x => x.IsPossesionFound).HasDefaultValue(false);
            builder.Property(x => x.IsShotFound).HasDefaultValue(false);
            builder.Property(x => x.IsShotOnTargetFound).HasDefaultValue(false);

            builder.Property(x => x.IsActive).HasDefaultValue(true);

            builder.HasIndex(x => x.SerialUniqueID).IsClustered(false).HasDatabaseName("IX_SerialUniqueID");
        }
    }
}

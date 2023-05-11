using Core.Entities.Concrete.ExternalDbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SBA.ExternalDataAccess.Concrete.EntityFramework.Configurations
{
    public class StatisticInfoHolderConfig : IEntityTypeConfiguration<StatisticInfoHolder>
    {
        public void Configure(EntityTypeBuilder<StatisticInfoHolder> builder)
        {
            builder.ToTable("StatisticInfoHolders");
            builder.HasKey(k => k.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.AwayPercent).HasPrecision(7, 2);
            builder.Property(x => x.HomePercent).HasPrecision(7, 2);
        }
    }
}

using Core.Entities.Concrete.ExternalDbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SBA.ExternalDataAccess.Concrete.EntityFramework.Configurations
{
    public class StatisticInfoHolderConfig : IEntityTypeConfiguration<StatisticInfoHolder>
    {
        public void Configure(EntityTypeBuilder<StatisticInfoHolder> builder)
        {
            TimeZoneInfo azerbaycanZone = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");
            DateTime azerbaycanTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, azerbaycanZone);

            builder.ToTable("StatisticInfoHolders");
            builder.HasKey(k => k.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.CreatedDate).HasDefaultValue(azerbaycanTime.Date);
            builder.Property(x => x.AwayPercent).HasPrecision(7, 2);
            builder.Property(x => x.HomePercent).HasPrecision(7, 2);
        }
    }
}

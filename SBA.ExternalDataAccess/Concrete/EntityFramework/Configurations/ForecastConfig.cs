using Core.Entities.Concrete.ExternalDbEntities;
using Core.Resources.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SBA.ExternalDataAccess.Concrete.EntityFramework.Configurations
{
    public class ForecastConfig : IEntityTypeConfiguration<Forecast>
    {
        public void Configure(EntityTypeBuilder<Forecast> builder)
        {
            TimeZoneInfo azerbaycanZone = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");
            DateTime azerbaycanTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, azerbaycanZone);

            builder.ToTable("Forecasts");
            builder.HasKey(k => k.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.IsSuccess).HasDefaultValue(false);
            builder.Property(x => x.IsChecked).HasDefaultValue(false);

            builder.Property(x => x.Serial).HasDefaultValue(0);

            builder.Property(x => x.CreatedBy).HasMaxLength(100).HasDefaultValue("System.Admin");
            builder.Property(x => x.ModifiedBy).HasMaxLength(100).HasDefaultValue("System.Admin");

            builder.Property(x => x.CreatedDateTime).HasDefaultValue(azerbaycanTime.Date);
            builder.Property(x => x.ModifiedDateTime).HasDefaultValue(azerbaycanTime.Date);

            builder.HasOne(p => p.MatchIdentifier).WithMany(x => x.Forecasts).HasForeignKey(x => x.MatchIdentifierId);
        }
    }
}

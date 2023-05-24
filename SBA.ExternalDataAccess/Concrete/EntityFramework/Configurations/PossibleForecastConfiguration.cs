using Core.Entities.Concrete.ExternalDbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SBA.ExternalDataAccess.Concrete.EntityFramework.Configurations
{
    public class PossibleForecastConfiguration : IEntityTypeConfiguration<PossibleForecast>
    {
        public void Configure(EntityTypeBuilder<PossibleForecast> builder)
        {
            TimeZoneInfo azerbaycanZone = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");
            DateTime azerbaycanTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, azerbaycanZone);

            builder.ToTable("PossibleForecasts");
            builder.HasKey(k => k.Id);

            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.CreatedDate).HasDefaultValue(azerbaycanTime.Date);
            builder.Property(x => x.UpdateVersion).HasDefaultValue(0);
        }
    }
}

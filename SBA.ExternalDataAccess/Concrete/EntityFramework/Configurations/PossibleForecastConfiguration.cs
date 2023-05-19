using Core.Entities.Concrete.ExternalDbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SBA.ExternalDataAccess.Concrete.EntityFramework.Configurations
{
    public class PossibleForecastConfiguration : IEntityTypeConfiguration<PossibleForecast>
    {
        public void Configure(EntityTypeBuilder<PossibleForecast> builder)
        {
            builder.ToTable("PossibleForecasts");
            builder.HasKey(k => k.Id);

            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.CreatedDate).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.UpdateVersion).HasDefaultValue(0);
        }
    }
}

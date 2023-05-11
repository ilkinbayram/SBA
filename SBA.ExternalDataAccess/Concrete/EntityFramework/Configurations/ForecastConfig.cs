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
            builder.ToTable("Forecasts");
            builder.HasKey(k => k.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.IsSuccess).HasDefaultValue(false);
            builder.Property(x => x.IsChecked).HasDefaultValue(false);

            builder.Property(x => x.CreatedBy).HasMaxLength(100).HasDefaultValue("System.Admin");
            builder.Property(x => x.ModifiedBy).HasMaxLength(100).HasDefaultValue("System.Admin");

            builder.Property(x => x.CreatedDateTime).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.ModifiedDateTime).HasDefaultValue(DateTime.Now);

            builder.HasOne(p => p.MatchIdentifier).WithMany(x => x.Forecasts).HasForeignKey(x => x.MatchIdentifierId);
        }
    }
}

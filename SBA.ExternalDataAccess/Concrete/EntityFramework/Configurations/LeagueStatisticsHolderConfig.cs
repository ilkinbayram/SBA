using Core.Entities.Concrete.ExternalDbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SBA.ExternalDataAccess.Concrete.EntityFramework.Configurations
{
    public class LeagueStatisticsHolderConfig : IEntityTypeConfiguration<LeagueStatisticsHolder>
    {
        public void Configure(EntityTypeBuilder<LeagueStatisticsHolder> builder)
        {
            TimeZoneInfo azerbaycanZone = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");
            DateTime azerbaycanTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, azerbaycanZone);

            builder.ToTable("LeagueStatisticsHolders");
            builder.HasKey(k => k.Id);

            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.CountFound);

            builder.Property(x => x.CreatedBy).HasMaxLength(100).HasDefaultValue("System.Admin");
            builder.Property(x => x.ModifiedBy).HasMaxLength(100).HasDefaultValue("System.Admin");

            builder.Property(x => x.CreatedDateTime).HasDefaultValue(azerbaycanTime.Date);
            builder.Property(x => x.ModifiedDateTime).HasDefaultValue(azerbaycanTime.Date);

            builder.Property(x => x.FT_GoalsAverage).HasPrecision(7,2);
            builder.Property(x => x.HT_GoalsAverage).HasPrecision(7,2);
            builder.Property(x => x.SH_GoalsAverage).HasPrecision(7,2);
            builder.Property(x => x.CountryName).HasMaxLength(100);
            builder.Property(x => x.LeagueName).HasMaxLength(150);
            builder.Property(x => x.DateOfAnalyse).HasDefaultValue(DateTime.Now.Date);
            builder.HasMany(p => p.ComparisonStatisticsHolders).WithOne(x => x.LeagueStatisticsHolder).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p => p.TeamPerformanceStatisticsHolders).WithOne(x => x.LeagueStatisticsHolder).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p => p.AverageStatisticsHolders).WithOne(x => x.LeagueStatisticsHolder).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

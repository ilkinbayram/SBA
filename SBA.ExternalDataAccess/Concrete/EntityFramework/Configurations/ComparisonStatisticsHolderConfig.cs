using Core.Entities.Concrete;
using Core.Entities.Concrete.ExternalDbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SBA.ExternalDataAccess.Concrete.EntityFramework.Configurations
{
    public class ComparisonStatisticsHolderConfig : IEntityTypeConfiguration<ComparisonStatisticsHolder>
    {
        public void Configure(EntityTypeBuilder<ComparisonStatisticsHolder> builder)
        {
            builder.ToTable("ComparisonStatisticsHolders");
            builder.HasKey(k => k.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Average_FT_Goals_HomeTeam).HasPrecision(7,2);
            builder.Property(x => x.Average_HT_Goals_HomeTeam).HasPrecision(7,2);
            builder.Property(x => x.Average_SH_Goals_HomeTeam).HasPrecision(7,2);
            builder.Property(x => x.Average_FT_Goals_AwayTeam).HasPrecision(7,2);
            builder.Property(x => x.Average_HT_Goals_AwayTeam).HasPrecision(7,2);
            builder.Property(x => x.Average_SH_Goals_AwayTeam).HasPrecision(7,2);
            builder.Property(x => x.Average_FT_Corners_HomeTeam).HasPrecision(7,2);
            builder.Property(x => x.Average_FT_Corners_AwayTeam).HasPrecision(7,2);

            builder.HasOne(p => p.LeagueStatisticsHolder).WithMany(x => x.ComparisonStatisticsHolders).HasForeignKey(x => x.LeagueStaisticsHolderId);
        }
    }
}

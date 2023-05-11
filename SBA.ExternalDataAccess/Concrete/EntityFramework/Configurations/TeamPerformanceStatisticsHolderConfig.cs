using Core.Entities.Concrete.ExternalDbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SBA.ExternalDataAccess.Concrete.EntityFramework.Configurations
{
    public class TeamPerformanceStatisticsHolderConfig : IEntityTypeConfiguration<TeamPerformanceStatisticsHolder>
    {
        public void Configure(EntityTypeBuilder<TeamPerformanceStatisticsHolder> builder)
        {
            builder.ToTable("TeamPerformanceStatisticsHolders");
            builder.HasKey(k => k.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.CreatedBy).HasMaxLength(100).HasDefaultValue("System.Admin");
            builder.Property(x => x.ModifiedBy).HasMaxLength(100).HasDefaultValue("System.Admin");

            builder.Property(x => x.CreatedDateTime).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.ModifiedDateTime).HasDefaultValue(DateTime.Now);

            builder.Property(x => x.Average_FT_Goals_Team).HasPrecision(7, 2);
            builder.Property(x => x.Average_HT_Goals_Team).HasPrecision(7, 2);
            builder.Property(x => x.Average_SH_Goals_Team).HasPrecision(7, 2);
            builder.Property(x => x.Average_FT_Corners_Team).HasPrecision(7, 2);

            builder.HasOne(p => p.LeagueStatisticsHolder).WithMany(x => x.TeamPerformanceStatisticsHolders).HasForeignKey(x => x.LeagueStaisticsHolderId);
        }
    }
}

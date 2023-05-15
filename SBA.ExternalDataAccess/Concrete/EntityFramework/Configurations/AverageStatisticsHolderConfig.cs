using Core.Entities.Concrete.ExternalDbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SBA.ExternalDataAccess.Concrete.EntityFramework.Configurations
{
    public class AverageStatisticsHolderConfig : IEntityTypeConfiguration<AverageStatisticsHolder>
    {
        public void Configure(EntityTypeBuilder<AverageStatisticsHolder> builder)
        {
            builder.ToTable("AverageStatisticsHolders");
            builder.HasKey(k => k.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.CreatedBy).HasMaxLength(100).HasDefaultValue("System.Admin");
            builder.Property(x => x.ModifiedBy).HasMaxLength(100).HasDefaultValue("System.Admin");

            builder.Property(x => x.CreatedDateTime).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.ModifiedDateTime).HasDefaultValue(DateTime.Now);

            builder.Property(x => x.Average_FT_Goals_HomeTeam).HasPrecision(7, 2);
            builder.Property(x => x.Average_HT_Goals_HomeTeam).HasPrecision(7, 2);
            builder.Property(x => x.Average_SH_Goals_HomeTeam).HasPrecision(7, 2);
            builder.Property(x => x.Average_FT_Goals_AwayTeam).HasPrecision(7, 2);
            builder.Property(x => x.Average_HT_Goals_AwayTeam).HasPrecision(7, 2);
            builder.Property(x => x.Average_SH_Goals_AwayTeam).HasPrecision(7, 2);
            builder.Property(x => x.Average_FT_Corners_HomeTeam).HasPrecision(7, 2);
            builder.Property(x => x.Average_FT_Corners_AwayTeam).HasPrecision(7, 2);
            builder.Property(x => x.Average_FT_GK_Saves_HomeTeam).HasPrecision(7, 2);
            builder.Property(x => x.Average_FT_GK_Saves_AwayTeam).HasPrecision(7, 2);
            builder.Property(x => x.Average_FT_ShutOnTarget_HomeTeam).HasPrecision(7, 2);
            builder.Property(x => x.Average_FT_ShutOnTarget_AwayTeam).HasPrecision(7, 2);
            builder.Property(x => x.Average_FT_Shut_HomeTeam).HasPrecision(7, 2);
            builder.Property(x => x.Average_FT_Shut_AwayTeam).HasPrecision(7, 2);

            builder.HasOne(p => p.LeagueStatisticsHolder).WithMany(x => x.AverageStatisticsHolders).HasForeignKey(x=>x.LeagueStaisticsHolderId);
        }
    }
}

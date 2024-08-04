using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBA.DataAccess.Concrete.EntityFramework.Configurations.BaseConfig;

namespace SBA.DataAccess.Concrete.EntityFramework.Configurations
{
    public class PerformanceOverallConfig : BaseEntityTypeConfig<PerformanceOverall>
    {
        public void Configure(EntityTypeBuilder<PerformanceOverall> builder)
        {
            builder.ToTable("PerformanceOverall");

            builder.Property(x => x.Average_FT_Goals_Home_Team).HasPrecision(7, 2);
            builder.Property(x => x.Average_FT_Goals_Away_Team).HasPrecision(7, 2);
            builder.Property(x => x.Average_HT_Goals_Home_Team).HasPrecision(7, 2);
            builder.Property(x => x.Average_HT_Goals_Away_Team).HasPrecision(7, 2);
            builder.Property(x => x.Average_SH_Goals_Home_Team).HasPrecision(7, 2);
            builder.Property(x => x.Average_SH_Goals_Away_Team).HasPrecision(7, 2);
            builder.Property(x => x.Average_FT_Conceded_Goals_Home_Team).HasPrecision(7, 2);
            builder.Property(x => x.Average_FT_Conceded_Goals_Away_Team).HasPrecision(7, 2);
            builder.Property(x => x.Average_HT_Conceded_Goals_Home_Team).HasPrecision(7, 2);
            builder.Property(x => x.Average_HT_Conceded_Goals_Away_Team).HasPrecision(7, 2);
            builder.Property(x => x.Average_SH_Conceded_Goals_Home_Team).HasPrecision(7, 2);
            builder.Property(x => x.Average_SH_Conceded_Goals_Away_Team).HasPrecision(7, 2);

            builder.Property(x => x.Average_FT_GK_Saves_Home_Team).HasPrecision(7, 2);
            builder.Property(x => x.Average_FT_GK_Saves_Away_Team).HasPrecision(7, 2);
        }
    }
}

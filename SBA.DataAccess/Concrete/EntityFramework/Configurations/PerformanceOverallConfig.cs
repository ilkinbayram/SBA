using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SBA.DataAccess.Concrete.EntityFramework.Configurations
{
    public class PerformanceOverallConfig : IEntityTypeConfiguration<PerformanceOverall>
    {
        public void Configure(EntityTypeBuilder<PerformanceOverall> builder)
        {
            TimeZoneInfo azerbaycanZone = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");
            DateTime azerbaycanTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, azerbaycanZone);

            builder.ToTable("PerformanceOverall");
            builder.HasKey(k => k.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.CreatedBy).HasMaxLength(100).HasDefaultValue("System.Admin");
            builder.Property(x => x.ModifiedBy).HasMaxLength(100).HasDefaultValue("System.Admin");

            builder.Property(x => x.CreatedDateTime).HasDefaultValue(azerbaycanTime.Date);
            builder.Property(x => x.ModifiedDateTime).HasDefaultValue(azerbaycanTime.Date);

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

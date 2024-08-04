using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBA.DataAccess.Concrete.EntityFramework.Configurations.BaseConfig;

namespace SBA.DataAccess.Concrete.EntityFramework.Configurations
{
    public class MatchBetConfiguration : BaseEntityTypeConfig<MatchBet>
    {
        public void Configure(EntityTypeBuilder<MatchBet> builder)
        {
            builder.ToTable("MatchBets");

            builder.Property(x => x.Country).HasMaxLength(100);
            builder.Property(x => x.LeagueName).HasMaxLength(100);
            builder.Property(x => x.HomeTeam).HasMaxLength(100);
            builder.Property(x => x.AwayTeam).HasMaxLength(100);
            builder.Property(x => x.HT_Match_Result).HasMaxLength(10);
            builder.Property(x => x.FT_Match_Result).HasMaxLength(10);

            builder.HasIndex(x=>x.SerialUniqueID).IsClustered(false).HasDatabaseName("IX_SerialUniqueID");
            builder.HasIndex(x=>x.LeagueName).IsClustered(false).HasDatabaseName("IX_LeagueName");
            builder.HasIndex(x=>x.Country).IsClustered(false).HasDatabaseName("IX_Country");

            builder.Property(x => x.FTWin1_Odd).HasPrecision(7, 2);
            builder.Property(x => x.FTDraw_Odd).HasPrecision(7, 2);
            builder.Property(x => x.FTWin2_Odd).HasPrecision(7, 2);
            builder.Property(x => x.HTWin1_Odd).HasPrecision(7, 2);
            builder.Property(x => x.HTDraw_Odd).HasPrecision(7, 2);
            builder.Property(x => x.HTWin2_Odd).HasPrecision(7, 2);
            builder.Property(x => x.HT_Under_1_5_Odd).HasPrecision(7, 2);
            builder.Property(x => x.HT_Over_1_5_Odd).HasPrecision(7, 2);
            builder.Property(x => x.FT_Under_1_5_Odd).HasPrecision(7, 2);
            builder.Property(x => x.FT_Over_1_5_Odd).HasPrecision(7, 2);
            builder.Property(x => x.FT_Under_2_5_Odd).HasPrecision(7, 2);
            builder.Property(x => x.FT_Over_2_5_Odd).HasPrecision(7, 2);
            builder.Property(x => x.FT_Under_3_5_Odd).HasPrecision(7, 2);
            builder.Property(x => x.FT_Over_3_5_Odd).HasPrecision(7, 2);
            builder.Property(x => x.FT_GG_Odd).HasPrecision(7, 2);
            builder.Property(x => x.FT_NG_Odd).HasPrecision(7, 2);
            builder.Property(x => x.FT_01_Odd).HasPrecision(7, 2);
            builder.Property(x => x.FT_23_Odd).HasPrecision(7, 2);
            builder.Property(x => x.FT_45_Odd).HasPrecision(7, 2);
            builder.Property(x => x.FT_6_Odd).HasPrecision(7, 2);
            builder.Property(x => x.MatchDate).HasPrecision(7, 2);
        }
    }
}

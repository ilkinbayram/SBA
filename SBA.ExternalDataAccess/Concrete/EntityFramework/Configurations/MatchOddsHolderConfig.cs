using Core.Entities.Concrete.ExternalDbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SBA.ExternalDataAccess.Concrete.EntityFramework.Configurations
{
    public class MatchOddsHolderConfig : IEntityTypeConfiguration<MatchOddsHolder>
    {
        public void Configure(EntityTypeBuilder<MatchOddsHolder> builder)
        {
            TimeZoneInfo azerbaycanZone = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");
            DateTime azerbaycanTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, azerbaycanZone);

            builder.ToTable("MatchOddsHolders");
            builder.HasKey(k => k.Id);

            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Serial).HasDefaultValue(0);

            builder.Property(x => x.CreatedBy).HasMaxLength(100).HasDefaultValue("System.Admin");
            builder.Property(x => x.ModifiedBy).HasMaxLength(100).HasDefaultValue("System.Admin");

            builder.Property(x => x.CreatedDateTime).HasDefaultValue(azerbaycanTime.Date);
            builder.Property(x => x.ModifiedDateTime).HasDefaultValue(azerbaycanTime.Date);

            builder.Property(x => x.FT_Win1).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.FT_Draw).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.FT_Win2).HasPrecision(7, 2).HasDefaultValue(-1m);

            builder.Property(x => x.HT_FT_Home_Home).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.HT_FT_Home_Draw).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.HT_FT_Home_Away).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.HT_FT_Draw_Home).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.HT_FT_Draw_Draw).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.HT_FT_Draw_Away).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.HT_FT_Away_Home).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.HT_FT_Away_Draw).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.HT_FT_Away_Away).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.FT_Win1_Under_15).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.FT_Draw_Under_15).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.FT_Win2_Under_15).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.FT_Win1_Over_15).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.FT_Draw_Over_15).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.FT_Win2_Over_15).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.FT_Win1_Under_25).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.FT_Draw_Under_25).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.FT_Win2_Under_25).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.FT_Win1_Over_25).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.FT_Draw_Over_25).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.FT_Win2_Over_25).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.FT_Win1_Under_35).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.FT_Draw_Under_35).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.FT_Win2_Under_35).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.FT_Win1_Over_35).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.FT_Draw_Over_35).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.FT_Win2_Over_35).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.FT_Win1_Under_45).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.FT_Draw_Under_45).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.FT_Win2_Under_45).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.FT_Win1_Over_45).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.FT_Draw_Over_45).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.FT_Win2_Over_45).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Handicap_04_Win1).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Handicap_04_Draw).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Handicap_04_Win2).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Handicap_03_Win1).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Handicap_03_Draw).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Handicap_03_Win2).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Handicap_02_Win1).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Handicap_02_Draw).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Handicap_02_Win2).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Handicap_01_Win1).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Handicap_01_Draw).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Handicap_01_Win2).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Handicap_40_Win1).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Handicap_40_Draw).HasPrecision(7, 2).HasDefaultValue(-1m);

            builder.Property(x => x.Handicap_40_Win2).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Handicap_30_Win1).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Handicap_30_Draw).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Handicap_30_Win2).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Handicap_20_Win1).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Handicap_20_Draw).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Handicap_20_Win2).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Handicap_10_Win1).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Handicap_10_Draw).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Handicap_10_Win2).HasPrecision(7, 2).HasDefaultValue(-1m);

            builder.Property(x => x.FT_Double_1_X).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.FT_Double_1_2).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.FT_Double_X_2).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.FirstGoal_Home).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.FirstGoal_None).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.FirstGoal_Away).HasPrecision(7, 2).HasDefaultValue(-1m);

            builder.Property(x => x.FT_4_5_Under).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.FT_4_5_Over).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.FT_5_5_Under).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.FT_5_5_Over).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.HT_0_5_Under).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.HT_0_5_Over).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.HT_2_5_Under).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.HT_2_5_Over).HasPrecision(7, 2).HasDefaultValue(-1m);

            builder.Property(x => x.Home_2_5_Under).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Home_2_5_Over).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Home_3_5_Under).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Home_3_5_Over).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Home_4_5_Under).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Home_4_5_Over).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Away_2_5_Under).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Away_2_5_Over).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Away_3_5_Under).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Away_3_5_Over).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Away_4_5_Under).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Away_4_5_Over).HasPrecision(7, 2).HasDefaultValue(-1m);

            builder.Property(x => x.Even_Tek).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Odd_Cut).HasPrecision(7, 2).HasDefaultValue(-1m);

            builder.Property(x => x.Score_0_0).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Score_1_0).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Score_2_0).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Score_3_0).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Score_4_0).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Score_5_0).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Score_6_0).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Score_0_1).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Score_0_2).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Score_0_3).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Score_0_4).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Score_0_5).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Score_0_6).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Score_1_1).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Score_2_1).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Score_3_1).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Score_4_1).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Score_5_1).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Score_1_2).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Score_1_3).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Score_1_4).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Score_1_5).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Score_2_2).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Score_3_2).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Score_4_2).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Score_2_3).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Score_2_4).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Score_3_3).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.Score_Other).HasPrecision(7, 2).HasDefaultValue(-1m);

            builder.Property(x => x.MoreGoal_1st).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.MoreGoal_Equal).HasPrecision(7, 2).HasDefaultValue(-1m);
            builder.Property(x => x.MoreGoal_2nd).HasPrecision(7, 2).HasDefaultValue(-1m);
        }
    }
}

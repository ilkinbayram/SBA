using Core.Entities.Concrete.Base;

namespace Core.Entities.Concrete.ExternalDbEntities
{
    public class AverageStatisticsHolder : BaseStatisticsHolder, IEntity
    {
        public AverageStatisticsHolder()
        {
            Average_FT_Goals_HomeTeam = -1;
            Average_FT_Goals_AwayTeam = -1;

            Average_HT_Goals_HomeTeam = -1;
            Average_HT_Goals_AwayTeam = -1;

            Average_SH_Goals_HomeTeam = -1;
            Average_SH_Goals_AwayTeam = -1;

            Average_FT_Conceeded_Goals_HomeTeam = -1;
            Average_FT_Conceeded_Goals_AwayTeam = -1;

            Average_HT_Conceeded_Goals_HomeTeam = -1;
            Average_HT_Conceeded_Goals_AwayTeam = -1;

            Average_SH_Conceeded_Goals_HomeTeam = -1;
            Average_SH_Conceeded_Goals_AwayTeam = -1;

            Average_FT_Corners_HomeTeam = -1;
            Average_FT_Corners_AwayTeam = -1;

            Average_FT_Shut_HomeTeam = -1;
            Average_FT_Shut_AwayTeam = -1;

            Average_FT_ShutOnTarget_HomeTeam = -1;
            Average_FT_ShutOnTarget_AwayTeam = -1;

            Average_FT_GK_Saves_HomeTeam = -1;
            Average_FT_GK_Saves_AwayTeam = -1;

            Home_Possesion = -1;
            Away_Possesion = -1;

            Is_FT_Win1 = -1;
            Is_FT_X1 = -1;
            Is_FT_X2 = -1;
            Is_FT_Win2 = -1;

            Is_HT_Win1 = -1;
            Is_HT_X1 = -1;
            Is_HT_X2 = -1;
            Is_HT_Win2 = -1;

            Is_SH_Win1 = -1;
            Is_SH_X1 = -1;
            Is_SH_X2 = -1;
            Is_SH_Win2 = -1;

            Corner_Home_3_5_Over = -1;

            Corner_Home_4_5_Over = -1;

            Corner_Home_5_5_Over = -1;

            Corner_Away_3_5_Over = -1;
            Corner_Away_4_5_Over = -1;
            Corner_Away_5_5_Over = -1;

            Corner_7_5_Over_Home = -1;
            Corner_8_5_Over_Home = -1;
            Corner_9_5_Over_Home = -1;
            Corner_7_5_Over_Away = -1;
            Corner_8_5_Over_Away = -1;
            Corner_9_5_Over_Away = -1;

            Is_Corner_FT_Win1 = -1;
            Is_Corner_FT_X1 = -1;
            Is_Corner_FT_X2 = -1;
            Is_Corner_FT_Win2 = -1;

            FT_GG_Home = -1;
            SH_GG_Home = -1;
            HT_GG_Home = -1;
            FT_15_Over_Home = -1;
            FT_25_Over_Home = -1;
            FT_35_Over_Home = -1;
            HT_05_Over_Home = -1;
            HT_15_Over_Home = -1;
            SH_05_Over_Home = -1;
            SH_15_Over_Home = -1;
            FT_GG_Away = -1;
            SH_GG_Away = -1;
            HT_GG_Away = -1;
            FT_15_Over_Away = -1;
            FT_25_Over_Away = -1;
            FT_35_Over_Away = -1;
            HT_05_Over_Away = -1;
            HT_15_Over_Away = -1;
            SH_05_Over_Away = -1;
            SH_15_Over_Away = -1;
            Home_HT_05_Over = -1;
            Home_HT_15_Over = -1;
            Home_SH_05_Over = -1;
            Home_SH_15_Over = -1;
            Home_FT_05_Over = -1;
            Home_FT_15_Over = -1;
            Home_Win_Any_Half = -1;
            Away_HT_05_Over = -1;
            Away_HT_15_Over = -1;
            Away_SH_05_Over = -1;
            Away_SH_15_Over = -1;
            Away_FT_05_Over = -1;
            Away_FT_15_Over = -1;
            Away_Win_Any_Half = -1;
            UniqueIdentity = Guid.NewGuid();
        }

        public int BySideType { get; set; }

        public int LeagueStaisticsHolderId { get; set; }

        public decimal Average_FT_Goals_HomeTeam { get; set; }
        public decimal Average_FT_Goals_AwayTeam { get; set; }
        public decimal Average_HT_Goals_HomeTeam { get; set; }
        public decimal Average_HT_Goals_AwayTeam { get; set; }
        public decimal Average_SH_Goals_HomeTeam { get; set; }
        public decimal Average_SH_Goals_AwayTeam { get; set; }

        public decimal Average_FT_Conceeded_Goals_HomeTeam { get; set; }
        public decimal Average_FT_Conceeded_Goals_AwayTeam { get; set; }
        public decimal Average_HT_Conceeded_Goals_HomeTeam { get; set; }
        public decimal Average_HT_Conceeded_Goals_AwayTeam { get; set; }
        public decimal Average_SH_Conceeded_Goals_HomeTeam { get; set; }
        public decimal Average_SH_Conceeded_Goals_AwayTeam { get; set; }

        public decimal Average_FT_Corners_HomeTeam { get; set; }
        public decimal Average_FT_Corners_AwayTeam { get; set; }


        public decimal Average_FT_Shut_HomeTeam { get; set; }
        public decimal Average_FT_Shut_AwayTeam { get; set; }
        public decimal Average_FT_ShutOnTarget_HomeTeam { get; set; }
        public decimal Average_FT_ShutOnTarget_AwayTeam { get; set; }
        public decimal Average_FT_GK_Saves_HomeTeam { get; set; }
        public decimal Average_FT_GK_Saves_AwayTeam { get; set; }
        public int Home_Possesion { get; set; }
        public int Away_Possesion { get; set; }

        public int Is_FT_Win1 { get; set; }
        public int Is_FT_X1 { get; set; }
        public int Is_FT_X2 { get; set; }
        public int Is_FT_Win2 { get; set; }

        public int Is_HT_Win1 { get; set; }
        public int Is_HT_X1 { get; set; }
        public int Is_HT_X2 { get; set; }
        public int Is_HT_Win2 { get; set; }

        public int Is_SH_Win1 { get; set; }
        public int Is_SH_X1 { get; set; }
        public int Is_SH_X2 { get; set; }
        public int Is_SH_Win2 { get; set; }

        public int Corner_Home_3_5_Over { get; set; }
        public int Corner_Home_4_5_Over { get; set; }
        public int Corner_Home_5_5_Over { get; set; }
        public int Corner_Away_3_5_Over { get; set; }
        public int Corner_Away_4_5_Over { get; set; }
        public int Corner_Away_5_5_Over { get; set; }
        public int Corner_7_5_Over_Home { get; set; }
        public int Corner_8_5_Over_Home { get; set; }
        public int Corner_9_5_Over_Home { get; set; }
        public int Corner_7_5_Over_Away { get; set; }
        public int Corner_8_5_Over_Away { get; set; }
        public int Corner_9_5_Over_Away { get; set; }
        public int Is_Corner_FT_Win1 { get; set; }
        public int Is_Corner_FT_X1 { get; set; }
        public int Is_Corner_FT_X2 { get; set; }
        public int Is_Corner_FT_Win2 { get; set; }

        public int FT_GG_Home { get; set; }
        public int SH_GG_Home { get; set; }
        public int HT_GG_Home { get; set; }
        public int FT_15_Over_Home { get; set; }
        public int FT_25_Over_Home { get; set; }
        public int FT_35_Over_Home { get; set; }
        public int HT_05_Over_Home { get; set; }
        public int HT_15_Over_Home { get; set; }
        public int SH_05_Over_Home { get; set; }
        public int SH_15_Over_Home { get; set; }
        public int FT_GG_Away { get; set; }
        public int SH_GG_Away { get; set; }
        public int HT_GG_Away { get; set; }
        public int FT_15_Over_Away { get; set; }
        public int FT_25_Over_Away { get; set; }
        public int FT_35_Over_Away { get; set; }
        public int HT_05_Over_Away { get; set; }
        public int HT_15_Over_Away { get; set; }
        public int SH_05_Over_Away { get; set; }
        public int SH_15_Over_Away { get; set; }
        public int Home_HT_05_Over { get; set; }
        public int Home_HT_15_Over { get; set; }
        public int Home_SH_05_Over { get; set; }
        public int Home_SH_15_Over { get; set; }
        public int Home_FT_05_Over { get; set; }
        public int Home_FT_15_Over { get; set; }
        public int Home_Win_Any_Half { get; set; }
        public int Away_HT_05_Over { get; set; }
        public int Away_HT_15_Over { get; set; }
        public int Away_SH_05_Over { get; set; }
        public int Away_SH_15_Over { get; set; }
        public int Away_FT_05_Over { get; set; }
        public int Away_FT_15_Over { get; set; }
        public int Away_Win_Any_Half { get; set; }

        public virtual LeagueStatisticsHolder LeagueStatisticsHolder { get; set; }
    }
}

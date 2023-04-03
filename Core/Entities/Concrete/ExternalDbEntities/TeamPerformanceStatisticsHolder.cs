using Core.Entities.Concrete.Base;

namespace Core.Entities.Concrete.ExternalDbEntities
{
    public class TeamPerformanceStatisticsHolder : BaseStatisticsHolder, IEntity
    {
        public TeamPerformanceStatisticsHolder()
        {
            Average_FT_Goals_Team = -1;
            Average_HT_Goals_Team = -1;
            Average_SH_Goals_Team = -1;
            Average_FT_Corners_Team = -1;
            Average_FT_Corners_Team = -1;
            Average_FT_Shut_Team = -1;
            Average_FT_ShutOnTarget_Team = -1;
            Team_Possesion = -1;
            Is_FT_Win = -1;
            Is_FT_X = -1;
            Is_HT_Win = -1;
            Is_HT_X = -1;
            Is_SH_Win = -1;
            Is_SH_X = -1;
            Corner_Team_3_5_Over = -1;
            Corner_Team_4_5_Over = -1;
            Corner_Team_5_5_Over = -1;
            Corner_7_5_Over = -1;
            Corner_8_5_Over = -1;
            Corner_9_5_Over = -1;
            Is_Corner_FT_Win = -1;
            Is_Corner_FT_X = -1;
            FT_GG = -1;
            SH_GG = -1;
            HT_GG = -1;
            FT_15_Over = -1;
            FT_25_Over = -1;
            FT_35_Over = -1;
            HT_05_Over = -1;
            HT_15_Over = -1;
            SH_05_Over = -1;
            SH_15_Over = -1;
            Team_HT_05_Over = -1;
            Team_HT_15_Over = -1;
            Team_SH_05_Over = -1;
            Team_SH_15_Over = -1;
            Team_FT_05_Over = -1;
            Team_FT_15_Over = -1;
            Team_Win_Any_Half = -1;
        }

        public int BySideType { get; set; }
        public int HomeOrAway { get; set; }

        public int LeagueStaisticsHolderId { get; set; }

        public decimal Average_FT_Goals_Team { get; set; }
        public decimal Average_HT_Goals_Team { get; set; }
        public decimal Average_SH_Goals_Team { get; set; }
        public decimal Average_FT_Corners_Team { get; set; }

        public decimal Average_FT_Shut_Team { get; set; }
        public decimal Average_FT_ShutOnTarget_Team { get; set; }
        public int Team_Possesion { get; set; }


        public int Is_FT_Win { get; set; }
        public int Is_FT_X { get; set; }

        public int Is_HT_Win { get; set; }
        public int Is_HT_X { get; set; }

        public int Is_SH_Win { get; set; }
        public int Is_SH_X { get; set; }

        public int Corner_Team_3_5_Over { get; set; }
        public int Corner_Team_4_5_Over { get; set; }
        public int Corner_Team_5_5_Over { get; set; }
        public int Corner_7_5_Over { get; set; }
        public int Corner_8_5_Over { get; set; }
        public int Corner_9_5_Over { get; set; }
        public int Is_Corner_FT_Win { get; set; }
        public int Is_Corner_FT_X { get; set; }

        public int FT_GG { get; set; }
        public int SH_GG { get; set; }
        public int HT_GG { get; set; }
        public int FT_15_Over { get; set; }
        public int FT_25_Over { get; set; }
        public int FT_35_Over { get; set; }
        public int HT_05_Over { get; set; }
        public int HT_15_Over { get; set; }
        public int SH_05_Over { get; set; }
        public int SH_15_Over { get; set; }
        public int Team_HT_05_Over { get; set; }
        public int Team_HT_15_Over { get; set; }
        public int Team_SH_05_Over { get; set; }
        public int Team_SH_15_Over { get; set; }
        public int Team_FT_05_Over { get; set; }
        public int Team_FT_15_Over { get; set; }
        public int Team_Win_Any_Half { get; set; }

        public virtual LeagueStatisticsHolder LeagueStatisticsHolder { get; set; }
    }
}

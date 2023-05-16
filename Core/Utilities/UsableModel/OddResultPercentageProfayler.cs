namespace Core.Utilities.UsableModel
{
    public class OddResultPercentageProfayler
    {
        public int CountFound { get; set; }

        public decimal Average_FT_Corners_HomeTeam { get; set; }
        public decimal Average_FT_Corners_AwayTeam { get; set; }
        public decimal Average_FT_Goals_HomeTeam { get; set; }
        public decimal Average_FT_Goals_AwayTeam { get; set; }
        public decimal Average_HT_Goals_HomeTeam { get; set; }
        public decimal Average_HT_Goals_AwayTeam { get; set; }
        public decimal Average_SH_Goals_HomeTeam { get; set; }
        public decimal Average_SH_Goals_AwayTeam { get; set; }

        public decimal Average_FT_Conceded_Goals_HomeTeam { get; set; }
        public decimal Average_FT_Conceded_Goals_AwayTeam { get; set; }
        public decimal Average_HT_Conceded_Goals_HomeTeam { get; set; }
        public decimal Average_HT_Conceded_Goals_AwayTeam { get; set; }
        public decimal Average_SH_Conceded_Goals_HomeTeam { get; set; }
        public decimal Average_SH_Conceded_Goals_AwayTeam { get; set; }

        public decimal Average_FT_Shot_HomeTeam { get; set; }
        public decimal Average_FT_Shot_AwayTeam { get; set; }
        public decimal Average_FT_ShotOnTarget_HomeTeam { get; set; }
        public decimal Average_FT_ShotOnTarget_AwayTeam { get; set; }
        public decimal Average_FT_Possesion_HomeTeam { get; set; }
        public decimal Average_FT_Possesion_AwayTeam { get; set; }
        public decimal Average_FT_GK_Saves_HomeTeam { get; set; }
        public decimal Average_FT_GK_Saves_AwayTeam { get; set; }


        public PercentageComplainer MoreGoalsBetweenTimes { get; set; }
        public PercentageComplainer HT_Result { get; set; }
        public PercentageComplainer FT_Result { get; set; }
        public PercentageComplainer SH_Result { get; set; }

        public PercentageComplainer Is_FT_Win1 { get; set; }
        public PercentageComplainer Is_FT_X { get; set; }
        public PercentageComplainer Is_FT_Win2 { get; set; }

        public PercentageComplainer Is_HT_Win1 { get; set; }
        public PercentageComplainer Is_HT_X { get; set; }
        public PercentageComplainer Is_HT_Win2 { get; set; }

        public PercentageComplainer Is_SH_Win1 { get; set; }
        public PercentageComplainer Is_SH_X { get; set; }
        public PercentageComplainer Is_SH_Win2 { get; set; }

        public PercentageComplainer Corner_Home_3_5_Over { get; set; }
        public PercentageComplainer Corner_Home_4_5_Over { get; set; }
        public PercentageComplainer Corner_Home_5_5_Over { get; set; }
        public PercentageComplainer Corner_Away_3_5_Over { get; set; }
        public PercentageComplainer Corner_Away_4_5_Over { get; set; }
        public PercentageComplainer Corner_Away_5_5_Over { get; set; }
        public PercentageComplainer Corner_7_5_Over { get; set; }
        public PercentageComplainer Corner_8_5_Over { get; set; }
        public PercentageComplainer Corner_9_5_Over { get; set; }
        public PercentageComplainer Is_Corner_FT_Win1 { get; set; }
        public PercentageComplainer Is_Corner_FT_X { get; set; }
        public PercentageComplainer Is_Corner_FT_Win2 { get; set; }

        public PercentageComplainer HT_FT_Result { get; set; }


        public PercentageComplainer Total_BetweenGoals { get; set; }

        public PercentageComplainer FT_GG { get; set; }
        public PercentageComplainer SH_GG { get; set; }
        public PercentageComplainer HT_GG { get; set; }


        public PercentageComplainer FT_15_Over { get; set; }
        public PercentageComplainer FT_25_Over { get; set; }
        public PercentageComplainer FT_35_Over { get; set; }

        public PercentageComplainer HT_05_Over { get; set; }
        public PercentageComplainer HT_15_Over { get; set; }

        public PercentageComplainer SH_05_Over { get; set; }
        public PercentageComplainer SH_15_Over { get; set; }

        public PercentageComplainer Home_HT_05_Over { get; set; }
        public PercentageComplainer Home_HT_15_Over { get; set; }

        public PercentageComplainer Home_SH_05_Over { get; set; }
        public PercentageComplainer Home_SH_15_Over { get; set; }

        public PercentageComplainer Home_FT_05_Over { get; set; }
        public PercentageComplainer Home_FT_15_Over { get; set; }

        public PercentageComplainer Home_Win_Any_Half { get; set; }


        public PercentageComplainer Away_HT_05_Over { get; set; }
        public PercentageComplainer Away_HT_15_Over { get; set; }

        public PercentageComplainer Away_SH_05_Over { get; set; }
        public PercentageComplainer Away_SH_15_Over { get; set; }

        public PercentageComplainer Away_FT_05_Over { get; set; }
        public PercentageComplainer Away_FT_15_Over { get; set; }

        public PercentageComplainer Away_Win_Any_Half { get; set; }
    }
}

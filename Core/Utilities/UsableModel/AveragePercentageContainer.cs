namespace Core.Utilities.UsableModel
{
    public class AveragePercentageContainer
    {
        public AveragePercentageContainer()
        {
            Average_FT_Goals_HomeTeam = (decimal)-1.00;
            Average_FT_Goals_AwayTeam = (decimal)-1.00;
            Average_HT_Goals_HomeTeam = (decimal)-1.00;
            Average_HT_Goals_AwayTeam = (decimal)-1.00;
            Average_SH_Goals_HomeTeam = (decimal)-1.00;
            Average_SH_Goals_AwayTeam = (decimal)-1.00;
        }


        public decimal Average_FT_Goals_HomeTeam { get; set; }
        public decimal Average_FT_Goals_AwayTeam { get; set; }
        public decimal Average_HT_Goals_HomeTeam { get; set; }
        public decimal Average_HT_Goals_AwayTeam { get; set; }
        public decimal Average_SH_Goals_HomeTeam { get; set; }
        public decimal Average_SH_Goals_AwayTeam { get; set; }
        public decimal Average_FT_Corners_HomeTeam { get; set; }
        public decimal Average_FT_Corners_AwayTeam { get; set; }
        public decimal Average_FT_Shot_HomeTeam { get; set; }
        public decimal Average_FT_Shot_AwayTeam { get; set; }
        public decimal Average_FT_ShotOnTarget_HomeTeam { get; set; }
        public decimal Average_FT_ShotOnTarget_AwayTeam { get; set; }
        public decimal Average_FT_GK_Saves_HomeTeam { get; set; }
        public decimal Average_FT_GK_Saves_AwayTeam { get; set; }
        public decimal Average_FT_Possesion_HomeTeam { get; set; }
        public decimal Average_FT_Possesion_AwayTeam { get; set; }


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


    public class AverageShort
    {
        public AverageShort(AveragePercentageContainer averagePercentage)
        {
            Average_FT_Corners_HomeTeam = averagePercentage.Average_FT_Corners_HomeTeam.ToString("0.00");
            Average_FT_Corners_AwayTeam = averagePercentage.Average_FT_Corners_AwayTeam.ToString("0.00");
            Average_FT_Goals_HomeTeam = averagePercentage.Average_FT_Goals_HomeTeam.ToString("0.00");
            Average_FT_Goals_AwayTeam = averagePercentage.Average_FT_Goals_AwayTeam.ToString("0.00");
            Average_HT_Goals_HomeTeam = averagePercentage.Average_HT_Goals_HomeTeam.ToString("0.00");
            Average_HT_Goals_AwayTeam = averagePercentage.Average_HT_Goals_AwayTeam.ToString("0.00");
            Average_SH_Goals_HomeTeam = averagePercentage.Average_SH_Goals_HomeTeam.ToString("0.00");
            Average_SH_Goals_AwayTeam = averagePercentage.Average_SH_Goals_AwayTeam.ToString("0.00");
            FT_15_Over = averagePercentage.FT_15_Over;
            FT_25_Over = averagePercentage.FT_25_Over;
            FT_35_Over = averagePercentage.FT_35_Over;
            HT_05_Over = averagePercentage.HT_05_Over;
            HT_15_Over = averagePercentage.HT_15_Over;
            SH_05_Over = averagePercentage.SH_05_Over;
            SH_15_Over = averagePercentage.SH_15_Over;
            FT_GG = averagePercentage.FT_GG;
            Away_FT_05_Over = averagePercentage.Away_FT_05_Over;
            Home_FT_05_Over = averagePercentage.Home_FT_05_Over;
            Away_FT_15_Over = averagePercentage.Away_FT_15_Over;
            Home_FT_15_Over = averagePercentage.Home_FT_15_Over;
            Away_HT_05_Over = averagePercentage.Away_HT_05_Over;
            Home_HT_05_Over = averagePercentage.Home_HT_05_Over;
            Away_SH_05_Over = averagePercentage.Away_SH_05_Over;
            Home_SH_05_Over = averagePercentage.Home_SH_05_Over;
            Is_FT_Win1 = averagePercentage.Is_FT_Win1;
            Is_FT_X = averagePercentage.Is_FT_X;
            Is_FT_Win2 = averagePercentage.Is_FT_Win2;
            Is_HT_Win1 = averagePercentage.Is_HT_Win1;
            Is_HT_X = averagePercentage.Is_HT_X;
            Is_HT_Win2 = averagePercentage.Is_HT_Win2;
            Is_SH_Win1 = averagePercentage.Is_SH_Win1;
            Is_SH_X = averagePercentage.Is_SH_X;
            Is_SH_Win2 = averagePercentage.Is_SH_Win2;
            Is_Corner_FT_Win1 = averagePercentage.Is_Corner_FT_Win1;
            Is_Corner_FT_X = averagePercentage.Is_Corner_FT_X;
            Is_Corner_FT_Win2 = averagePercentage.Is_Corner_FT_Win2;
            Corner_Home_3_5_Over = averagePercentage.Corner_Home_3_5_Over;
            Corner_Home_4_5_Over = averagePercentage.Corner_Home_4_5_Over;
            Corner_Home_5_5_Over = averagePercentage.Corner_Home_5_5_Over;
            Corner_Away_3_5_Over = averagePercentage.Corner_Away_3_5_Over;
            Corner_Away_4_5_Over = averagePercentage.Corner_Away_4_5_Over;
            Corner_Away_5_5_Over = averagePercentage.Corner_Away_5_5_Over;
            Corner_7_5_Over = averagePercentage.Corner_7_5_Over;
            Corner_8_5_Over = averagePercentage.Corner_8_5_Over;
            Corner_9_5_Over = averagePercentage.Corner_9_5_Over;
        }

        public string Average_FT_Corners_HomeTeam { get; set; }
        public string Average_FT_Corners_AwayTeam { get; set; }
        public string Average_FT_Goals_HomeTeam { get; set; }
        public string Average_FT_Goals_AwayTeam { get; set; }
        public string Average_HT_Goals_HomeTeam { get; set; }
        public string Average_HT_Goals_AwayTeam { get; set; }
        public string Average_SH_Goals_HomeTeam { get; set; }
        public string Average_SH_Goals_AwayTeam { get; set; }

        public PercentageComplainer FT_15_Over { get; set; }
        public PercentageComplainer FT_25_Over { get; set; }
        public PercentageComplainer FT_35_Over { get; set; }
        public PercentageComplainer HT_05_Over { get; set; }
        public PercentageComplainer HT_15_Over { get; set; }
        public PercentageComplainer SH_05_Over { get; set; }
        public PercentageComplainer SH_15_Over { get; set; }
        public PercentageComplainer FT_GG { get; set; }
        public PercentageComplainer Away_FT_05_Over { get; set; }
        public PercentageComplainer Home_FT_05_Over { get; set; }
        public PercentageComplainer Away_FT_15_Over { get; set; }
        public PercentageComplainer Home_FT_15_Over { get; set; }
        public PercentageComplainer Away_HT_05_Over { get; set; }
        public PercentageComplainer Home_HT_05_Over { get; set; }
        public PercentageComplainer Away_SH_05_Over { get; set; }
        public PercentageComplainer Home_SH_05_Over { get; set; }

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
    }
}

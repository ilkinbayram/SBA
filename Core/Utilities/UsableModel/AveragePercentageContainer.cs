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



        public PercentageComplainer MoreGoalsBetweenTimes { get; set; }
        public PercentageComplainer HT_Result { get; set; }
        public PercentageComplainer FT_Result { get; set; }
        public PercentageComplainer SH_Result { get; set; }
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
            Average_FT_Goals_HomeTeam = averagePercentage.Average_FT_Goals_HomeTeam;
            Average_FT_Goals_AwayTeam = averagePercentage.Average_FT_Goals_AwayTeam;
            Average_HT_Goals_HomeTeam = averagePercentage.Average_HT_Goals_HomeTeam;
            Average_HT_Goals_AwayTeam = averagePercentage.Average_HT_Goals_AwayTeam;
            Average_SH_Goals_HomeTeam = averagePercentage.Average_SH_Goals_HomeTeam;
            Average_SH_Goals_AwayTeam = averagePercentage.Average_SH_Goals_AwayTeam;
            FT_15_Over = averagePercentage.FT_15_Over;
            FT_25_Over = averagePercentage.FT_25_Over;
            FT_35_Over = averagePercentage.FT_35_Over;
            Away_FT_05_Over = averagePercentage.Away_FT_05_Over;
            Home_FT_05_Over = averagePercentage.Home_FT_05_Over;
        }

        public decimal Average_FT_Goals_HomeTeam { get; set; }
        public decimal Average_FT_Goals_AwayTeam { get; set; }
        public decimal Average_HT_Goals_HomeTeam { get; set; }
        public decimal Average_HT_Goals_AwayTeam { get; set; }
        public decimal Average_SH_Goals_HomeTeam { get; set; }
        public decimal Average_SH_Goals_AwayTeam { get; set; }

        public PercentageComplainer FT_15_Over { get; set; }
        public PercentageComplainer FT_25_Over { get; set; }
        public PercentageComplainer FT_35_Over { get; set; }
        public PercentageComplainer Away_FT_05_Over { get; set; }
        public PercentageComplainer Home_FT_05_Over { get; set; }
    }
}

namespace Core.Utilities.UsableModel
{
    public class TeamPercentageProfiler
    {
        public TeamPercentageProfiler()
        {
            ZEND_HT_Result = string.Empty;
            ZEND_FT_Result = string.Empty;
        }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string Serial { get; set; }
        public string TargetURL { get; set; }
        public string ZEND_HT_Result { get; set; }
        public string ZEND_FT_Result { get; set; }
        public PercentageComplainer HT_Result { get; set; }
        public PercentageComplainer SH_Result { get; set; }
        public PercentageComplainer FT_Result { get; set; }
        public PercentageComplainer MoreGoalsBetweenTimes { get; set; }
        public PercentageComplainer Home_FT_0_5_Over { get; set; }
        public PercentageComplainer Home_FT_1_5_Over { get; set; }
        public PercentageComplainer Home_HT_0_5_Over { get; set; }
        public PercentageComplainer Home_HT_1_5_Over { get; set; }
        public PercentageComplainer Home_SH_0_5_Over { get; set; }
        public PercentageComplainer Home_SH_1_5_Over { get; set; }
        public PercentageComplainer Away_FT_0_5_Over { get; set; }
        public PercentageComplainer Away_FT_1_5_Over { get; set; }
        public PercentageComplainer Away_HT_0_5_Over { get; set; }
        public PercentageComplainer Away_HT_1_5_Over { get; set; }
        public PercentageComplainer Away_SH_0_5_Over { get; set; }
        public PercentageComplainer Away_SH_1_5_Over { get; set; }
        public PercentageComplainer Away_Win_Any_Half { get; set; }
        public PercentageComplainer Home_Win_Any_Half { get; set; }
        public PercentageComplainer HT_FT_Result { get; set; }
        public PercentageComplainer HT_0_5_Over { get; set; }
        public PercentageComplainer HT_1_5_Over { get; set; }
        public PercentageComplainer SH_0_5_Over { get; set; }
        public PercentageComplainer SH_1_5_Over { get; set; }
        public PercentageComplainer FT_1_5_Over { get; set; }
        public PercentageComplainer FT_2_5_Over { get; set; }
        public PercentageComplainer FT_3_5_Over { get; set; }
        public PercentageComplainer FT_GG { get; set; }
        public PercentageComplainer HT_GG { get; set; }
        public PercentageComplainer SH_GG { get; set; }
        public PercentageComplainer FT_TotalBetween { get; set; }
    }
}

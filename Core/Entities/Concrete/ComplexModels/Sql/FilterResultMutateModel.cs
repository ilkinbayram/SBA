namespace Core.Entities.Concrete.ComplexModels.Sql
{
    public class FilterResultMutateModel
    {
        public bool Home_HT_0_5_Over { get; set; }
        public bool Home_HT_1_5_Over { get; set; }
        public bool Home_SH_0_5_Over { get; set; }
        public bool Home_SH_1_5_Over { get; set; }
        public bool Away_HT_0_5_Over { get; set; }
        public bool Away_HT_1_5_Over { get; set; }
        public bool Away_SH_0_5_Over { get; set; }
        public bool Away_SH_1_5_Over { get; set; }
        public bool Away_Win_Any_Half { get; set; }
        public bool Home_Win_Any_Half { get; set; }
        public bool Away_FT_0_5_Over { get; set; }
        public bool Away_FT_1_5_Over { get; set; }
        public bool Home_FT_0_5_Over { get; set; }
        public bool Home_FT_1_5_Over { get; set; }
        public bool HT_0_5_Over { get; set; }
        public bool HT_1_5_Over { get; set; }
        public bool SH_0_5_Over { get; set; }
        public bool SH_1_5_Over { get; set; }
        public bool FT_1_5_Over { get; set; }
        public bool FT_2_5_Over { get; set; }
        public bool FT_3_5_Over { get; set; }
        public bool FT_GG { get; set; }
        public bool HT_GG { get; set; }
        public bool SH_GG { get; set; }
        public bool IsCornerFound { get; set; }
        public bool Corner_7_5_Over { get; set; }
        public bool Corner_8_5_Over { get; set; }
        public bool Corner_9_5_Over { get; set; }
        public bool Corner_Home_3_5_Over { get; set; }
        public bool Corner_Home_4_5_Over { get; set; }
        public bool Corner_Home_5_5_Over { get; set; }
        public bool Corner_Away_3_5_Over { get; set; }
        public bool Corner_Away_4_5_Over { get; set; }
        public bool Corner_Away_5_5_Over { get; set; }
        public int HomeCornerCount { get; set; }
        public int AwayCornerCount { get; set; }
        public int HomePossesion { get; set; }
        public int AwayPossesion { get; set; }
        public int HomeShotCount { get; set; }
        public int AwayShotCount { get; set; }
        public int HomeShotOnTargetCount { get; set; }
        public int AwayShotOnTargetCount { get; set; }
        public bool IsPossesionFound { get; set; }
        public bool IsShotFound { get; set; }
        public bool IsShotOnTargetFound { get; set; }
        public bool Is_Corner_FT_Win1 { get; set; }
        public bool Is_Corner_FT_X { get; set; }
        public bool Is_Corner_FT_Win2 { get; set; }
        public bool Is_FT_Win1 { get; set; }
        public bool Is_FT_X { get; set; }
        public bool Is_FT_Win2 { get; set; }
        public bool Is_HT_Win1 { get; set; }
        public bool Is_HT_X { get; set; }
        public bool Is_HT_Win2 { get; set; }
        public bool Is_SH_Win1 { get; set; }
        public bool Is_SH_X { get; set; }
        public bool Is_SH_Win2 { get; set; }

        public int Home_HT_GoalsCount { get; set; }
        public int Home_SH_GoalsCount { get; set; }
        public int Home_FT_GoalsCount { get; set; }
        public int Away_HT_GoalsCount { get; set; }
        public int Away_SH_GoalsCount { get; set; }
        public int Away_FT_GoalsCount { get; set; }
        public int Home_GK_SavesCount { get; set; }
        public int Away_GK_SavesCount { get; set; }
    }
}

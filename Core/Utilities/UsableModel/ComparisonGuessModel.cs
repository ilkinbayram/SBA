namespace Core.Utilities.UsableModel
{
    public class ComparisonGuessModel
    {
        public ComparisonGuessModel()
        {
            Serial = string.Empty;
            Home = string.Empty;
            Away = string.Empty;
        }

        public string Serial { get; set; }
        public string CountryName { get; set; }
        public string Home { get; set; }
        public string Away { get; set; }
        public int BeSide__CountFound { get; set; }
        public int General__CountFound { get; set; }

        public string BeSideAverage__FT_Goals_HomeTeam { get; set; }
        public string BeSideAverage__FT_Goals_AwayTeam { get; set; }
        public string BeSideAverage__HT_Goals_HomeTeam { get; set; }
        public string BeSideAverage__HT_Goals_AwayTeam { get; set; }
        public string BeSideAverage__SH_Goals_HomeTeam { get; set; }
        public string BeSideAverage__SH_Goals_AwayTeam { get; set; }

        public string GeneralAverage__FT_Goals_HomeTeam { get; set; }
        public string GeneralAverage__FT_Goals_AwayTeam { get; set; }
        public string GeneralAverage__HT_Goals_HomeTeam { get; set; }
        public string GeneralAverage__HT_Goals_AwayTeam { get; set; }
        public string GeneralAverage__SH_Goals_HomeTeam { get; set; }
        public string GeneralAverage__SH_Goals_AwayTeam { get; set; }

        public PercentageComplainer BySide__MoreGoalsBetweenTimes { get; set; }

        public PercentageComplainer BySide__HT_Result { get; set; }
        public PercentageComplainer BySide__FT_Result { get; set; }
        public PercentageComplainer BySide__SH_Result { get; set; }

        public PercentageComplainer BySide__HT_FT_Result { get; set; }


        public PercentageComplainer BySide__Total_BetweenGoals { get; set; }

        public PercentageComplainer BySide__FT_GG { get; set; }
        public PercentageComplainer BySide__SH_GG { get; set; }
        public PercentageComplainer BySide__HT_GG { get; set; }


        public PercentageComplainer BySide__FT_15_Over { get; set; }
        public PercentageComplainer BySide__FT_25_Over { get; set; }
        public PercentageComplainer BySide__FT_35_Over { get; set; }

        public PercentageComplainer BySide__HT_05_Over { get; set; }
        public PercentageComplainer BySide__HT_15_Over { get; set; }

        public PercentageComplainer BySide__SH_05_Over { get; set; }
        public PercentageComplainer BySide__SH_15_Over { get; set; }

        public PercentageComplainer BySide__Home_HT_05_Over { get; set; }
        public PercentageComplainer BySide__Home_HT_15_Over { get; set; }

        public PercentageComplainer BySide__Home_SH_05_Over { get; set; }
        public PercentageComplainer BySide__Home_SH_15_Over { get; set; }

        public PercentageComplainer BySide__Home_FT_05_Over { get; set; }
        public PercentageComplainer BySide__Home_FT_15_Over { get; set; }

        public PercentageComplainer BySide__Home_Win_Any_Half { get; set; }


        public PercentageComplainer BySide__Away_HT_05_Over { get; set; }
        public PercentageComplainer BySide__Away_HT_15_Over { get; set; }

        public PercentageComplainer BySide__Away_SH_05_Over { get; set; }
        public PercentageComplainer BySide__Away_SH_15_Over { get; set; }

        public PercentageComplainer BySide__Away_FT_05_Over { get; set; }
        public PercentageComplainer BySide__Away_FT_15_Over { get; set; }

        public PercentageComplainer BySide__Away_Win_Any_Half { get; set; }


        public PercentageComplainer General__MoreGoalsBetweenTimes { get; set; } 

        public PercentageComplainer General__HT_Result { get; set; }
        public PercentageComplainer General__FT_Result { get; set; }
        public PercentageComplainer General__SH_Result { get; set; }

        public PercentageComplainer General__HT_FT_Result { get; set; }

        public PercentageComplainer General__Total_BetweenGoals { get; set; }

        public PercentageComplainer General__FT_GG { get; set; }
        public PercentageComplainer General__SH_GG { get; set; }
        public PercentageComplainer General__HT_GG { get; set; }


        public PercentageComplainer General__FT_15_Over { get; set; }
        public PercentageComplainer General__FT_25_Over { get; set; }
        public PercentageComplainer General__FT_35_Over { get; set; }

        public PercentageComplainer General__HT_05_Over { get; set; }
        public PercentageComplainer General__HT_15_Over { get; set; }

        public PercentageComplainer General__SH_05_Over { get; set; }
        public PercentageComplainer General__SH_15_Over { get; set; }


        public PercentageComplainer General__Home_HT_05_Over { get; set; }
        public PercentageComplainer General__Home_HT_15_Over { get; set; }

        public PercentageComplainer General__Home_SH_05_Over { get; set; }
        public PercentageComplainer General__Home_SH_15_Over { get; set; }

        public PercentageComplainer General__Home_FT_05_Over { get; set; }
        public PercentageComplainer General__Home_FT_15_Over { get; set; }

        public PercentageComplainer General__Home_Win_Any_Half { get; set; }


        public PercentageComplainer General__Away_HT_05_Over { get; set; }
        public PercentageComplainer General__Away_HT_15_Over { get; set; }

        public PercentageComplainer General__Away_SH_05_Over { get; set; }
        public PercentageComplainer General__Away_SH_15_Over { get; set; }

        public PercentageComplainer General__Away_FT_05_Over { get; set; }
        public PercentageComplainer General__Away_FT_15_Over { get; set; }

        public PercentageComplainer General__Away_Win_Any_Half { get; set; }
    }
}

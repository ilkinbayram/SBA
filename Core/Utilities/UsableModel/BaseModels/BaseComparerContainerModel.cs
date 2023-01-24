using Core.Resources.Enums;

namespace Core.Utilities.UsableModel.BaseModels
{
    public class BaseComparerContainerModel
    {
        public BaseComparerContainerModel()
        {
                
        }
        public string Serial { get; set; }
        public string UnchangableHomeTeam { get; set; }
        public string UnchangableAwayTeam { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string CountryName { get; set; }
        public int HT_Goals_HomeTeam { get; set; }
        public int HT_Goals_AwayTeam { get; set; }
        public int FT_Goals_HomeTeam { get; set; }
        public int FT_Goals_AwayTeam { get; set; }
        public int SH_Goals_HomeTeam => Calculate_SH_Goals_Home();
        public int SH_Goals_AwayTeam => Calculate_SH_Goals_Away();

        public int MoreGoalsBetweenTimes
        {
            get
            {
                var totalHtGoals = HT_Goals_HomeTeam + HT_Goals_AwayTeam;
                var totalShGoals = SH_Goals_HomeTeam + SH_Goals_AwayTeam;

                if (totalHtGoals == totalShGoals)
                    return 9;
                else if (totalHtGoals > totalShGoals)
                    return 1;
                else
                    return 2;
            }
        }

        public HalfFullResultEnum HT_FT_Result
        {
            get
            {
                if (HT_Result == 1 && FT_Result == 1)
                    return HalfFullResultEnum.Home_Home;
                else if (HT_Result == 1 && FT_Result == 9)
                    return HalfFullResultEnum.Home_Draw;
                else if (HT_Result == 1 && FT_Result == 2)
                    return HalfFullResultEnum.Home_Away;
                else if (HT_Result == 9 && FT_Result == 1)
                    return HalfFullResultEnum.Draw_Home;
                else if (HT_Result == 9 && FT_Result == 9)
                    return HalfFullResultEnum.Draw_Draw;
                else if (HT_Result == 9 && FT_Result == 2)
                    return HalfFullResultEnum.Draw_Away;
                else if (HT_Result == 2 && FT_Result == 1)
                    return HalfFullResultEnum.Away_Home;
                else if (HT_Result == 2 && FT_Result == 9)
                    return HalfFullResultEnum.Away_Draw;
                else
                    return HalfFullResultEnum.Away_Away;
            }
        }


        public int FT_Result
        {
            get
            {
                if ((FT_Goals_HomeTeam - FT_Goals_AwayTeam) == 0)
                {
                    return 9;
                }
                else if ((FT_Goals_HomeTeam - FT_Goals_AwayTeam) > 0)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            set
            {
                this.FT_Result = value;
            }
        }
        public int HT_Result
        {
            get
            {
                var diff = HT_Goals_HomeTeam - HT_Goals_AwayTeam;

                if (diff == 0)
                {
                    return 9;
                }
                else if (diff > 0)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            set
            {
                this.HT_Result = value;
            }
        }

        public int SH_Result
        {
            get
            {
                var shHome = FT_Goals_HomeTeam - HT_Goals_HomeTeam;
                var shAway = FT_Goals_AwayTeam - HT_Goals_AwayTeam;

                var diff = shHome - shAway;

                if (diff == 0)
                {
                    return 9;
                }
                else if (diff > 0)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            set
            {
                this.SH_Result = value;
            }
        }

        public int Total_BetweenGoals
        {
            get
            {
                var total = FT_Goals_HomeTeam + FT_Goals_AwayTeam;

                if (total < 2)
                {
                    return 10;
                }
                else if (total > 1 && total < 4)
                {
                    return 23;
                }
                else if (total > 3 && total < 6)
                {
                    return 45;
                }
                else
                {
                    return 60;
                }
            }
        }

        public bool FT_GG => FT_Goals_AwayTeam > 0 && FT_Goals_HomeTeam > 0;
        public bool HT_GG => HT_Goals_AwayTeam > 0 && HT_Goals_HomeTeam > 0;
        public bool SH_GG => SH_Goals_AwayTeam > 0 && SH_Goals_HomeTeam > 0;


        public bool Home_HT_05_Over => HT_Goals_HomeTeam > 0;
        public bool Home_HT_15_Over => HT_Goals_HomeTeam > 1;

        public bool Home_SH_05_Over => SH_Goals_HomeTeam > 0;
        public bool Home_SH_15_Over => SH_Goals_HomeTeam > 1;

        public bool Home_FT_05_Over => FT_Goals_HomeTeam > 0;
        public bool Home_FT_15_Over => FT_Goals_HomeTeam > 1;

        public bool Home_Win_Any_Half => HT_Result == 1 || SH_Result == 1;

        public bool Away_HT_05_Over => HT_Goals_AwayTeam > 0;
        public bool Away_HT_15_Over => HT_Goals_AwayTeam > 1;

        public bool Away_SH_05_Over => SH_Goals_AwayTeam > 0;
        public bool Away_SH_15_Over => SH_Goals_AwayTeam > 1;

        public bool Away_FT_05_Over => FT_Goals_AwayTeam > 0;
        public bool Away_FT_15_Over => FT_Goals_AwayTeam > 1;

        public bool Away_Win_Any_Half => HT_Result == 2 || SH_Result == 2;

        public bool HT_05_Over => (HT_Goals_HomeTeam + HT_Goals_AwayTeam) > 0;
        public bool HT_15_Over => (HT_Goals_HomeTeam + HT_Goals_AwayTeam) > 1;

        public bool SH_05_Over => (SH_Goals_HomeTeam + SH_Goals_AwayTeam) > 0;
        public bool SH_15_Over => (SH_Goals_HomeTeam + SH_Goals_AwayTeam) > 1;

        public bool FT_15_Over => (FT_Goals_HomeTeam + FT_Goals_AwayTeam) > 1;
        public bool FT_25_Over => (FT_Goals_HomeTeam + FT_Goals_AwayTeam) > 2;
        public bool FT_35_Over => (FT_Goals_HomeTeam + FT_Goals_AwayTeam) > 3;

        public int Calculate_SH_Goals_Away()
        {
            return FT_Goals_AwayTeam - HT_Goals_AwayTeam;
        }

        public int Calculate_SH_Goals_Home()
        {
            return FT_Goals_HomeTeam - HT_Goals_HomeTeam;
        }
    }
}

using Core.Resources.Enums;

namespace Core.Utilities.UsableModel.BaseModels
{
    public class BaseComparerContainerModel
    {
        private string _serial;
        private string _unchangableHomeTeam;
        private string _unchangableAwayTeam;
        private string _homeTeam;
        private string _awayTeam;
        private string _countryName;
        private string _leagueName;
        private int _hT_Goals_HomeTeam;
        private int _hT_Goals_AwayTeam;
        private int _fT_Goals_HomeTeam;
        private int _fT_Goals_AwayTeam;
        private int _homeCornersCount;
        private int _awayCornersCount;
        private bool _hasCorner;
        private int _homeShutCount;
        private int _awayShutCount;
        private bool _hasShut;
        private int _homeShutOnTargetCount;
        private int _awayShutOnTargetCount;
        private bool _hasShutOnTarget;
        private int _homePossesionCount;
        private int _awayPossesionCount;
        private bool _hasPossesion;

        public BaseComparerContainerModel(string serial, 
                                          string unchangableHomeTeam, 
                                          string unchangableAwayTeam, 
                                          string homeTeam, 
                                          string awayTeam, 
                                          string countryName, 
                                          int hT_Goals_HomeTeam,
                                          int hT_Goals_AwayTeam, 
                                          string leagueName, 
                                          int fT_Goals_HomeTeam, 
                                          int fT_Goals_AwayTeam, 
                                          int homeCornersCount,
                                          int awayCornersCount,
                                          bool hasCorner,
                                          int homeShutCount,
                                          int awayShutCount,
                                          bool hasShut,
                                          int homeShutOnTargetCount,
                                          int awayShutOnTargetCount,
                                          bool hasShutOnTarget,
                                          int homePossesionCount,
                                          int awayPossesionCount,
                                          bool hasPossesion)
        {
            _serial = serial;
            _unchangableHomeTeam = unchangableHomeTeam;
            _unchangableAwayTeam = unchangableAwayTeam;
            _homeTeam = homeTeam;
            _awayTeam = awayTeam;
            _countryName = countryName;
            _hT_Goals_HomeTeam = hT_Goals_HomeTeam;
            _hT_Goals_AwayTeam = hT_Goals_AwayTeam;
            _leagueName = leagueName;
            _fT_Goals_HomeTeam = fT_Goals_HomeTeam;
            _fT_Goals_AwayTeam = fT_Goals_AwayTeam;
            _homeCornersCount = homeCornersCount;
            _awayCornersCount = awayCornersCount;
            _hasCorner = hasCorner;
            _homeShutCount = homeShutCount;
            _awayShutCount = awayShutCount;
            _hasShut = hasShut;
            _homeShutOnTargetCount = homeShutOnTargetCount;
            _awayShutOnTargetCount = awayShutOnTargetCount;
            _hasShutOnTarget = hasShutOnTarget;
            _homePossesionCount = homePossesionCount;
            _awayPossesionCount = awayPossesionCount;
            _hasPossesion = hasPossesion;
        }

        public BaseComparerContainerModel()
        {

        }

        public bool HasCorner
        {
            get => _hasCorner;
            set => _hasCorner = value;
        }

        public bool HasShut
        {
            get => _hasShut;
            set => _hasShut = value;
        }

        public bool HasShutOnTarget
        {
            get => _hasShutOnTarget;
            set => _hasShutOnTarget = value;
        }

        public bool HasPossesion
        {
            get => _hasPossesion;
            set => _hasPossesion = value;
        }

        public string Serial
        {
            get => _serial;
            set => _serial = value;
        }

        public string UnchangableHomeTeam
        {
            get => _unchangableHomeTeam;
            set => _unchangableHomeTeam = value;
        }

        public string UnchangableAwayTeam
        {
            get => _unchangableAwayTeam;
            set => _unchangableAwayTeam = value;
        }
        public string HomeTeam
        {
            get => _homeTeam;
            set => _homeTeam = value;
        }

        public string AwayTeam
        {
            get => _awayTeam;
            set => _awayTeam = value;
        }
        public string CountryName
        {
            get => _countryName;
            set => _countryName = value;
        }
        public string LeagueName
        {
            get => _leagueName;
            set => _leagueName = value;
        }
        public int HT_Goals_HomeTeam
        {
            get => _hT_Goals_HomeTeam;
            set => _hT_Goals_HomeTeam = value;
        }
        public int HT_Goals_AwayTeam
        {
            get => _hT_Goals_AwayTeam;
            set => _hT_Goals_AwayTeam = value;
        }
        public int FT_Goals_HomeTeam
        {
            get => _fT_Goals_HomeTeam;
            set => _fT_Goals_HomeTeam = value;
        }
        public int FT_Goals_AwayTeam
        {
            get => _fT_Goals_AwayTeam;
            set => _fT_Goals_AwayTeam = value;
        }

        public int HomeCornersCount
        {
            get => _homeCornersCount;
            set => _homeCornersCount = value;
        }
        public int AwayCornersCount
        {
            get => _awayCornersCount;
            set => _awayCornersCount = value;
        }

        public int HomeShutCount
        {
            get => _homeShutCount;
            set => _homeShutCount = value;
        }
        public int AwayShutCount
        {
            get => _awayShutCount;
            set => _awayShutCount = value;
        }

        public int HomeShutOnTargetCount
        {
            get => _homeShutOnTargetCount;
            set => _homeShutOnTargetCount = value;
        }
        public int AwayShutOnTargetCount
        {
            get => _awayShutOnTargetCount;
            set => _awayShutOnTargetCount = value;
        }

        public int HomePossesionCount
        {
            get => _homePossesionCount;
            set => _homePossesionCount = value;
        }
        public int AwayPossesionCount
        {
            get => _awayPossesionCount;
            set => _awayPossesionCount = value;
        }

        public int SH_Goals_HomeTeam => Calculate_SH_Goals_Home();
        public int SH_Goals_AwayTeam => Calculate_SH_Goals_Away();

        public bool Corner_Home_3_5_Over => HomeCornersCount > 3;
        public bool Corner_Home_4_5_Over => HomeCornersCount > 4;
        public bool Corner_Home_5_5_Over => HomeCornersCount > 5;
        public bool Corner_Away_3_5_Over => AwayCornersCount > 3;
        public bool Corner_Away_4_5_Over => AwayCornersCount > 4;
        public bool Corner_Away_5_5_Over => AwayCornersCount > 5;
        public bool Corner_7_5_Over => (HomeCornersCount + AwayCornersCount) > 7;
        public bool Corner_8_5_Over => (HomeCornersCount + AwayCornersCount) > 8;
        public bool Corner_9_5_Over => (HomeCornersCount + AwayCornersCount) > 9;
        public bool Is_Corner_FT_Win1 => HomeCornersCount > AwayCornersCount;
        public bool Is_Corner_FT_X => HomeCornersCount == AwayCornersCount;
        public bool Is_Corner_FT_Win2 => HomeCornersCount < AwayCornersCount;

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

        public bool Is_FT_Win1 => FT_Result == 1;
        public bool Is_FT_X => FT_Result == 9;
        public bool Is_FT_Win2 => FT_Result == 2;

        public bool Is_HT_Win1 => HT_Result == 1;
        public bool Is_HT_X => HT_Result == 9;
        public bool Is_HT_Win2 => HT_Result == 2;

        public bool Is_SH_Win1 => SH_Result == 1;
        public bool Is_SH_X => SH_Result == 9;
        public bool Is_SH_Win2 => SH_Result == 2;

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

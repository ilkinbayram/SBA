using Core.Resources.Enums;
using Core.Utilities.Helpers;

namespace Core.Utilities.UsableModel
{
    public class JobAnalyseModel
    {
        public JobAnalyseModel()
        {
        }

        public string Serial => this.ComparisonInfoContainer.Serial;
        public TeamPercentageProfiler TeamPercentageProfiler { get; set; }
        public ComparisonGuessContainer ComparisonInfoContainer { get; set; }
        public ComparisonGuessContainer ComparisonOnlyDB { get; set; }
        public FormPerformanceGuessContainer HomeTeam_FormPerformanceGuessContainer { get; set; }
        public FormPerformanceGuessContainer AwayTeam_FormPerformanceGuessContainer { get; set; }
        public StandingInfoModel? StandingInfoModel { get; set; }

        #region ShortProps

        public bool Is_GG
        {
            get
            {
                return IsGoalGoal();
            }
        }

        public bool Is_FT_25_Over
        {
            get
            {
                return IsFt25_Over();
            }
        }

        public bool Is_FT_25_Under
        {
            get
            {
                return IsFt25_Under();
            }
        }

        public bool Is_HT_15_Over
        {
            get
            {
                return IsHt15_Over();
            }
        }


        public bool IsGoalGoal()
        {
            try
            {
                if (StandingInfoModel == null) return false;

                decimal homeTeamGoalAverage = (HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_HomeTeam + HomeTeam_FormPerformanceGuessContainer.General.Average_FT_Goals_HomeTeam + HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_AwayTeam + HomeTeam_FormPerformanceGuessContainer.General.Average_FT_Goals_AwayTeam) / 4;

                decimal awayTeamGoalAverage = (AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_HomeTeam + AwayTeam_FormPerformanceGuessContainer.General.Average_FT_Goals_HomeTeam + AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_AwayTeam + AwayTeam_FormPerformanceGuessContainer.General.Average_FT_Goals_AwayTeam) / 4;

                decimal indc = StandingInfoModel.UpTeam.Indicator / StandingInfoModel.DownTeam.Indicator;

                bool indRes = indc > 1 && indc < 2;

                decimal comparisonHomeTeamGoalAverage = (ComparisonInfoContainer.HomeAway.Average_FT_Goals_HomeTeam + ComparisonInfoContainer.HomeAway.Average_FT_Goals_AwayTeam + ComparisonInfoContainer.General.Average_FT_Goals_HomeTeam + ComparisonInfoContainer.General.Average_FT_Goals_AwayTeam) / 4;


                bool averageBoolean = homeTeamGoalAverage >= (decimal)1.40 && awayTeamGoalAverage >= (decimal)1.40 && indRes && comparisonHomeTeamGoalAverage >= (decimal)1.4;

                if (!averageBoolean) return false;

                bool isFt15Over =
                    ComparisonInfoContainer.HomeAway.FT_15_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard && ComparisonInfoContainer.HomeAway.FT_15_Over.FeatureName.ToLower() == "true" &&
                    HomeTeam_FormPerformanceGuessContainer.HomeAway.FT_15_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard && HomeTeam_FormPerformanceGuessContainer.HomeAway.FT_15_Over.FeatureName.ToLower() == "true" &&
                    AwayTeam_FormPerformanceGuessContainer.HomeAway.FT_15_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard && AwayTeam_FormPerformanceGuessContainer.HomeAway.FT_15_Over.FeatureName.ToLower() == "true";
                bool isHomeFt05Over =
                    ComparisonInfoContainer.HomeAway.Home_FT_05_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard && ComparisonInfoContainer.HomeAway.Home_FT_05_Over.FeatureName.ToLower() == "true" &&
                    HomeTeam_FormPerformanceGuessContainer.HomeAway.Home_FT_05_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard &&
                    HomeTeam_FormPerformanceGuessContainer.HomeAway.Home_FT_05_Over.FeatureName.ToLower() == "true" &&
                    AwayTeam_FormPerformanceGuessContainer.HomeAway.Home_FT_05_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard &&
                    AwayTeam_FormPerformanceGuessContainer.HomeAway.Home_FT_05_Over.FeatureName.ToLower() == "true";
                bool isAwayFt05Over =
                    ComparisonInfoContainer.HomeAway.Away_FT_05_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard && ComparisonInfoContainer.HomeAway.Away_FT_05_Over.FeatureName.ToLower() == "true" &&
                    HomeTeam_FormPerformanceGuessContainer.HomeAway.Away_FT_05_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard &&
                    HomeTeam_FormPerformanceGuessContainer.HomeAway.Away_FT_05_Over.FeatureName.ToLower() == "true" &&
                    AwayTeam_FormPerformanceGuessContainer.HomeAway.Away_FT_05_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard &&
                    AwayTeam_FormPerformanceGuessContainer.HomeAway.Away_FT_05_Over.FeatureName.ToLower() == "true";

                return isFt15Over && isHomeFt05Over && isAwayFt05Over;
            }
            catch (System.Exception)
            {
                return false;
            }
        }



        public bool IsHt15_Over()
        {
            try
            {
                if (StandingInfoModel == null) return false;

                decimal justHomeGoalAvg = (HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_HT_Goals_HomeTeam + HomeTeam_FormPerformanceGuessContainer.General.Average_HT_Goals_HomeTeam + ComparisonInfoContainer.HomeAway.Average_HT_Goals_HomeTeam + ComparisonInfoContainer.General.Average_HT_Goals_HomeTeam) / 4;

                decimal justHomePassedAvg = (HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_HT_Goals_AwayTeam + HomeTeam_FormPerformanceGuessContainer.General.Average_HT_Goals_AwayTeam + ComparisonInfoContainer.HomeAway.Average_HT_Goals_AwayTeam + ComparisonInfoContainer.General.Average_HT_Goals_AwayTeam) / 4;

                decimal justAwayGoalAvg = (AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_HT_Goals_AwayTeam + AwayTeam_FormPerformanceGuessContainer.General.Average_HT_Goals_AwayTeam + ComparisonInfoContainer.HomeAway.Average_HT_Goals_AwayTeam + ComparisonInfoContainer.General.Average_HT_Goals_AwayTeam) / 4;

                decimal justAwayPassedAvg = (AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_HT_Goals_HomeTeam + AwayTeam_FormPerformanceGuessContainer.General.Average_HT_Goals_HomeTeam + ComparisonInfoContainer.HomeAway.Average_HT_Goals_HomeTeam + ComparisonInfoContainer.General.Average_HT_Goals_HomeTeam) / 4;



                decimal homeTeamGoalAverage = (HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_HT_Goals_HomeTeam + HomeTeam_FormPerformanceGuessContainer.General.Average_HT_Goals_HomeTeam + HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_HT_Goals_AwayTeam + HomeTeam_FormPerformanceGuessContainer.General.Average_HT_Goals_AwayTeam) / 4;

                decimal awayTeamGoalAverage = (AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_HT_Goals_HomeTeam + AwayTeam_FormPerformanceGuessContainer.General.Average_HT_Goals_HomeTeam + AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_HT_Goals_AwayTeam + AwayTeam_FormPerformanceGuessContainer.General.Average_HT_Goals_AwayTeam) / 4;

                decimal comparisonHomeTeamGoalAverage = (ComparisonInfoContainer.HomeAway.Average_HT_Goals_HomeTeam + ComparisonInfoContainer.HomeAway.Average_HT_Goals_AwayTeam + ComparisonInfoContainer.General.Average_HT_Goals_HomeTeam + ComparisonInfoContainer.General.Average_HT_Goals_AwayTeam) / 4;

                decimal indc = StandingInfoModel.UpTeam.Indicator / StandingInfoModel.DownTeam.Indicator;

                bool indRes = indc > 1 && indc < 2;

                bool averageBoolean = homeTeamGoalAverage >= (decimal)0.9 && awayTeamGoalAverage >= (decimal)0.9 && indRes && comparisonHomeTeamGoalAverage >= (decimal)0.9;

                if (!averageBoolean) return false;

                if (!indRes)
                {
                    if (HomeTeam_FormPerformanceGuessContainer.Home == StandingInfoModel.UpTeam.TeamName)
                    {
                        bool homeStrongBoolean = justHomeGoalAvg > (decimal)1.5 && justAwayPassedAvg > (decimal)1.5;
                        if (!homeStrongBoolean) return false;
                    }
                    else
                    {
                        bool awayStrongBoolean = justAwayGoalAvg > (decimal)1.5 && justHomePassedAvg > (decimal)1.5;
                        if (!awayStrongBoolean) return false;
                    }
                }

                bool isHomeHt05Over =
                    ComparisonInfoContainer.HomeAway.Home_HT_05_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard && ComparisonInfoContainer.HomeAway.Home_HT_05_Over.FeatureName.ToLower() == "true" &&
                    HomeTeam_FormPerformanceGuessContainer.HomeAway.Home_HT_05_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard && HomeTeam_FormPerformanceGuessContainer.HomeAway.Home_HT_05_Over.FeatureName.ToLower() == "true" &&
                    AwayTeam_FormPerformanceGuessContainer.HomeAway.Home_HT_05_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard && AwayTeam_FormPerformanceGuessContainer.HomeAway.Home_HT_05_Over.FeatureName.ToLower() == "true";
                bool isAwayHt05Over =
                    ComparisonInfoContainer.HomeAway.Away_HT_05_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard && ComparisonInfoContainer.HomeAway.Away_HT_05_Over.FeatureName.ToLower() == "true" &&
                    HomeTeam_FormPerformanceGuessContainer.HomeAway.Away_HT_05_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard && HomeTeam_FormPerformanceGuessContainer.HomeAway.Away_HT_05_Over.FeatureName.ToLower() == "true" &&
                    AwayTeam_FormPerformanceGuessContainer.HomeAway.Away_HT_05_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard && AwayTeam_FormPerformanceGuessContainer.HomeAway.Away_HT_05_Over.FeatureName.ToLower() == "true";
                bool isHomeHt15Over =
                    ComparisonInfoContainer.HomeAway.Home_HT_15_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard && ComparisonInfoContainer.HomeAway.Home_HT_15_Over.FeatureName.ToLower() == "true" &&
                    HomeTeam_FormPerformanceGuessContainer.HomeAway.Home_HT_15_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard &&
                    HomeTeam_FormPerformanceGuessContainer.HomeAway.Home_HT_15_Over.FeatureName.ToLower() == "true" &&
                    AwayTeam_FormPerformanceGuessContainer.HomeAway.Home_HT_15_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard &&
                    AwayTeam_FormPerformanceGuessContainer.HomeAway.Home_HT_15_Over.FeatureName.ToLower() == "true";
                bool isAwayHt15Over =
                    ComparisonInfoContainer.HomeAway.Away_HT_15_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard && ComparisonInfoContainer.HomeAway.Away_HT_15_Over.FeatureName.ToLower() == "true" &&
                    HomeTeam_FormPerformanceGuessContainer.HomeAway.Away_HT_15_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard &&
                    HomeTeam_FormPerformanceGuessContainer.HomeAway.Away_HT_15_Over.FeatureName.ToLower() == "true" &&
                    AwayTeam_FormPerformanceGuessContainer.HomeAway.Away_HT_15_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard &&
                    AwayTeam_FormPerformanceGuessContainer.HomeAway.Away_HT_15_Over.FeatureName.ToLower() == "true";

                bool anyTeam15Over = isHomeHt15Over || isAwayHt15Over;

                if (anyTeam15Over) return true;

                return isHomeHt05Over && isAwayHt05Over;

            }
            catch (System.Exception)
            {
                return false;
            }
        }




        public bool IsFt25_Over()
        {
            try
            {
                if (StandingInfoModel == null) return false;

                decimal justHomeGoalAvg = (HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_HomeTeam + HomeTeam_FormPerformanceGuessContainer.General.Average_FT_Goals_HomeTeam + ComparisonInfoContainer.HomeAway.Average_FT_Goals_HomeTeam + ComparisonInfoContainer.General.Average_FT_Goals_HomeTeam) / 4;

                decimal justHomePassedAvg = (HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_AwayTeam + HomeTeam_FormPerformanceGuessContainer.General.Average_FT_Goals_AwayTeam + ComparisonInfoContainer.HomeAway.Average_FT_Goals_AwayTeam + ComparisonInfoContainer.General.Average_FT_Goals_AwayTeam) / 4;

                decimal justAwayGoalAvg = (AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_AwayTeam + AwayTeam_FormPerformanceGuessContainer.General.Average_FT_Goals_AwayTeam + ComparisonInfoContainer.HomeAway.Average_FT_Goals_AwayTeam + ComparisonInfoContainer.General.Average_FT_Goals_AwayTeam) / 4;

                decimal justAwayPassedAvg = (AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_HomeTeam + AwayTeam_FormPerformanceGuessContainer.General.Average_FT_Goals_HomeTeam + ComparisonInfoContainer.HomeAway.Average_FT_Goals_HomeTeam + ComparisonInfoContainer.General.Average_FT_Goals_HomeTeam) / 4;



                decimal homeTeamGoalAverage = (HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_HomeTeam + HomeTeam_FormPerformanceGuessContainer.General.Average_FT_Goals_HomeTeam + HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_AwayTeam + HomeTeam_FormPerformanceGuessContainer.General.Average_FT_Goals_AwayTeam) / 4;

                decimal awayTeamGoalAverage = (AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_HomeTeam + AwayTeam_FormPerformanceGuessContainer.General.Average_FT_Goals_HomeTeam + AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_AwayTeam + AwayTeam_FormPerformanceGuessContainer.General.Average_FT_Goals_AwayTeam) / 4;

                decimal comparisonHomeTeamGoalAverage = (ComparisonInfoContainer.HomeAway.Average_FT_Goals_HomeTeam + ComparisonInfoContainer.HomeAway.Average_FT_Goals_AwayTeam + ComparisonInfoContainer.General.Average_FT_Goals_HomeTeam + ComparisonInfoContainer.General.Average_FT_Goals_AwayTeam) / 4;

                decimal indc = StandingInfoModel.UpTeam.Indicator / StandingInfoModel.DownTeam.Indicator;

                bool indRes = indc > 1 && indc < 2;

                bool averageBoolean = homeTeamGoalAverage >= (decimal)1.50 && awayTeamGoalAverage >= (decimal)1.50 && indRes && comparisonHomeTeamGoalAverage >= (decimal)1.50;

                if (!averageBoolean) return false;

                if (!indRes)
                {
                    if (HomeTeam_FormPerformanceGuessContainer.Home == StandingInfoModel.UpTeam.TeamName)
                    {
                        bool homeStrongBoolean = justHomeGoalAvg > (decimal)2.5 && justAwayPassedAvg > (decimal)2.5;
                        if (!homeStrongBoolean) return false;
                    }
                    else
                    {
                        bool awayStrongBoolean = justAwayGoalAvg > (decimal)2.5 && justHomePassedAvg > (decimal)2.5;
                        if (!awayStrongBoolean) return false;
                    }
                }

                bool isFt25Over =
                    ComparisonInfoContainer.HomeAway.FT_25_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard && ComparisonInfoContainer.HomeAway.FT_25_Over.FeatureName.ToLower() == "true" &&
                    HomeTeam_FormPerformanceGuessContainer.HomeAway.FT_25_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard && HomeTeam_FormPerformanceGuessContainer.HomeAway.FT_25_Over.FeatureName.ToLower() == "true" &&
                    AwayTeam_FormPerformanceGuessContainer.HomeAway.FT_25_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard && AwayTeam_FormPerformanceGuessContainer.HomeAway.FT_25_Over.FeatureName.ToLower() == "true";
                bool isHt05Over =
                    ComparisonInfoContainer.HomeAway.HT_05_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard && ComparisonInfoContainer.HomeAway.HT_05_Over.FeatureName.ToLower() == "true" &&
                    HomeTeam_FormPerformanceGuessContainer.HomeAway.HT_05_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard && HomeTeam_FormPerformanceGuessContainer.HomeAway.HT_05_Over.FeatureName.ToLower() == "true" &&
                    AwayTeam_FormPerformanceGuessContainer.HomeAway.HT_05_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard && AwayTeam_FormPerformanceGuessContainer.HomeAway.HT_05_Over.FeatureName.ToLower() == "true";
                bool isHomeFt05Over =
                    ComparisonInfoContainer.HomeAway.Home_FT_05_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard && ComparisonInfoContainer.HomeAway.Home_FT_05_Over.FeatureName.ToLower() == "true" &&
                    HomeTeam_FormPerformanceGuessContainer.HomeAway.Home_FT_05_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard &&
                    HomeTeam_FormPerformanceGuessContainer.HomeAway.Home_FT_05_Over.FeatureName.ToLower() == "true" &&
                    AwayTeam_FormPerformanceGuessContainer.HomeAway.Home_FT_05_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard &&
                    AwayTeam_FormPerformanceGuessContainer.HomeAway.Home_FT_05_Over.FeatureName.ToLower() == "true";
                bool isAwayFt05Over =
                    ComparisonInfoContainer.HomeAway.Away_FT_05_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard && ComparisonInfoContainer.HomeAway.Away_FT_05_Over.FeatureName.ToLower() == "true" &&
                    HomeTeam_FormPerformanceGuessContainer.HomeAway.Away_FT_05_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard &&
                    HomeTeam_FormPerformanceGuessContainer.HomeAway.Away_FT_05_Over.FeatureName.ToLower() == "true" &&
                    AwayTeam_FormPerformanceGuessContainer.HomeAway.Away_FT_05_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard &&
                    AwayTeam_FormPerformanceGuessContainer.HomeAway.Away_FT_05_Over.FeatureName.ToLower() == "true";

                return isFt25Over && isHt05Over && isHomeFt05Over && isAwayFt05Over;

            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public bool IsFt25_Under()
        {
            try
            {
                if (StandingInfoModel == null) return false;

                decimal justHomeGoalAvg = (HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_HomeTeam + HomeTeam_FormPerformanceGuessContainer.General.Average_FT_Goals_HomeTeam + ComparisonInfoContainer.HomeAway.Average_FT_Goals_HomeTeam + ComparisonInfoContainer.General.Average_FT_Goals_HomeTeam) / 4;

                decimal justHomePassedAvg = (HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_AwayTeam + HomeTeam_FormPerformanceGuessContainer.General.Average_FT_Goals_AwayTeam + ComparisonInfoContainer.HomeAway.Average_FT_Goals_AwayTeam + ComparisonInfoContainer.General.Average_FT_Goals_AwayTeam) / 4;

                decimal justAwayGoalAvg = (AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_AwayTeam + AwayTeam_FormPerformanceGuessContainer.General.Average_FT_Goals_AwayTeam + ComparisonInfoContainer.HomeAway.Average_FT_Goals_AwayTeam + ComparisonInfoContainer.General.Average_FT_Goals_AwayTeam) / 4;

                decimal justAwayPassedAvg = (AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_HomeTeam + AwayTeam_FormPerformanceGuessContainer.General.Average_FT_Goals_HomeTeam + ComparisonInfoContainer.HomeAway.Average_FT_Goals_HomeTeam + ComparisonInfoContainer.General.Average_FT_Goals_HomeTeam) / 4;



                decimal homeTeamGoalAverage = (HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_HomeTeam + HomeTeam_FormPerformanceGuessContainer.General.Average_FT_Goals_HomeTeam + HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_AwayTeam + HomeTeam_FormPerformanceGuessContainer.General.Average_FT_Goals_AwayTeam) / 4;

                decimal awayTeamGoalAverage = (AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_HomeTeam + AwayTeam_FormPerformanceGuessContainer.General.Average_FT_Goals_HomeTeam + AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_AwayTeam + AwayTeam_FormPerformanceGuessContainer.General.Average_FT_Goals_AwayTeam) / 4;

                decimal comparisonHomeTeamGoalAverage = (ComparisonInfoContainer.HomeAway.Average_FT_Goals_HomeTeam + ComparisonInfoContainer.HomeAway.Average_FT_Goals_AwayTeam + ComparisonInfoContainer.General.Average_FT_Goals_HomeTeam + ComparisonInfoContainer.General.Average_FT_Goals_AwayTeam) / 4;

                decimal indc = StandingInfoModel.UpTeam.Indicator / StandingInfoModel.DownTeam.Indicator;

                bool indRes = indc > 1 && indc < 2;

                bool averageBoolean = homeTeamGoalAverage <= (decimal)0.74 && awayTeamGoalAverage <= (decimal)0.74 && comparisonHomeTeamGoalAverage <= (decimal)0.74;

                if (!averageBoolean) return false;

                if (!indRes)
                {
                    if (HomeTeam_FormPerformanceGuessContainer.Home == StandingInfoModel.UpTeam.TeamName)
                    {
                        bool homeStrongBoolean = justHomeGoalAvg <= (decimal)1.4 && justAwayPassedAvg < (decimal)1.4;
                        if (!homeStrongBoolean) return false;
                    }
                    else
                    {
                        bool awayStrongBoolean = justAwayGoalAvg <= (decimal)1.4 && justHomePassedAvg < (decimal)1.4;
                        if (!awayStrongBoolean) return false;
                    }
                }

                bool isFt25Over =
                    ComparisonInfoContainer.HomeAway.FT_25_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard && ComparisonInfoContainer.HomeAway.FT_25_Over.FeatureName.ToLower() == "false" &&
                    HomeTeam_FormPerformanceGuessContainer.HomeAway.FT_25_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard && HomeTeam_FormPerformanceGuessContainer.HomeAway.FT_25_Over.FeatureName.ToLower() == "false" &&
                    AwayTeam_FormPerformanceGuessContainer.HomeAway.FT_25_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard && AwayTeam_FormPerformanceGuessContainer.HomeAway.FT_25_Over.FeatureName.ToLower() == "false";
                bool isHt15Over =
                    ComparisonInfoContainer.HomeAway.HT_15_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard && ComparisonInfoContainer.HomeAway.HT_15_Over.FeatureName.ToLower() == "false" &&
                    HomeTeam_FormPerformanceGuessContainer.HomeAway.HT_15_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard && HomeTeam_FormPerformanceGuessContainer.HomeAway.HT_15_Over.FeatureName.ToLower() == "false" &&
                    AwayTeam_FormPerformanceGuessContainer.HomeAway.HT_15_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard && AwayTeam_FormPerformanceGuessContainer.HomeAway.HT_15_Over.FeatureName.ToLower() == "false";
                bool isSh15Over =
                    ComparisonInfoContainer.HomeAway.SH_15_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard && ComparisonInfoContainer.HomeAway.SH_15_Over.FeatureName.ToLower() == "false" &&
                    HomeTeam_FormPerformanceGuessContainer.HomeAway.SH_15_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard && HomeTeam_FormPerformanceGuessContainer.HomeAway.SH_15_Over.FeatureName.ToLower() == "false" &&
                    AwayTeam_FormPerformanceGuessContainer.HomeAway.SH_15_Over.Percentage > (int)StaticPercentageDefinerEnum.TwoWayStandard && AwayTeam_FormPerformanceGuessContainer.HomeAway.SH_15_Over.FeatureName.ToLower() == "false";

                return isFt25Over && isHt15Over && isSh15Over;


            }
            catch (System.Exception)
            {
                return false;
            }
        }

        #endregion



        public AveragePercentageContainer AverageProfiler
        {
            get
            {
                //return new AveragePercentageContainer();
                try
                {
                    if (this.ComparisonInfoContainer == null || this.ComparisonInfoContainer.General.CountFound < 4)
                    {
                        return null;
                    }

                    if (this.HomeTeam_FormPerformanceGuessContainer == null || this.HomeTeam_FormPerformanceGuessContainer.General.CountFound < 4)
                    {
                        return null;
                    }

                    if (this.AwayTeam_FormPerformanceGuessContainer == null || this.AwayTeam_FormPerformanceGuessContainer.General.CountFound < 4)
                    {
                        return null;
                    }

                    //if (this.StandingInfoModel == null || this.StandingInfoModel.UpTeam == null || this.StandingInfoModel.DownTeam == null)
                    //{
                    //    return null;
                    //}

                    // TODO : Look
                    return new AveragePercentageContainer
                    {
                        Average_HT_Goals_HomeTeam =
                                Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.Average_HT_Goals_HomeTeam,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.Average_HT_Goals_HomeTeam),
                        Average_SH_Goals_HomeTeam =
                                Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.Average_SH_Goals_HomeTeam,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.Average_SH_Goals_HomeTeam),
                        Average_FT_Goals_HomeTeam =
                                Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.Average_FT_Goals_HomeTeam,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.Average_FT_Goals_HomeTeam),
                        Average_FT_Corners_HomeTeam = Calculator.GetSpecialBetAverage(
                                    this.ComparisonOnlyDB, "General|Average_FT_Corners_HomeTeam",
                                    this.HomeTeam_FormPerformanceGuessContainer.General.Average_FT_Corners_HomeTeam),
                        Home_HT_05_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.Home_HT_05_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.Home_HT_05_Over),
                        Home_HT_15_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.Home_HT_15_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.Home_HT_15_Over),
                        Home_SH_05_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.Home_SH_05_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.Home_SH_05_Over),
                        Home_SH_15_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.Home_SH_15_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.Home_SH_15_Over),
                        Home_FT_05_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.Home_FT_05_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.Home_FT_05_Over),
                        Home_FT_15_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.Home_FT_15_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.Home_FT_15_Over),
                        Corner_Home_3_5_Over = Calculator.GetSpecialBetAverage(
                                    this.ComparisonOnlyDB, "General|Corner_Home_3_5_Over",
                                    this.HomeTeam_FormPerformanceGuessContainer.General.Corner_Home_3_5_Over),
                        Corner_Home_4_5_Over = Calculator.GetSpecialBetAverage(
                                    this.ComparisonOnlyDB, "General|Corner_Home_4_5_Over", 
                                    this.HomeTeam_FormPerformanceGuessContainer.General.Corner_Home_4_5_Over),
                        Corner_Home_5_5_Over = Calculator.GetSpecialBetAverage(
                                    this.ComparisonOnlyDB, "General|Corner_Home_5_5_Over",
                                    this.HomeTeam_FormPerformanceGuessContainer.General.Corner_Home_5_5_Over),
                        Home_Win_Any_Half = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.Home_Win_Any_Half,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.Home_Win_Any_Half),

                        Average_HT_Goals_AwayTeam =
                                Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.Average_HT_Goals_AwayTeam,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.Average_HT_Goals_AwayTeam),
                        Average_SH_Goals_AwayTeam =
                                Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.Average_SH_Goals_AwayTeam,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.Average_SH_Goals_AwayTeam),
                        Average_FT_Goals_AwayTeam =
                                Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.Average_FT_Goals_AwayTeam,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.Average_FT_Goals_AwayTeam),
                        Average_FT_Corners_AwayTeam = Calculator.GetSpecialBetAverage(
                                    this.ComparisonOnlyDB, "General|Average_FT_Corners_AwayTeam",
                                    this.HomeTeam_FormPerformanceGuessContainer.General.Average_FT_Corners_AwayTeam),
                        Away_HT_05_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.Away_HT_05_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.Away_HT_05_Over),
                        Away_HT_15_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.Away_HT_15_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.Away_HT_15_Over),
                        Away_SH_05_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.Away_SH_05_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.Away_SH_05_Over),
                        Away_SH_15_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.Away_SH_15_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.Away_SH_15_Over),
                        Away_FT_05_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.Away_FT_05_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.Away_FT_05_Over),
                        Away_FT_15_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.Away_FT_15_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.Away_FT_15_Over),
                        Corner_Away_3_5_Over = Calculator.GetSpecialBetAverage(
                                    this.ComparisonOnlyDB, "General|Corner_Away_3_5_Over",
                                    this.AwayTeam_FormPerformanceGuessContainer.General.Corner_Away_3_5_Over),
                        Corner_Away_4_5_Over = Calculator.GetSpecialBetAverage(
                                    this.ComparisonOnlyDB, "General|Corner_Away_4_5_Over",
                                    this.AwayTeam_FormPerformanceGuessContainer.General.Corner_Away_4_5_Over),
                        Corner_Away_5_5_Over = Calculator.GetSpecialBetAverage(
                                    this.ComparisonOnlyDB, "General|Corner_Away_5_5_Over",
                                    this.AwayTeam_FormPerformanceGuessContainer.General.Corner_Away_5_5_Over),
                        Away_Win_Any_Half = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.Away_Win_Any_Half,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.Away_Win_Any_Half),

                        HT_05_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.HT_05_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.HT_05_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.HT_05_Over),
                        HT_15_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.HT_15_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.HT_15_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.HT_15_Over),
                        SH_05_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.SH_05_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.SH_05_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.SH_05_Over),
                        SH_15_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.SH_15_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.SH_15_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.SH_15_Over),

                        FT_15_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.FT_15_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.FT_15_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.FT_15_Over),
                        FT_25_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.FT_25_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.FT_25_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.FT_25_Over),
                        FT_35_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.FT_35_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.FT_35_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.FT_35_Over),

                        Corner_7_5_Over = Calculator.GetSpecialBetAverage(
                                    this.ComparisonOnlyDB, "General|Corner_7_5_Over",
                                    this.HomeTeam_FormPerformanceGuessContainer.General.Corner_7_5_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.Corner_7_5_Over),
                        Corner_8_5_Over = Calculator.GetSpecialBetAverage(
                                    this.ComparisonOnlyDB, "General|Corner_8_5_Over",
                                    this.HomeTeam_FormPerformanceGuessContainer.General.Corner_8_5_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.Corner_8_5_Over),
                        Corner_9_5_Over = Calculator.GetSpecialBetAverage(
                                    this.ComparisonOnlyDB, "General|Corner_9_5_Over",
                                    this.HomeTeam_FormPerformanceGuessContainer.General.Corner_9_5_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.Corner_9_5_Over),

                        HT_Result = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.HT_Result,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.HT_Result,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.HT_Result),
                        SH_Result = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.SH_Result,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.SH_Result,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.SH_Result),
                        FT_Result = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.FT_Result,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.FT_Result,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.FT_Result),

                        Is_FT_Win1 = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.Is_FT_Win1,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.Is_FT_Win1),
                        Is_FT_Win2 = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.Is_FT_Win2,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.Is_FT_Win2),
                        Is_FT_X = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.Is_FT_X,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.Is_FT_X,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.Is_FT_X),

                        Is_HT_Win1 = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.Is_HT_Win1,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.Is_HT_Win1),
                        Is_HT_Win2 = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.Is_HT_Win2,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.Is_HT_Win2),
                        Is_HT_X = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.Is_HT_X,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.Is_HT_X,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.Is_HT_X),

                        Is_SH_Win1 = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.Is_SH_Win1,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.Is_SH_Win1),
                        Is_SH_Win2 = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.Is_SH_Win2,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.Is_SH_Win2),
                        Is_SH_X = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.Is_SH_X,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.Is_SH_X,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.Is_SH_X),


                        Is_Corner_FT_Win1 = Calculator.GetSpecialBetAverage(
                                    this.ComparisonOnlyDB, "General|Is_Corner_FT_Win1",
                                    this.HomeTeam_FormPerformanceGuessContainer.General.Is_Corner_FT_Win1),
                        Is_Corner_FT_Win2 = Calculator.GetSpecialBetAverage(
                                    this.ComparisonOnlyDB, "General|Is_Corner_FT_Win2",
                                    this.AwayTeam_FormPerformanceGuessContainer.General.Is_Corner_FT_Win2),
                        Is_Corner_FT_X = Calculator.GetSpecialBetAverage(
                                    this.ComparisonOnlyDB, "General|Is_Corner_FT_X",
                                    this.HomeTeam_FormPerformanceGuessContainer.General.Is_Corner_FT_X,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.Is_Corner_FT_X),

                        HT_GG = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.HT_GG,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.HT_GG,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.HT_GG),
                        SH_GG = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.SH_GG,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.SH_GG,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.SH_GG),
                        FT_GG = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.FT_GG, this.HomeTeam_FormPerformanceGuessContainer.General.FT_GG,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.FT_GG),
                        HT_FT_Result = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.HT_FT_Result, this.HomeTeam_FormPerformanceGuessContainer.General.HT_FT_Result,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.HT_FT_Result),
                        MoreGoalsBetweenTimes = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.MoreGoalsBetweenTimes, this.HomeTeam_FormPerformanceGuessContainer.General.MoreGoalsBetweenTimes,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.MoreGoalsBetweenTimes),
                        Total_BetweenGoals = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.Total_BetweenGoals, this.HomeTeam_FormPerformanceGuessContainer.General.Total_BetweenGoals,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.Total_BetweenGoals)
                    };
                }
                catch (System.Exception)
                {

                    return null;
                }

            }
        }



        public AveragePercentageContainer AverageProfilerHomeAway
        {
            get
            {
                //return new AveragePercentageContainer();
                try
                {
                    if (this.ComparisonInfoContainer == null || this.ComparisonInfoContainer.General.CountFound < 4)
                    {
                        return null;
                    }

                    if (this.HomeTeam_FormPerformanceGuessContainer == null || this.HomeTeam_FormPerformanceGuessContainer.General.CountFound < 4)
                    {
                        return null;
                    }

                    if (this.AwayTeam_FormPerformanceGuessContainer == null || this.AwayTeam_FormPerformanceGuessContainer.General.CountFound < 4)
                    {
                        return null;
                    }

                    //if (this.StandingInfoModel == null || this.StandingInfoModel.UpTeam == null || this.StandingInfoModel.DownTeam == null)
                    //{
                    //    return null;
                    //}


                    return new AveragePercentageContainer
                    {
                        Average_HT_Goals_HomeTeam =
                                Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.Average_HT_Goals_HomeTeam,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_HT_Goals_HomeTeam),
                        Average_SH_Goals_HomeTeam =
                                Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.Average_SH_Goals_HomeTeam,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_SH_Goals_HomeTeam),
                        Average_FT_Goals_HomeTeam =
                                Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.Average_FT_Goals_HomeTeam,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_HomeTeam),
                        Average_FT_Corners_HomeTeam =
                                Calculator.GetSpecialBetAverage(
                                    this.ComparisonOnlyDB, "HomeAway|Average_FT_Corners_HomeTeam",
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Corners_HomeTeam),
                        Home_HT_05_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.Home_HT_05_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Home_HT_05_Over),
                        Home_HT_15_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.Home_HT_15_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Home_HT_15_Over),
                        Home_SH_05_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.Home_SH_05_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Home_SH_05_Over),
                        Home_SH_15_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.Home_SH_15_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Home_SH_15_Over),
                        Home_FT_05_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.Home_FT_05_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Home_FT_05_Over),
                        Home_FT_15_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.Home_FT_15_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Home_FT_15_Over),
                        Corner_Home_3_5_Over = Calculator.GetSpecialBetAverage(
                                    this.ComparisonOnlyDB, "HomeAway|Corner_Home_3_5_Over",
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Corner_Home_3_5_Over),
                        Corner_Home_4_5_Over = Calculator.GetSpecialBetAverage(
                                    this.ComparisonOnlyDB, "HomeAway|Corner_Home_4_5_Over",
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Corner_Home_4_5_Over),
                        Corner_Home_5_5_Over = Calculator.GetSpecialBetAverage(
                                    this.ComparisonOnlyDB, "HomeAway|Corner_Home_5_5_Over",
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Corner_Home_5_5_Over),
                        Home_Win_Any_Half = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.Home_Win_Any_Half,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Home_Win_Any_Half),

                        Average_HT_Goals_AwayTeam =
                                Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.Average_HT_Goals_AwayTeam,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_HT_Goals_AwayTeam),
                        Average_SH_Goals_AwayTeam =
                                Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.Average_SH_Goals_AwayTeam,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_SH_Goals_AwayTeam),
                        Average_FT_Goals_AwayTeam =
                                Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.Average_FT_Goals_AwayTeam,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_AwayTeam),
                        Average_FT_Corners_AwayTeam =
                                Calculator.GetSpecialBetAverage(
                                    this.ComparisonOnlyDB, "HomeAway|Average_FT_Corners_AwayTeam",
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Corners_AwayTeam),
                        Away_HT_05_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.Away_HT_05_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Away_HT_05_Over),
                        Away_HT_15_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.Away_HT_15_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Away_HT_15_Over),
                        Away_SH_05_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.Away_SH_05_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Away_SH_05_Over),
                        Away_SH_15_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.Away_SH_15_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Away_SH_15_Over),
                        Away_FT_05_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.Away_FT_05_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Away_FT_05_Over),
                        Away_FT_15_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.Away_FT_15_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Away_FT_15_Over),
                        Corner_Away_3_5_Over = Calculator.GetSpecialBetAverage(
                                    this.ComparisonOnlyDB, "HomeAway|Corner_Away_3_5_Over",
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Corner_Away_3_5_Over),
                        Corner_Away_4_5_Over = Calculator.GetSpecialBetAverage(
                                    this.ComparisonOnlyDB, "HomeAway|Corner_Away_4_5_Over",
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Corner_Away_4_5_Over),
                        Corner_Away_5_5_Over = Calculator.GetSpecialBetAverage(
                                    this.ComparisonOnlyDB, "HomeAway|Corner_Away_5_5_Over",
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Corner_Away_5_5_Over),
                        Away_Win_Any_Half = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.Away_Win_Any_Half,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Away_Win_Any_Half),

                        HT_05_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.HT_05_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.HT_05_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.HT_05_Over),
                        HT_15_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.HT_15_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.HT_15_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.HT_15_Over),
                        SH_05_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.SH_05_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.SH_05_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.SH_05_Over),
                        SH_15_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.SH_15_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.SH_15_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.SH_15_Over),

                        FT_15_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.FT_15_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.FT_15_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.FT_15_Over),
                        FT_25_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.FT_25_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.FT_25_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.FT_25_Over),
                        FT_35_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.FT_35_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.FT_35_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.FT_35_Over),

                        Corner_7_5_Over = Calculator.GetSpecialBetAverage(
                                    this.ComparisonOnlyDB, "HomeAway|Corner_7_5_Over",
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Corner_7_5_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Corner_7_5_Over),
                        Corner_8_5_Over = Calculator.GetSpecialBetAverage(
                                    this.ComparisonOnlyDB, "HomeAway|Corner_8_5_Over",
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Corner_8_5_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Corner_8_5_Over),
                        Corner_9_5_Over = Calculator.GetSpecialBetAverage(
                                    this.ComparisonOnlyDB, "HomeAway|Corner_9_5_Over",
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Corner_9_5_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Corner_9_5_Over),

                        HT_Result = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.HT_Result,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.HT_Result,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.HT_Result),
                        SH_Result = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.SH_Result,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.SH_Result,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.SH_Result),
                        FT_Result = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.FT_Result,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.FT_Result,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.FT_Result),

                        Is_FT_Win1 = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.Is_FT_Win1,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Is_FT_Win1),
                        Is_FT_Win2 = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.Is_FT_Win2,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Is_FT_Win2),
                        Is_FT_X = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.Is_FT_X,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Is_FT_X,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Is_FT_X),

                        Is_HT_Win1 = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.Is_HT_Win1,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Is_HT_Win1),
                        Is_HT_Win2 = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.Is_HT_Win2,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Is_HT_Win2),
                        Is_HT_X = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.Is_HT_X,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Is_HT_X,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Is_HT_X),

                        Is_SH_Win1 = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.Is_SH_Win1,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Is_SH_Win1),
                        Is_SH_Win2 = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.Is_SH_Win2,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Is_SH_Win2),
                        Is_SH_X = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.Is_SH_X,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Is_SH_X,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Is_SH_X),


                        Is_Corner_FT_Win1 = Calculator.GetSpecialBetAverage(
                                    this.ComparisonOnlyDB, "HomeAway|Is_Corner_FT_Win1",
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Is_Corner_FT_Win1),
                        Is_Corner_FT_Win2 = Calculator.GetSpecialBetAverage(
                                    this.ComparisonOnlyDB, "HomeAway|Is_Corner_FT_Win2",
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Is_Corner_FT_Win2),
                        Is_Corner_FT_X = Calculator.GetSpecialBetAverage(
                                    this.ComparisonOnlyDB, "HomeAway|Is_Corner_FT_X",
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Is_Corner_FT_X,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Is_Corner_FT_X),

                        HT_GG = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.HT_GG,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.HT_GG,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.HT_GG),
                        SH_GG = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.SH_GG,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.SH_GG,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.SH_GG),
                        FT_GG = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.FT_GG,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.FT_GG,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.FT_GG),
                        HT_FT_Result = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.HT_FT_Result,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.HT_FT_Result,            this.AwayTeam_FormPerformanceGuessContainer.HomeAway.HT_FT_Result),
                        MoreGoalsBetweenTimes = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.MoreGoalsBetweenTimes,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.MoreGoalsBetweenTimes,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.MoreGoalsBetweenTimes),
                        Total_BetweenGoals = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.Total_BetweenGoals,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Total_BetweenGoals, this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Total_BetweenGoals)
                    };
                }
                catch (System.Exception)
                {

                    return null;
                }

            }
        }
    }

    public class JobAnalyseModelNisbi
    {
        public JobAnalyseModelNisbi()
        {
        }

        public string Serial => this.ComparisonInfoContainer.Serial;

        public TeamPercentageProfiler TeamPercentageProfiler { get; set; }
        public ComparisonGuessContainer ComparisonInfoContainer { get; set; }
        public FormPerformanceGuessContainer HomeTeam_FormPerformanceGuessContainer { get; set; }
        public FormPerformanceGuessContainer AwayTeam_FormPerformanceGuessContainer { get; set; }
        public StandingInfoModel StandingInfoModel { get; set; }
        public AveragePercentageContainer AverageProfiler
        {
            get
            {
                //return new AveragePercentageContainer();
                try
                {
                    if (this.ComparisonInfoContainer == null || this.ComparisonInfoContainer.General.CountFound < 4)
                    {
                        return null;
                    }

                    if (this.HomeTeam_FormPerformanceGuessContainer == null || this.HomeTeam_FormPerformanceGuessContainer.General.CountFound < 4)
                    {
                        return null;
                    }

                    if (this.AwayTeam_FormPerformanceGuessContainer == null || this.AwayTeam_FormPerformanceGuessContainer.General.CountFound < 4)
                    {
                        return null;
                    }

                    //if (this.StandingInfoModel == null || this.StandingInfoModel.UpTeam == null || this.StandingInfoModel.DownTeam == null)
                    //{
                    //    return null;
                    //}


                    return new AveragePercentageContainer
                    {
                        Average_HT_Goals_HomeTeam =
                                Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.Average_HT_Goals_HomeTeam,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.Average_HT_Goals_HomeTeam),
                        Average_SH_Goals_HomeTeam =
                                Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.Average_SH_Goals_HomeTeam,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.Average_SH_Goals_HomeTeam),
                        Average_FT_Goals_HomeTeam =
                                Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.Average_FT_Goals_HomeTeam,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.Average_FT_Goals_HomeTeam),


                        Home_HT_05_Over = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.General.Home_HT_05_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.Home_HT_05_Over),
                        Home_HT_15_Over = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.General.Home_HT_15_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.Home_HT_15_Over),
                        Home_SH_05_Over = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.General.Home_SH_05_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.Home_SH_05_Over),
                        Home_SH_15_Over = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.General.Home_SH_15_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.Home_SH_15_Over),
                        Home_FT_05_Over = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.General.Home_FT_05_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.Home_FT_05_Over),
                        Home_FT_15_Over = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.General.Home_FT_15_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.Home_FT_15_Over),
                        Home_Win_Any_Half = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.General.Home_Win_Any_Half,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.Home_Win_Any_Half),

                        Average_HT_Goals_AwayTeam =
                                Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.Average_HT_Goals_AwayTeam,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.Average_HT_Goals_AwayTeam),
                        Average_SH_Goals_AwayTeam =
                                Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.Average_SH_Goals_AwayTeam,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.Average_SH_Goals_AwayTeam),
                        Average_FT_Goals_AwayTeam =
                                Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.Average_FT_Goals_AwayTeam,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.Average_FT_Goals_AwayTeam),


                        Away_HT_05_Over = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.General.Away_HT_05_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.Away_HT_05_Over),
                        Away_HT_15_Over = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.General.Away_HT_15_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.Away_HT_15_Over),
                        Away_SH_05_Over = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.General.Away_SH_05_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.Away_SH_05_Over),
                        Away_SH_15_Over = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.General.Away_SH_15_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.Away_SH_15_Over),
                        Away_FT_05_Over = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.General.Away_FT_05_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.Away_FT_05_Over),
                        Away_FT_15_Over = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.General.Away_FT_15_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.Away_FT_15_Over),
                        Away_Win_Any_Half = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.General.Away_Win_Any_Half,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.Away_Win_Any_Half),

                        HT_05_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.HT_05_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.HT_05_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.HT_05_Over),
                        HT_15_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.HT_15_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.HT_15_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.HT_15_Over),
                        SH_05_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.SH_05_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.SH_05_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.SH_05_Over),
                        SH_15_Over = Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.General.SH_15_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.SH_15_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.SH_15_Over),

                        FT_15_Over = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.General.FT_15_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.FT_15_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.FT_15_Over),
                        FT_25_Over = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.General.FT_25_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.FT_25_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.FT_25_Over),
                        FT_35_Over = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.General.FT_35_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.FT_35_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.FT_35_Over),
                        HT_Result = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.General.HT_Result,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.HT_Result,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.HT_Result),
                        SH_Result = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.General.SH_Result,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.SH_Result,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.SH_Result),
                        FT_Result = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.General.FT_Result,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.FT_Result,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.FT_Result),
                        HT_GG = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.General.HT_GG,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.HT_GG,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.HT_GG),
                        SH_GG = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.General.SH_GG,
                                    this.HomeTeam_FormPerformanceGuessContainer.General.SH_GG,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.SH_GG),
                        FT_GG = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.General.FT_GG, this.HomeTeam_FormPerformanceGuessContainer.General.FT_GG,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.FT_GG),
                        HT_FT_Result = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.General.HT_FT_Result, this.HomeTeam_FormPerformanceGuessContainer.General.HT_FT_Result,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.HT_FT_Result),
                        MoreGoalsBetweenTimes = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.General.MoreGoalsBetweenTimes, this.HomeTeam_FormPerformanceGuessContainer.General.MoreGoalsBetweenTimes,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.MoreGoalsBetweenTimes),
                        Total_BetweenGoals = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.General.Total_BetweenGoals, this.HomeTeam_FormPerformanceGuessContainer.General.Total_BetweenGoals,
                                    this.AwayTeam_FormPerformanceGuessContainer.General.Total_BetweenGoals),
                    };
                }
                catch (System.Exception)
                {

                    return null;
                }

            }
        }



        public AveragePercentageContainer AverageProfilerHomeAway
        {
            get
            {
                //return new AveragePercentageContainer();
                try
                {
                    if (this.ComparisonInfoContainer == null || this.ComparisonInfoContainer.General.CountFound < 4)
                    {
                        return null;
                    }

                    if (this.HomeTeam_FormPerformanceGuessContainer == null || this.HomeTeam_FormPerformanceGuessContainer.General.CountFound < 4)
                    {
                        return null;
                    }

                    if (this.AwayTeam_FormPerformanceGuessContainer == null || this.AwayTeam_FormPerformanceGuessContainer.General.CountFound < 4)
                    {
                        return null;
                    }

                    //if (this.StandingInfoModel == null || this.StandingInfoModel.UpTeam == null || this.StandingInfoModel.DownTeam == null)
                    //{
                    //    return null;
                    //}


                    return new AveragePercentageContainer
                    {
                        Average_HT_Goals_HomeTeam =
                                Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.Average_HT_Goals_HomeTeam,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_HT_Goals_HomeTeam),
                        Average_SH_Goals_HomeTeam =
                                Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.Average_SH_Goals_HomeTeam,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_SH_Goals_HomeTeam),
                        Average_FT_Goals_HomeTeam =
                                Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.Average_FT_Goals_HomeTeam,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_HomeTeam),
                        Home_HT_05_Over = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.HomeAway.Home_HT_05_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Home_HT_05_Over),
                        Home_HT_15_Over = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.HomeAway.Home_HT_15_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Home_HT_15_Over),
                        Home_SH_05_Over = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.HomeAway.Home_SH_05_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Home_SH_05_Over),
                        Home_SH_15_Over = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.HomeAway.Home_SH_15_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Home_SH_15_Over),
                        Home_FT_05_Over = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.HomeAway.Home_FT_05_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Home_FT_05_Over),
                        Home_FT_15_Over = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.HomeAway.Home_FT_15_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Home_FT_15_Over),
                        Home_Win_Any_Half = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.HomeAway.Home_Win_Any_Half,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Home_Win_Any_Half),

                        Average_HT_Goals_AwayTeam =
                                Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.Average_HT_Goals_AwayTeam,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_HT_Goals_AwayTeam),
                        Average_SH_Goals_AwayTeam =
                                Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.Average_SH_Goals_AwayTeam,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_SH_Goals_AwayTeam),
                        Average_FT_Goals_AwayTeam =
                                Calculator.GetBetAverage(
                                    this.ComparisonInfoContainer.HomeAway.Average_FT_Goals_AwayTeam,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_AwayTeam),
                        Away_HT_05_Over = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.HomeAway.Away_HT_05_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Away_HT_05_Over),
                        Away_HT_15_Over = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.HomeAway.Away_HT_15_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Away_HT_15_Over),
                        Away_SH_05_Over = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.HomeAway.Away_SH_05_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Away_SH_05_Over),
                        Away_SH_15_Over = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.HomeAway.Away_SH_15_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Away_SH_15_Over),
                        Away_FT_05_Over = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.HomeAway.Away_FT_05_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Away_FT_05_Over),
                        Away_FT_15_Over = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.HomeAway.Away_FT_15_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Away_FT_15_Over),
                        Away_Win_Any_Half = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.HomeAway.Away_Win_Any_Half,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Away_Win_Any_Half),

                        HT_05_Over = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.HomeAway.HT_05_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.HT_05_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.HT_05_Over),
                        HT_15_Over = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.HomeAway.HT_15_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.HT_15_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.HT_15_Over),
                        SH_05_Over = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.HomeAway.SH_05_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.SH_05_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.SH_05_Over),
                        SH_15_Over = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.HomeAway.SH_15_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.SH_15_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.SH_15_Over),

                        FT_15_Over = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.HomeAway.FT_15_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.FT_15_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.FT_15_Over),
                        FT_25_Over = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.HomeAway.FT_25_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.FT_25_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.FT_25_Over),
                        FT_35_Over = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.HomeAway.FT_35_Over,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.FT_35_Over,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.FT_35_Over),
                        HT_Result = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.HomeAway.HT_Result,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.HT_Result,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.HT_Result),
                        SH_Result = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.HomeAway.SH_Result,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.SH_Result,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.SH_Result),
                        FT_Result = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.HomeAway.FT_Result,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.FT_Result,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.FT_Result),
                        HT_GG = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.HomeAway.HT_GG,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.HT_GG,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.HT_GG),
                        SH_GG = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.HomeAway.SH_GG,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.SH_GG,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.SH_GG),
                        FT_GG = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.HomeAway.FT_GG,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.FT_GG,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.FT_GG),
                        HT_FT_Result = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.HomeAway.HT_FT_Result,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.HT_FT_Result, this.AwayTeam_FormPerformanceGuessContainer.HomeAway.HT_FT_Result),
                        MoreGoalsBetweenTimes = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.HomeAway.MoreGoalsBetweenTimes,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.MoreGoalsBetweenTimes,
                                    this.AwayTeam_FormPerformanceGuessContainer.HomeAway.MoreGoalsBetweenTimes),
                        Total_BetweenGoals = Calculator.GetBetAverageNisbi(
                                    this.ComparisonInfoContainer.HomeAway.Total_BetweenGoals,
                                    this.HomeTeam_FormPerformanceGuessContainer.HomeAway.Total_BetweenGoals, this.AwayTeam_FormPerformanceGuessContainer.HomeAway.Total_BetweenGoals),
                    };
                }
                catch (System.Exception)
                {

                    return null;
                }

            }
        }
    }
}

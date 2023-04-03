using Core.Entities.Concrete.ComplexModels.Sql;

namespace SBA.WebAPI.Utilities.Helpers
{
    public class FileFormatBinder
    {
        public string BindComplexStats(string format, string formatPosShut, string statTeamCorner, string statAllCorner, MatchLeagueComplexDto dto)
        {
            string formatPosShutBySidePerformanceHome = ""; // 198

            if (dto == null) return "NO CONTENT";

            if (dto.HomeTeamPerformanceAtHome.Average_FT_Shut_Team >= 0m &&
                dto.HomeTeamPerformanceAtHome.Team_Possesion >= 0)
            {
                formatPosShutBySidePerformanceHome = string.Format(formatPosShut, dto.Match.HomeTeam, dto.HomeTeamPerformanceAtHome.Team_Possesion, dto.HomeTeamPerformanceAtHome.Average_FT_Shut_Team, dto.HomeTeamPerformanceAtHome.Average_FT_ShutOnTarget_Team, dto.HomeTeamPerformanceAtHome.Team_ShutOnTarget_Percent);
            }

            string formatCornerBySidePerformanceHome = ""; // 199

            if (dto.HomeTeamPerformanceAtHome.Average_FT_Corners_Team >= 0m)
            {
                formatCornerBySidePerformanceHome = string.Format(statTeamCorner, dto.Match.HomeTeam, dto.HomeTeamPerformanceAtHome.Average_FT_Corners_Team, dto.HomeTeamPerformanceAtHome.Is_FT_CornerWinTeam, dto.HomeTeamPerformanceAtHome.Is_FT_CornerX, dto.HomeTeamPerformanceAtHome.Team_FT_Corner_35_Over, dto.HomeTeamPerformanceAtHome.Team_FT_Corner_45_Over, dto.HomeTeamPerformanceAtHome.FT_Corner_75_Over, dto.HomeTeamPerformanceAtHome.FT_Corner_85_Over, dto.HomeTeamPerformanceAtHome.FT_Corner_95_Over);
            }

            string formatPosShutBySidePerformanceAway = ""; // 200

            if (dto.AwayTeamPerformanceAtAway.Average_FT_Shut_Team >= 0m &&
                dto.AwayTeamPerformanceAtAway.Team_Possesion >= 0)
            {
                formatPosShutBySidePerformanceAway = string.Format(formatPosShut, dto.Match.AwayTeam, dto.AwayTeamPerformanceAtAway.Team_Possesion, dto.AwayTeamPerformanceAtAway.Average_FT_Shut_Team, dto.AwayTeamPerformanceAtAway.Average_FT_ShutOnTarget_Team, dto.AwayTeamPerformanceAtAway.Team_ShutOnTarget_Percent);
            }

            string formatCornerBySidePerformanceAway = ""; // 201

            if (dto.AwayTeamPerformanceAtAway.Average_FT_Corners_Team >= 0m)
            {
                formatCornerBySidePerformanceAway = string.Format(statTeamCorner, dto.Match.AwayTeam, dto.AwayTeamPerformanceAtAway.Average_FT_Corners_Team, dto.AwayTeamPerformanceAtAway.Is_FT_CornerWinTeam, dto.AwayTeamPerformanceAtAway.Is_FT_CornerX, dto.AwayTeamPerformanceAtAway.Team_FT_Corner_35_Over, dto.AwayTeamPerformanceAtAway.Team_FT_Corner_45_Over, dto.AwayTeamPerformanceAtAway.FT_Corner_75_Over, dto.AwayTeamPerformanceAtAway.FT_Corner_85_Over, dto.AwayTeamPerformanceAtAway.FT_Corner_95_Over);
            }

            string formatPosShutGeneralPerformanceHome = ""; // 202

            if (dto.HomeTeamPerformanceGeneral.Average_FT_Shut_Team >= 0m &&
                dto.HomeTeamPerformanceGeneral.Team_Possesion >= 0)
            {
                formatPosShutGeneralPerformanceHome = string.Format(formatPosShut, dto.Match.HomeTeam, dto.HomeTeamPerformanceGeneral.Team_Possesion, dto.HomeTeamPerformanceGeneral.Average_FT_Shut_Team, dto.HomeTeamPerformanceGeneral.Average_FT_ShutOnTarget_Team, dto.HomeTeamPerformanceGeneral.Team_ShutOnTarget_Percent);
            }

            string formatCornerGeneralPerformanceHome = ""; // 203

            if (dto.HomeTeamPerformanceGeneral.Average_FT_Corners_Team >= 0m)
            {
                formatCornerGeneralPerformanceHome = string.Format(statTeamCorner, dto.Match.HomeTeam, dto.HomeTeamPerformanceGeneral.Average_FT_Corners_Team, dto.HomeTeamPerformanceGeneral.Is_FT_CornerWinTeam, dto.HomeTeamPerformanceGeneral.Is_FT_CornerX, dto.HomeTeamPerformanceGeneral.Team_FT_Corner_35_Over, dto.HomeTeamPerformanceGeneral.Team_FT_Corner_45_Over, dto.HomeTeamPerformanceGeneral.FT_Corner_75_Over, dto.HomeTeamPerformanceGeneral.FT_Corner_85_Over, dto.HomeTeamPerformanceGeneral.FT_Corner_95_Over);
            }

            string formatPosShutGeneralPerformanceAway = ""; // 204

            if (dto.AwayTeamPerformanceGeneral.Average_FT_Shut_Team >= 0m &&
                dto.AwayTeamPerformanceGeneral.Team_Possesion >= 0)
            {
                formatPosShutGeneralPerformanceAway = string.Format(formatPosShut, dto.Match.AwayTeam, dto.AwayTeamPerformanceGeneral.Team_Possesion, dto.AwayTeamPerformanceGeneral.Average_FT_Shut_Team, dto.AwayTeamPerformanceGeneral.Average_FT_ShutOnTarget_Team, dto.AwayTeamPerformanceGeneral.Team_ShutOnTarget_Percent);
            }

            string formatCornerGeneralPerformanceAway = ""; // 205

            if (dto.AwayTeamPerformanceGeneral.Average_FT_Corners_Team >= 0m)
            {
                formatCornerGeneralPerformanceAway = string.Format(statTeamCorner, dto.Match.AwayTeam, dto.AwayTeamPerformanceGeneral.Average_FT_Corners_Team, dto.AwayTeamPerformanceGeneral.Is_FT_CornerWinTeam, dto.AwayTeamPerformanceGeneral.Is_FT_CornerX, dto.AwayTeamPerformanceGeneral.Team_FT_Corner_35_Over, dto.AwayTeamPerformanceGeneral.Team_FT_Corner_45_Over, dto.AwayTeamPerformanceGeneral.FT_Corner_75_Over, dto.AwayTeamPerformanceGeneral.FT_Corner_85_Over, dto.AwayTeamPerformanceGeneral.FT_Corner_95_Over);
            }

            return string.Format(format,
                dto.Match.Match, // +0
                dto.Match.HomeTeam, // +1
                dto.Match.AwayTeam, // +2
                dto.Match.Country, // +3
                dto.Match.League, // +4
                dto.LeagueStat.FT_GoalsAverage, // +5
                dto.LeagueStat.HT_GoalsAverage, // +6
                dto.LeagueStat.SH_GoalsAverage, // +7
                dto.LeagueStat.GG_Percentage, // +8
                dto.LeagueStat.FT_Over15_Percentage, // +9
                dto.LeagueStat.FT_Over25_Percentage, // +10
                dto.LeagueStat.FT_Over35_Percentage, // +11
                dto.LeagueStat.HT_Over05_Percentage, // +12
                dto.LeagueStat.HT_Over15_Percentage, // +13
                dto.LeagueStat.SH_Over05_Percentage, // +14
                dto.LeagueStat.SH_Over15_Percentage, // +15


                dto.ComparisonHomeAway.Average_FT_Goals_HomeTeam, // 16 +
                dto.ComparisonHomeAway.Average_HT_Goals_HomeTeam, // 17+
                dto.ComparisonHomeAway.Average_SH_Goals_HomeTeam, // 18+
                dto.ComparisonHomeAway.Average_FT_Goals_AwayTeam, // +
                dto.ComparisonHomeAway.Average_HT_Goals_AwayTeam, // +
                dto.ComparisonHomeAway.Average_SH_Goals_AwayTeam, // +21
                dto.ComparisonHomeAway.Is_FT_Win1, // 
                dto.ComparisonHomeAway.Is_FT_X,
                dto.ComparisonHomeAway.Is_FT_Win2,
                dto.ComparisonHomeAway.Is_HT_Win1,
                dto.ComparisonHomeAway.Is_HT_X,
                dto.ComparisonHomeAway.Is_HT_Win2,
                dto.ComparisonHomeAway.Is_SH_Win1,
                dto.ComparisonHomeAway.Is_SH_X,
                dto.ComparisonHomeAway.Is_SH_Win2, //30
                dto.ComparisonHomeAway.FT_GG, // 31+
                dto.ComparisonHomeAway.HT_GG, // +
                dto.ComparisonHomeAway.SH_GG, //+33
                dto.ComparisonHomeAway.FT_15_Over,
                dto.ComparisonHomeAway.FT_25_Over,
                dto.ComparisonHomeAway.FT_35_Over, //+36
                dto.ComparisonHomeAway.HT_05_Over,
                dto.ComparisonHomeAway.HT_15_Over,
                dto.ComparisonHomeAway.SH_05_Over,
                dto.ComparisonHomeAway.SH_15_Over, //+40
                dto.ComparisonHomeAway.Home_FT_05_Over,
                dto.ComparisonHomeAway.Home_FT_15_Over,
                dto.ComparisonHomeAway.Home_HT_05_Over,
                dto.ComparisonHomeAway.Home_HT_15_Over,
                dto.ComparisonHomeAway.Home_SH_05_Over,
                dto.ComparisonHomeAway.Home_SH_15_Over, // 46+
                dto.ComparisonHomeAway.Home_Win_Any_Half, // +47
                dto.ComparisonHomeAway.Away_FT_05_Over,
                dto.ComparisonHomeAway.Away_FT_15_Over,
                dto.ComparisonHomeAway.Away_HT_05_Over,
                dto.ComparisonHomeAway.Away_HT_15_Over,
                dto.ComparisonHomeAway.Away_SH_05_Over,
                dto.ComparisonHomeAway.Away_SH_15_Over,
                dto.ComparisonHomeAway.Away_Win_Any_Half, // 54+



                dto.ComparisonGeneral.Average_FT_Goals_HomeTeam, // 16 +
                dto.ComparisonGeneral.Average_HT_Goals_HomeTeam, // 17+
                dto.ComparisonGeneral.Average_SH_Goals_HomeTeam, // 18+
                dto.ComparisonGeneral.Average_FT_Goals_AwayTeam, // +
                dto.ComparisonGeneral.Average_HT_Goals_AwayTeam, // +
                dto.ComparisonGeneral.Average_SH_Goals_AwayTeam, // +21
                dto.ComparisonGeneral.Is_FT_Win1, // 
                dto.ComparisonGeneral.Is_FT_X,
                dto.ComparisonGeneral.Is_FT_Win2,
                dto.ComparisonGeneral.Is_HT_Win1,
                dto.ComparisonGeneral.Is_HT_X,
                dto.ComparisonGeneral.Is_HT_Win2,
                dto.ComparisonGeneral.Is_SH_Win1,
                dto.ComparisonGeneral.Is_SH_X,
                dto.ComparisonGeneral.Is_SH_Win2, //
                dto.ComparisonGeneral.FT_GG, // 
                dto.ComparisonGeneral.HT_GG, // +
                dto.ComparisonGeneral.SH_GG, //+
                dto.ComparisonGeneral.FT_15_Over,
                dto.ComparisonGeneral.FT_25_Over,
                dto.ComparisonGeneral.FT_35_Over, //+
                dto.ComparisonGeneral.HT_05_Over,
                dto.ComparisonGeneral.HT_15_Over,
                dto.ComparisonGeneral.SH_05_Over,
                dto.ComparisonGeneral.SH_15_Over, //+
                dto.ComparisonGeneral.Home_FT_05_Over,
                dto.ComparisonGeneral.Home_FT_15_Over,
                dto.ComparisonGeneral.Home_HT_05_Over,
                dto.ComparisonGeneral.Home_HT_15_Over,
                dto.ComparisonGeneral.Home_SH_05_Over,
                dto.ComparisonGeneral.Home_SH_15_Over, // 
                dto.ComparisonGeneral.Home_Win_Any_Half, // +
                dto.ComparisonGeneral.Away_FT_05_Over,
                dto.ComparisonGeneral.Away_FT_15_Over,
                dto.ComparisonGeneral.Away_HT_05_Over,
                dto.ComparisonGeneral.Away_HT_15_Over,
                dto.ComparisonGeneral.Away_SH_05_Over,
                dto.ComparisonGeneral.Away_SH_15_Over,
                dto.ComparisonGeneral.Away_Win_Any_Half, // 93+


                dto.HomeTeamPerformanceAtHome.Average_FT_Goals_Team, // 94+
                dto.HomeTeamPerformanceAtHome.Average_HT_Goals_Team,
                dto.HomeTeamPerformanceAtHome.Average_SH_Goals_Team, // 96+
                dto.HomeTeamPerformanceAtHome.Is_FT_Win, // 97+
                dto.HomeTeamPerformanceAtHome.Is_FT_X,
                dto.HomeTeamPerformanceAtHome.Is_HT_Win,
                dto.HomeTeamPerformanceAtHome.Is_HT_X,
                dto.HomeTeamPerformanceAtHome.Is_SH_Win,
                dto.HomeTeamPerformanceAtHome.Is_SH_X, // 102+
                dto.HomeTeamPerformanceAtHome.FT_GG,
                dto.HomeTeamPerformanceAtHome.HT_GG,
                dto.HomeTeamPerformanceAtHome.SH_GG, // +105
                dto.HomeTeamPerformanceAtHome.FT_15_Over,
                dto.HomeTeamPerformanceAtHome.FT_25_Over,
                dto.HomeTeamPerformanceAtHome.FT_35_Over,
                dto.HomeTeamPerformanceAtHome.HT_05_Over,
                dto.HomeTeamPerformanceAtHome.HT_15_Over,
                dto.HomeTeamPerformanceAtHome.SH_05_Over,
                dto.HomeTeamPerformanceAtHome.SH_15_Over, // +112
                dto.HomeTeamPerformanceAtHome.Team_FT_05_Over,
                dto.HomeTeamPerformanceAtHome.Team_FT_15_Over,
                dto.HomeTeamPerformanceAtHome.Team_HT_05_Over,
                dto.HomeTeamPerformanceAtHome.Team_HT_15_Over,
                dto.HomeTeamPerformanceAtHome.Team_SH_05_Over,
                dto.HomeTeamPerformanceAtHome.Team_SH_15_Over,
                dto.HomeTeamPerformanceAtHome.Team_Win_Any_Half, // 119+


                dto.AwayTeamPerformanceAtAway.Average_FT_Goals_Team, // 120+
                dto.AwayTeamPerformanceAtAway.Average_HT_Goals_Team,
                dto.AwayTeamPerformanceAtAway.Average_SH_Goals_Team, // 
                dto.AwayTeamPerformanceAtAway.Is_FT_Win, // 
                dto.AwayTeamPerformanceAtAway.Is_FT_X,
                dto.AwayTeamPerformanceAtAway.Is_HT_Win,
                dto.AwayTeamPerformanceAtAway.Is_HT_X,
                dto.AwayTeamPerformanceAtAway.Is_SH_Win,
                dto.AwayTeamPerformanceAtAway.Is_SH_X, // 
                dto.AwayTeamPerformanceAtAway.FT_GG,
                dto.AwayTeamPerformanceAtAway.HT_GG,
                dto.AwayTeamPerformanceAtAway.SH_GG, // +
                dto.AwayTeamPerformanceAtAway.FT_15_Over,
                dto.AwayTeamPerformanceAtAway.FT_25_Over,
                dto.AwayTeamPerformanceAtAway.FT_35_Over,
                dto.AwayTeamPerformanceAtAway.HT_05_Over,
                dto.AwayTeamPerformanceAtAway.HT_15_Over,
                dto.AwayTeamPerformanceAtAway.SH_05_Over,
                dto.AwayTeamPerformanceAtAway.SH_15_Over, // 
                dto.AwayTeamPerformanceAtAway.Team_FT_05_Over,
                dto.AwayTeamPerformanceAtAway.Team_FT_15_Over,
                dto.AwayTeamPerformanceAtAway.Team_HT_05_Over,
                dto.AwayTeamPerformanceAtAway.Team_HT_15_Over,
                dto.AwayTeamPerformanceAtAway.Team_SH_05_Over,
                dto.AwayTeamPerformanceAtAway.Team_SH_15_Over,
                dto.AwayTeamPerformanceAtAway.Team_Win_Any_Half, // 145+


                dto.HomeTeamPerformanceGeneral.Average_FT_Goals_Team, // 146+
                dto.HomeTeamPerformanceGeneral.Average_HT_Goals_Team,
                dto.HomeTeamPerformanceGeneral.Average_SH_Goals_Team, // 
                dto.HomeTeamPerformanceGeneral.Is_FT_Win, // 
                dto.HomeTeamPerformanceGeneral.Is_FT_X,
                dto.HomeTeamPerformanceGeneral.Is_HT_Win,
                dto.HomeTeamPerformanceGeneral.Is_HT_X,
                dto.HomeTeamPerformanceGeneral.Is_SH_Win,
                dto.HomeTeamPerformanceGeneral.Is_SH_X, // 
                dto.HomeTeamPerformanceGeneral.FT_GG,
                dto.HomeTeamPerformanceGeneral.HT_GG,
                dto.HomeTeamPerformanceGeneral.SH_GG, // +
                dto.HomeTeamPerformanceGeneral.FT_15_Over,
                dto.HomeTeamPerformanceGeneral.FT_25_Over,
                dto.HomeTeamPerformanceGeneral.FT_35_Over,
                dto.HomeTeamPerformanceGeneral.HT_05_Over,
                dto.HomeTeamPerformanceGeneral.HT_15_Over,
                dto.HomeTeamPerformanceGeneral.SH_05_Over,
                dto.HomeTeamPerformanceGeneral.SH_15_Over, // +
                dto.HomeTeamPerformanceGeneral.Team_FT_05_Over,
                dto.HomeTeamPerformanceGeneral.Team_FT_15_Over,
                dto.HomeTeamPerformanceGeneral.Team_HT_05_Over,
                dto.HomeTeamPerformanceGeneral.Team_HT_15_Over,
                dto.HomeTeamPerformanceGeneral.Team_SH_05_Over,
                dto.HomeTeamPerformanceGeneral.Team_SH_15_Over,
                dto.HomeTeamPerformanceGeneral.Team_Win_Any_Half, // 171


                dto.AwayTeamPerformanceGeneral.Average_FT_Goals_Team, // 172+
                dto.AwayTeamPerformanceGeneral.Average_HT_Goals_Team,
                dto.AwayTeamPerformanceGeneral.Average_SH_Goals_Team, // +
                dto.AwayTeamPerformanceGeneral.Is_FT_Win, // +
                dto.AwayTeamPerformanceGeneral.Is_FT_X,
                dto.AwayTeamPerformanceGeneral.Is_HT_Win,
                dto.AwayTeamPerformanceGeneral.Is_HT_X,
                dto.AwayTeamPerformanceGeneral.Is_SH_Win,
                dto.AwayTeamPerformanceGeneral.Is_SH_X, // +
                dto.AwayTeamPerformanceGeneral.FT_GG,
                dto.AwayTeamPerformanceGeneral.HT_GG,
                dto.AwayTeamPerformanceGeneral.SH_GG, // +
                dto.AwayTeamPerformanceGeneral.FT_15_Over,
                dto.AwayTeamPerformanceGeneral.FT_25_Over,
                dto.AwayTeamPerformanceGeneral.FT_35_Over,
                dto.AwayTeamPerformanceGeneral.HT_05_Over,
                dto.AwayTeamPerformanceGeneral.HT_15_Over,
                dto.AwayTeamPerformanceGeneral.SH_05_Over,
                dto.AwayTeamPerformanceGeneral.SH_15_Over, // +
                dto.AwayTeamPerformanceGeneral.Team_FT_05_Over,
                dto.AwayTeamPerformanceGeneral.Team_FT_15_Over,
                dto.AwayTeamPerformanceGeneral.Team_HT_05_Over,
                dto.AwayTeamPerformanceGeneral.Team_HT_15_Over,
                dto.AwayTeamPerformanceGeneral.Team_SH_05_Over,
                dto.AwayTeamPerformanceGeneral.Team_SH_15_Over,
                dto.AwayTeamPerformanceGeneral.Team_Win_Any_Half, // 197+
                formatPosShutBySidePerformanceHome,
                formatCornerBySidePerformanceHome,
                formatPosShutBySidePerformanceAway,
                formatCornerBySidePerformanceAway,
                formatPosShutGeneralPerformanceHome,
                formatCornerGeneralPerformanceHome,
                formatPosShutGeneralPerformanceAway,
                formatCornerGeneralPerformanceAway);
        }
    }
}

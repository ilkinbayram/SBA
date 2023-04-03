using Core.Entities.Concrete.ComplexModels.ML;
using Core.Entities.Concrete.ComplexModels.Sql;
using Core.Entities.Concrete.ExternalDbEntities;
using SBA.WebAPI.Resources.Models;

namespace SBA.WebAPI.Utilities.Extensions
{
    public static class MapperExtension
    {
        public static ProgramMatchModel MapResult(this MatchIdentifier model)
        {
            return new ProgramMatchModel
            {
                Serial = model.Serial,
                AwayTeam= model.AwayTeam,
                HomeTeam= model.HomeTeam,
                MatchHour = model.MatchDateTime.Hour,
                MatchMinute = model.MatchDateTime.Minute,
                MatchTime = model.MatchDateTime.ToString("HH:mm")
            };
        }


        public static AiAnalyseModel MapToAI(this MatchLeagueComplexDto model)
        {
            return new AiAnalyseModel
            {
                Sport = "Football",
                AwayTeam = model.Match.AwayTeam,
                HomeTeam = model.Match.HomeTeam,
                MatchDateTime = DateTime.Now.Date,
                League_Statistics_For_Current_Month = new LeagueStatisticsAiModel
                {
                    Average_FullTime_Goals = model.LeagueStat.FT_GoalsAverage,
                    Average_HalfTime_Goals = model.LeagueStat.HT_GoalsAverage,
                    Average_SecondHalf_Goals = model.LeagueStat.SH_GoalsAverage,
                    CountryName = model.Match.Country,
                    LeagueName = model.Match.League,
                    FullTime_Over15_Percentage = model.LeagueStat.FT_Over15_Percentage,
                    FullTime_Over25_Percentage = model.LeagueStat.FT_Over25_Percentage,
                    FullTime_Over35_Percentage = model.LeagueStat.FT_Over35_Percentage,
                    HalfTime_Over05_Percentage = model.LeagueStat.HT_Over05_Percentage,
                    HalfTime_Over15_Percentage = model.LeagueStat.HT_Over15_Percentage,
                    SecondHalf_Over05_Percentage = model.LeagueStat.SH_Over05_Percentage,
                    SecondHalf_Over15_Percentage = model.LeagueStat.SH_Over15_Percentage,
                    BothTeamsToScore_Percentage = model.LeagueStat.GG_Percentage
                },
                H2H_Comparison = new ComparisonAiModel
                {
                    HomeAtHome_AwayAtAway = new ComparisonAiStatisticsHolder
                    {
                        Average_FullTime_Goals_AwayTeam = model.ComparisonHomeAway.Average_FT_Goals_AwayTeam,
                        Average_FullTime_Goals_HomeTeam = model.ComparisonHomeAway.Average_FT_Goals_HomeTeam,
                        Average_HalfTime_Goals_AwayTeam = model.ComparisonHomeAway.Average_HT_Goals_AwayTeam,
                        Average_HalfTime_Goals_HomeTeam = model.ComparisonHomeAway.Average_HT_Goals_HomeTeam,
                        Average_SecondHalf_Goals_AwayTeam = model.ComparisonHomeAway.Average_SH_Goals_AwayTeam,
                        Average_SecondHalf_Goals_HomeTeam = model.ComparisonHomeAway.Average_SH_Goals_HomeTeam,
                        FullTime_15_Over_Percentage = model.ComparisonHomeAway.FT_15_Over,
                        FullTime_25_Over_Percentage = model.ComparisonHomeAway.FT_25_Over,
                        FullTime_35_Over_Percentage = model.ComparisonHomeAway.FT_35_Over,
                        Away_FullTime_05_Over_Percentage = model.ComparisonHomeAway.Away_FT_05_Over,
                        Away_FullTime_15_Over_Percentage = model.ComparisonHomeAway.Away_FT_15_Over,
                        Away_HalfTime_05_Over_Percentage = model.ComparisonHomeAway.Away_HT_05_Over,
                        Away_HalfTime_15_Over_Percentage = model.ComparisonHomeAway.Away_HT_15_Over,
                        Away_SecondHalf_05_Over_Percentage = model.ComparisonHomeAway.Away_SH_05_Over,
                        Away_SecondHalf_15_Over_Percentage = model.ComparisonHomeAway.Away_SH_15_Over,
                        Away_Win_Any_Half_Percentage = model.ComparisonHomeAway.Away_Win_Any_Half,
                        Home_FullTime_05_Over_Percentage = model.ComparisonHomeAway.Home_FT_05_Over,
                        Home_FullTime_15_Over_Percentage = model.ComparisonHomeAway.Home_FT_15_Over,
                        Home_HalfTime_05_Over_Percentage = model.ComparisonHomeAway.Home_HT_05_Over,
                        Home_HalfTime_15_Over_Percentage = model.ComparisonHomeAway.Home_HT_15_Over,
                        Home_SecondHalf_05_Over_Percentage = model.ComparisonHomeAway.Home_SH_05_Over,
                        Home_SecondHalf_15_Over_Percentage = model.ComparisonHomeAway.Home_SH_15_Over,
                        Home_Win_Any_Half_Percentage = model.ComparisonHomeAway.Home_Win_Any_Half,
                        FullTime_Home_Win_Percentage = model.ComparisonHomeAway.Is_FT_Win1,
                        FullTime_Draw_Percentage = model.ComparisonHomeAway.Is_FT_X,
                        FullTime_Away_Win_Percentage = model.ComparisonHomeAway.Is_FT_Win2,
                        HalfTime_Home_Win_Percentage = model.ComparisonHomeAway.Is_HT_Win1,
                        HalfTime_Draw_Percentage = model.ComparisonHomeAway.Is_HT_X,
                        HalfTime_Away_Win_Percentage = model.ComparisonHomeAway.Is_HT_Win2,
                        SecondHalf_Home_Win_Percentage = model.ComparisonHomeAway.Is_SH_Win1,
                        SecondHalf_Draw_Percentage = model.ComparisonHomeAway.Is_SH_X,
                        SecondHalf_Away_Win_Percentage = model.ComparisonHomeAway.Is_SH_Win2,
                        FullTime_BothTeamToScore_Percentage = model.ComparisonHomeAway.FT_GG,
                        HalfTime_BothTeamToScore_Percentage = model.ComparisonHomeAway.HT_GG,
                        SecondHalf_BothTeamToScore_Percentage = model.ComparisonHomeAway.SH_GG,
                        HalfTime_05_Over_Percentage = model.ComparisonHomeAway.HT_05_Over,
                        HalfTime_15_Over_Percentage = model.ComparisonHomeAway.HT_15_Over,
                        SecondHalf_05_Over_Percentage = model.ComparisonHomeAway.SH_05_Over,
                        SecondHalf_15_Over_Percentage = model.ComparisonHomeAway.SH_15_Over
                    },
                    General = new ComparisonAiStatisticsHolder
                    {
                        Average_FullTime_Goals_AwayTeam = model.ComparisonGeneral.Average_FT_Goals_AwayTeam,
                        Average_FullTime_Goals_HomeTeam = model.ComparisonGeneral.Average_FT_Goals_HomeTeam,
                        Average_HalfTime_Goals_AwayTeam = model.ComparisonGeneral.Average_HT_Goals_AwayTeam,
                        Average_HalfTime_Goals_HomeTeam = model.ComparisonGeneral.Average_HT_Goals_HomeTeam,
                        Average_SecondHalf_Goals_AwayTeam = model.ComparisonGeneral.Average_SH_Goals_AwayTeam,
                        Average_SecondHalf_Goals_HomeTeam = model.ComparisonGeneral.Average_SH_Goals_HomeTeam,
                        FullTime_15_Over_Percentage = model.ComparisonGeneral.FT_15_Over,
                        FullTime_25_Over_Percentage = model.ComparisonGeneral.FT_25_Over,
                        FullTime_35_Over_Percentage = model.ComparisonGeneral.FT_35_Over,
                        Away_FullTime_05_Over_Percentage = model.ComparisonGeneral.Away_FT_05_Over,
                        Away_FullTime_15_Over_Percentage = model.ComparisonGeneral.Away_FT_15_Over,
                        Away_HalfTime_05_Over_Percentage = model.ComparisonGeneral.Away_HT_05_Over,
                        Away_HalfTime_15_Over_Percentage = model.ComparisonGeneral.Away_HT_15_Over,
                        Away_SecondHalf_05_Over_Percentage = model.ComparisonGeneral.Away_SH_05_Over,
                        Away_SecondHalf_15_Over_Percentage = model.ComparisonGeneral.Away_SH_15_Over,
                        Away_Win_Any_Half_Percentage = model.ComparisonGeneral.Away_Win_Any_Half,
                        Home_FullTime_05_Over_Percentage = model.ComparisonGeneral.Home_FT_05_Over,
                        Home_FullTime_15_Over_Percentage = model.ComparisonGeneral.Home_FT_15_Over,
                        Home_HalfTime_05_Over_Percentage = model.ComparisonGeneral.Home_HT_05_Over,
                        Home_HalfTime_15_Over_Percentage = model.ComparisonGeneral.Home_HT_15_Over,
                        Home_SecondHalf_05_Over_Percentage = model.ComparisonGeneral.Home_SH_05_Over,
                        Home_SecondHalf_15_Over_Percentage = model.ComparisonGeneral.Home_SH_15_Over,
                        Home_Win_Any_Half_Percentage = model.ComparisonGeneral.Home_Win_Any_Half,
                        FullTime_Home_Win_Percentage = model.ComparisonGeneral.Is_FT_Win1,
                        FullTime_Draw_Percentage = model.ComparisonGeneral.Is_FT_X,
                        FullTime_Away_Win_Percentage = model.ComparisonGeneral.Is_FT_Win2,
                        HalfTime_Home_Win_Percentage = model.ComparisonGeneral.Is_HT_Win1,
                        HalfTime_Draw_Percentage = model.ComparisonGeneral.Is_HT_X,
                        HalfTime_Away_Win_Percentage = model.ComparisonGeneral.Is_HT_Win2,
                        SecondHalf_Home_Win_Percentage = model.ComparisonGeneral.Is_SH_Win1,
                        SecondHalf_Draw_Percentage = model.ComparisonGeneral.Is_SH_X,
                        SecondHalf_Away_Win_Percentage = model.ComparisonGeneral.Is_SH_Win2,
                        FullTime_BothTeamToScore_Percentage = model.ComparisonGeneral.FT_GG,
                        HalfTime_BothTeamToScore_Percentage = model.ComparisonGeneral.HT_GG,
                        SecondHalf_BothTeamToScore_Percentage = model.ComparisonGeneral.SH_GG,
                        HalfTime_05_Over_Percentage = model.ComparisonGeneral.HT_05_Over,
                        HalfTime_15_Over_Percentage = model.ComparisonGeneral.HT_15_Over,
                        SecondHalf_05_Over_Percentage = model.ComparisonGeneral.SH_05_Over,
                        SecondHalf_15_Over_Percentage = model.ComparisonGeneral.SH_15_Over
                    }
                },
                Form_Performance_Teams = new PerformanceAiModel
                {
                    HomeTeam = new HomeTeamAiPerformanceHolder
                    {
                        Last_6_Matches_AtHome = new PerformanceAiStatisticsHolder
                        {
                            Individual_Average_FullTime_Goals = model.HomeTeamPerformanceAtHome.Average_FT_Goals_Team,
                            Individual_Average_HalfTime_Goals = model.HomeTeamPerformanceAtHome.Average_HT_Goals_Team,
                            Individual_Average_SecondHalf_Goals = model.HomeTeamPerformanceAtHome.Average_SH_Goals_Team,
                            Individual_Average_FullTime_Corners = model.HomeTeamPerformanceAtHome.Average_FT_Corners_Team,
                            Individual_Average_FullTime_Shot = model.HomeTeamPerformanceAtHome.Average_FT_Shut_Team,
                            Individual_Average_FullTime_ShotOnTarget = model.HomeTeamPerformanceAtHome.Average_FT_ShutOnTarget_Team,
                            Individual_Average_Possesion_Of_Ball = model.HomeTeamPerformanceAtHome.Team_Possesion,
                            FullTime_15_Over_Percentage = model.HomeTeamPerformanceAtHome.FT_15_Over,
                            FullTime_25_Over_Percentage = model.HomeTeamPerformanceAtHome.FT_25_Over,
                            FullTime_35_Over_Percentage = model.HomeTeamPerformanceAtHome.FT_35_Over,
                            HalfTime_05_Over_Percentage = model.HomeTeamPerformanceAtHome.HT_05_Over,
                            HalfTime_15_Over_Percentage = model.HomeTeamPerformanceAtHome.HT_15_Over,
                            SecondHalf_05_Over_Percentage = model.HomeTeamPerformanceAtHome.SH_05_Over,
                            SecondHalf_15_Over_Percentage = model.HomeTeamPerformanceAtHome.SH_15_Over,
                            FullTime_BothTeamToScore_Percentage = model.HomeTeamPerformanceAtHome.FT_GG,
                            HalfTime_BothTeamToScore_Percentage = model.HomeTeamPerformanceAtHome.HT_GG,
                            SecondHalf_BothTeamToScore_Percentage = model.HomeTeamPerformanceAtHome.SH_GG,
                            FullTime_Win_Percentage = model.HomeTeamPerformanceAtHome.Is_FT_Win,
                            FullTime_Draw_Percentage = model.HomeTeamPerformanceAtHome.Is_FT_X,
                            HalfTime_Win_Percentage = model.HomeTeamPerformanceAtHome.Is_HT_Win,
                            HalfTime_Draw_Percentage = model.HomeTeamPerformanceAtHome.Is_HT_X,
                            SecondHalf_Win_Percentage = model.HomeTeamPerformanceAtHome.Is_SH_Win,
                            SecondHalf_Draw_Percentage = model.HomeTeamPerformanceAtHome.Is_SH_X,
                            FullTime_Corner_75_Over_Percentage = model.HomeTeamPerformanceAtHome.FT_Corner_75_Over,
                            FullTime_Corner_85_Over_Percentage = model.HomeTeamPerformanceAtHome.FT_Corner_85_Over,
                            FullTime_Corner_95_Over_Percentage = model.HomeTeamPerformanceAtHome.FT_Corner_95_Over,
                            FullTime_Corner_Win_Percentage = model.HomeTeamPerformanceAtHome.Is_FT_CornerWinTeam,
                            FullTime_Corner_Draw_Percentage = model.HomeTeamPerformanceAtHome.Is_FT_CornerX,
                            Individual_FullTime_05_Over_Percentage = model.HomeTeamPerformanceAtHome.Team_FT_05_Over,
                            Individual_FullTime_15_Over_Percentage = model.HomeTeamPerformanceAtHome.Team_FT_15_Over,
                            Individual_HalfTime_05_Over_Percentage = model.HomeTeamPerformanceAtHome.Team_HT_05_Over,
                            Individual_HalfTime_15_Over_Percentage = model.HomeTeamPerformanceAtHome.Team_HT_15_Over,
                            Individual_SecondHalf_05_Over_Percentage = model.HomeTeamPerformanceAtHome.Team_SH_05_Over,
                            Individual_SecondHalf_15_Over_Percentage = model.HomeTeamPerformanceAtHome.Team_SH_15_Over,
                            Individual_FullTime_Corner_35_Over_Percentage = model.HomeTeamPerformanceAtHome.Team_FT_Corner_35_Over,
                            Individual_FullTime_Corner_45_Over_Percentage = model.HomeTeamPerformanceAtHome.Team_FT_Corner_45_Over,
                            Individual_Win_Any_Half_Percentage = model.HomeTeamPerformanceAtHome.Team_Win_Any_Half
                        },
                        Last_10_Matches_General = new PerformanceAiStatisticsHolder
                        {
                            Individual_Average_FullTime_Goals = model.HomeTeamPerformanceGeneral.Average_FT_Goals_Team,
                            Individual_Average_HalfTime_Goals = model.HomeTeamPerformanceGeneral.Average_HT_Goals_Team,
                            Individual_Average_SecondHalf_Goals = model.HomeTeamPerformanceGeneral.Average_SH_Goals_Team,
                            Individual_Average_FullTime_Corners = model.HomeTeamPerformanceGeneral.Average_FT_Corners_Team,
                            Individual_Average_FullTime_Shot = model.HomeTeamPerformanceGeneral.Average_FT_Shut_Team,
                            Individual_Average_FullTime_ShotOnTarget = model.HomeTeamPerformanceGeneral.Average_FT_ShutOnTarget_Team,
                            Individual_Average_Possesion_Of_Ball = model.HomeTeamPerformanceGeneral.Team_Possesion,
                            FullTime_15_Over_Percentage = model.HomeTeamPerformanceGeneral.FT_15_Over,
                            FullTime_25_Over_Percentage = model.HomeTeamPerformanceGeneral.FT_25_Over,
                            FullTime_35_Over_Percentage = model.HomeTeamPerformanceGeneral.FT_35_Over,
                            HalfTime_05_Over_Percentage = model.HomeTeamPerformanceGeneral.HT_05_Over,
                            HalfTime_15_Over_Percentage = model.HomeTeamPerformanceGeneral.HT_15_Over,
                            SecondHalf_05_Over_Percentage = model.HomeTeamPerformanceGeneral.SH_05_Over,
                            SecondHalf_15_Over_Percentage = model.HomeTeamPerformanceGeneral.SH_15_Over,
                            FullTime_BothTeamToScore_Percentage = model.HomeTeamPerformanceGeneral.FT_GG,
                            HalfTime_BothTeamToScore_Percentage = model.HomeTeamPerformanceGeneral.HT_GG,
                            SecondHalf_BothTeamToScore_Percentage = model.HomeTeamPerformanceGeneral.SH_GG,
                            FullTime_Win_Percentage = model.HomeTeamPerformanceGeneral.Is_FT_Win,
                            FullTime_Draw_Percentage = model.HomeTeamPerformanceGeneral.Is_FT_X,
                            HalfTime_Win_Percentage = model.HomeTeamPerformanceGeneral.Is_HT_Win,
                            HalfTime_Draw_Percentage = model.HomeTeamPerformanceGeneral.Is_HT_X,
                            SecondHalf_Win_Percentage = model.HomeTeamPerformanceGeneral.Is_SH_Win,
                            SecondHalf_Draw_Percentage = model.HomeTeamPerformanceGeneral.Is_SH_X,
                            FullTime_Corner_75_Over_Percentage = model.HomeTeamPerformanceGeneral.FT_Corner_75_Over,
                            FullTime_Corner_85_Over_Percentage = model.HomeTeamPerformanceGeneral.FT_Corner_85_Over,
                            FullTime_Corner_95_Over_Percentage = model.HomeTeamPerformanceGeneral.FT_Corner_95_Over,
                            FullTime_Corner_Win_Percentage = model.HomeTeamPerformanceGeneral.Is_FT_CornerWinTeam,
                            FullTime_Corner_Draw_Percentage = model.HomeTeamPerformanceGeneral.Is_FT_CornerX,
                            Individual_FullTime_05_Over_Percentage = model.HomeTeamPerformanceGeneral.Team_FT_05_Over,
                            Individual_FullTime_15_Over_Percentage = model.HomeTeamPerformanceGeneral.Team_FT_15_Over,
                            Individual_HalfTime_05_Over_Percentage = model.HomeTeamPerformanceGeneral.Team_HT_05_Over,
                            Individual_HalfTime_15_Over_Percentage = model.HomeTeamPerformanceGeneral.Team_HT_15_Over,
                            Individual_SecondHalf_05_Over_Percentage = model.HomeTeamPerformanceGeneral.Team_SH_05_Over,
                            Individual_SecondHalf_15_Over_Percentage = model.HomeTeamPerformanceGeneral.Team_SH_15_Over,
                            Individual_FullTime_Corner_35_Over_Percentage = model.HomeTeamPerformanceGeneral.Team_FT_Corner_35_Over,
                            Individual_FullTime_Corner_45_Over_Percentage = model.HomeTeamPerformanceGeneral.Team_FT_Corner_45_Over,
                            Individual_Win_Any_Half_Percentage = model.HomeTeamPerformanceGeneral.Team_Win_Any_Half
                        }
                    },
                    AwayTeam = new AwayTeamAiPerformanceHolder
                    {
                        Last_6_Matches_AtAway = new PerformanceAiStatisticsHolder
                        {
                            Individual_Average_FullTime_Goals = model.AwayTeamPerformanceAtAway.Average_FT_Goals_Team,
                            Individual_Average_HalfTime_Goals = model.AwayTeamPerformanceAtAway.Average_HT_Goals_Team,
                            Individual_Average_SecondHalf_Goals = model.AwayTeamPerformanceAtAway.Average_SH_Goals_Team,
                            Individual_Average_FullTime_Corners = model.AwayTeamPerformanceAtAway.Average_FT_Corners_Team,
                            Individual_Average_FullTime_Shot = model.AwayTeamPerformanceAtAway.Average_FT_Shut_Team,
                            Individual_Average_FullTime_ShotOnTarget = model.AwayTeamPerformanceAtAway.Average_FT_ShutOnTarget_Team,
                            Individual_Average_Possesion_Of_Ball = model.AwayTeamPerformanceAtAway.Team_Possesion,
                            FullTime_15_Over_Percentage = model.AwayTeamPerformanceAtAway.FT_15_Over,
                            FullTime_25_Over_Percentage = model.AwayTeamPerformanceAtAway.FT_25_Over,
                            FullTime_35_Over_Percentage = model.AwayTeamPerformanceAtAway.FT_35_Over,
                            HalfTime_05_Over_Percentage = model.AwayTeamPerformanceAtAway.HT_05_Over,
                            HalfTime_15_Over_Percentage = model.AwayTeamPerformanceAtAway.HT_15_Over,
                            SecondHalf_05_Over_Percentage = model.AwayTeamPerformanceAtAway.SH_05_Over,
                            SecondHalf_15_Over_Percentage = model.AwayTeamPerformanceAtAway.SH_15_Over,
                            FullTime_BothTeamToScore_Percentage = model.AwayTeamPerformanceAtAway.FT_GG,
                            HalfTime_BothTeamToScore_Percentage = model.AwayTeamPerformanceAtAway.HT_GG,
                            SecondHalf_BothTeamToScore_Percentage = model.AwayTeamPerformanceAtAway.SH_GG,
                            FullTime_Win_Percentage = model.AwayTeamPerformanceAtAway.Is_FT_Win,
                            FullTime_Draw_Percentage = model.AwayTeamPerformanceAtAway.Is_FT_X,
                            HalfTime_Win_Percentage = model.AwayTeamPerformanceAtAway.Is_HT_Win,
                            HalfTime_Draw_Percentage = model.AwayTeamPerformanceAtAway.Is_HT_X,
                            SecondHalf_Win_Percentage = model.AwayTeamPerformanceAtAway.Is_SH_Win,
                            SecondHalf_Draw_Percentage = model.AwayTeamPerformanceAtAway.Is_SH_X,
                            FullTime_Corner_75_Over_Percentage = model.AwayTeamPerformanceAtAway.FT_Corner_75_Over,
                            FullTime_Corner_85_Over_Percentage = model.AwayTeamPerformanceAtAway.FT_Corner_85_Over,
                            FullTime_Corner_95_Over_Percentage = model.AwayTeamPerformanceAtAway.FT_Corner_95_Over,
                            FullTime_Corner_Win_Percentage = model.AwayTeamPerformanceAtAway.Is_FT_CornerWinTeam,
                            FullTime_Corner_Draw_Percentage = model.AwayTeamPerformanceAtAway.Is_FT_CornerX,
                            Individual_FullTime_05_Over_Percentage = model.AwayTeamPerformanceAtAway.Team_FT_05_Over,
                            Individual_FullTime_15_Over_Percentage = model.AwayTeamPerformanceAtAway.Team_FT_15_Over,
                            Individual_HalfTime_05_Over_Percentage = model.AwayTeamPerformanceAtAway.Team_HT_05_Over,
                            Individual_HalfTime_15_Over_Percentage = model.AwayTeamPerformanceAtAway.Team_HT_15_Over,
                            Individual_SecondHalf_05_Over_Percentage = model.AwayTeamPerformanceAtAway.Team_SH_05_Over,
                            Individual_SecondHalf_15_Over_Percentage = model.AwayTeamPerformanceAtAway.Team_SH_15_Over,
                            Individual_FullTime_Corner_35_Over_Percentage = model.AwayTeamPerformanceAtAway.Team_FT_Corner_35_Over,
                            Individual_FullTime_Corner_45_Over_Percentage = model.AwayTeamPerformanceAtAway.Team_FT_Corner_45_Over,
                            Individual_Win_Any_Half_Percentage = model.AwayTeamPerformanceAtAway.Team_Win_Any_Half
                        },
                        Last_10_Matches_General = new PerformanceAiStatisticsHolder
                        {
                            Individual_Average_FullTime_Goals = model.AwayTeamPerformanceGeneral.Average_FT_Goals_Team,
                            Individual_Average_HalfTime_Goals = model.AwayTeamPerformanceGeneral.Average_HT_Goals_Team,
                            Individual_Average_SecondHalf_Goals = model.AwayTeamPerformanceGeneral.Average_SH_Goals_Team,
                            Individual_Average_FullTime_Corners = model.AwayTeamPerformanceGeneral.Average_FT_Corners_Team,
                            Individual_Average_FullTime_Shot = model.AwayTeamPerformanceGeneral.Average_FT_Shut_Team,
                            Individual_Average_FullTime_ShotOnTarget = model.AwayTeamPerformanceGeneral.Average_FT_ShutOnTarget_Team,
                            Individual_Average_Possesion_Of_Ball = model.AwayTeamPerformanceGeneral.Team_Possesion,
                            FullTime_15_Over_Percentage = model.AwayTeamPerformanceGeneral.FT_15_Over,
                            FullTime_25_Over_Percentage = model.AwayTeamPerformanceGeneral.FT_25_Over,
                            FullTime_35_Over_Percentage = model.AwayTeamPerformanceGeneral.FT_35_Over,
                            HalfTime_05_Over_Percentage = model.AwayTeamPerformanceGeneral.HT_05_Over,
                            HalfTime_15_Over_Percentage = model.AwayTeamPerformanceGeneral.HT_15_Over,
                            SecondHalf_05_Over_Percentage = model.AwayTeamPerformanceGeneral.SH_05_Over,
                            SecondHalf_15_Over_Percentage = model.AwayTeamPerformanceGeneral.SH_15_Over,
                            FullTime_BothTeamToScore_Percentage = model.AwayTeamPerformanceGeneral.FT_GG,
                            HalfTime_BothTeamToScore_Percentage = model.AwayTeamPerformanceGeneral.HT_GG,
                            SecondHalf_BothTeamToScore_Percentage = model.AwayTeamPerformanceGeneral.SH_GG,
                            FullTime_Win_Percentage = model.AwayTeamPerformanceGeneral.Is_FT_Win,
                            FullTime_Draw_Percentage = model.AwayTeamPerformanceGeneral.Is_FT_X,
                            HalfTime_Win_Percentage = model.AwayTeamPerformanceGeneral.Is_HT_Win,
                            HalfTime_Draw_Percentage = model.AwayTeamPerformanceGeneral.Is_HT_X,
                            SecondHalf_Win_Percentage = model.AwayTeamPerformanceGeneral.Is_SH_Win,
                            SecondHalf_Draw_Percentage = model.AwayTeamPerformanceGeneral.Is_SH_X,
                            FullTime_Corner_75_Over_Percentage = model.AwayTeamPerformanceGeneral.FT_Corner_75_Over,
                            FullTime_Corner_85_Over_Percentage = model.AwayTeamPerformanceGeneral.FT_Corner_85_Over,
                            FullTime_Corner_95_Over_Percentage = model.AwayTeamPerformanceGeneral.FT_Corner_95_Over,
                            FullTime_Corner_Win_Percentage = model.AwayTeamPerformanceGeneral.Is_FT_CornerWinTeam,
                            FullTime_Corner_Draw_Percentage = model.AwayTeamPerformanceGeneral.Is_FT_CornerX,
                            Individual_FullTime_05_Over_Percentage = model.AwayTeamPerformanceGeneral.Team_FT_05_Over,
                            Individual_FullTime_15_Over_Percentage = model.AwayTeamPerformanceGeneral.Team_FT_15_Over,
                            Individual_HalfTime_05_Over_Percentage = model.AwayTeamPerformanceGeneral.Team_HT_05_Over,
                            Individual_HalfTime_15_Over_Percentage = model.AwayTeamPerformanceGeneral.Team_HT_15_Over,
                            Individual_SecondHalf_05_Over_Percentage = model.AwayTeamPerformanceGeneral.Team_SH_05_Over,
                            Individual_SecondHalf_15_Over_Percentage = model.AwayTeamPerformanceGeneral.Team_SH_15_Over,
                            Individual_FullTime_Corner_35_Over_Percentage = model.AwayTeamPerformanceGeneral.Team_FT_Corner_35_Over,
                            Individual_FullTime_Corner_45_Over_Percentage = model.AwayTeamPerformanceGeneral.Team_FT_Corner_45_Over,
                            Individual_Win_Any_Half_Percentage = model.AwayTeamPerformanceGeneral.Team_Win_Any_Half
                        }
                    }
                }
            };
        }


        public static List<TimerMatchModel> Group(this List<MatchIdentifier> models)
        {
            return models.GroupBy(x=>x.MatchDateTime)
                .Select(x => new TimerMatchModel
                {
                    Hour = x.Key.Hour,
                    Minute = x.Key.Minute,
                    MatchTime = x.Key.ToString("HH:mm"),
                    Serials = x.Select(y => y.Serial).ToList()
                }).ToList();
        }
    }
}

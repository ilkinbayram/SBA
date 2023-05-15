using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete.ComplexModels.ML;
using Core.Entities.Concrete.ComplexModels.RequestModelHelpers;
using Core.Entities.Concrete.ComplexModels.Sql;
using Core.Entities.Concrete.ComplexModels.SqlModelHelpers;
using Core.Entities.Concrete.ExternalDbEntities;
using Core.Resources.Enums;
using SBA.ExternalDataAccess.Abstract;
using SBA.ExternalDataAccess.Concrete.EntityFramework.Contexts;

namespace SBA.ExternalDataAccess.Concrete
{
    public class EfLeagueStatisticsHolderDal : EfEntityRepositoryBase<LeagueStatisticsHolder, ExternalAppDbContext>, ILeagueStatisticsHolderDal
    {
        public EfLeagueStatisticsHolderDal(ExternalAppDbContext applicationContext) : base(applicationContext)
        {
        }

        public MatchLeagueComplexDto GetAiComplexStatistics(int serial)
        {
            var result = (from mid in Context.MatchIdentifiers
                          join cmpHA in Context.ComparisonStatisticsHolders on mid.Id equals cmpHA.MatchIdentifierId
                          where cmpHA.BySideType == (int)BySideType.HomeAway
                          join cmpGN in Context.ComparisonStatisticsHolders on mid.Id equals cmpGN.MatchIdentifierId
                          where cmpGN.BySideType == (int)BySideType.General
                          join prfHomeHA in Context.TeamPerformanceStatisticsHolders on mid.Id equals prfHomeHA.MatchIdentifierId
                          where prfHomeHA.BySideType == (int)BySideType.HomeAway && prfHomeHA.HomeOrAway == (int)HomeOrAway.Home
                          join prfAwayHA in Context.TeamPerformanceStatisticsHolders on mid.Id equals prfAwayHA.MatchIdentifierId
                          where prfAwayHA.BySideType == (int)BySideType.HomeAway && prfAwayHA.HomeOrAway == (int)HomeOrAway.Away
                          join prfHomeGN in Context.TeamPerformanceStatisticsHolders on mid.Id equals prfHomeGN.MatchIdentifierId
                          where prfHomeGN.BySideType == (int)BySideType.General && prfHomeGN.HomeOrAway == (int)HomeOrAway.Home
                          join prfAwayGN in Context.TeamPerformanceStatisticsHolders on mid.Id equals prfAwayGN.MatchIdentifierId
                          where prfAwayGN.BySideType == (int)BySideType.General && prfAwayGN.HomeOrAway == (int)HomeOrAway.Away
                          join lg in Context.LeagueStatisticsHolders on cmpGN.LeagueStaisticsHolderId equals lg.Id
                          where mid.Serial == serial
                          select new MatchLeagueComplexDto
                          {
                              Match = new MatchModelDto
                              {
                                  Country = lg.CountryName,
                                  League = lg.LeagueName,
                                  AwayTeam = mid.AwayTeam,
                                  HomeTeam = mid.HomeTeam,
                                  Match = $"{mid.HomeTeam} - {mid.AwayTeam}"
                              },
                              ComparisonHomeAway = new ComparisonModelDto
                              {
                                  Average_FT_Goals_HomeTeam = cmpHA.Average_FT_Goals_HomeTeam,
                                  Average_FT_Goals_AwayTeam = cmpHA.Average_FT_Goals_AwayTeam,
                                  Average_HT_Goals_HomeTeam = cmpHA.Average_HT_Goals_HomeTeam,
                                  Average_HT_Goals_AwayTeam = cmpHA.Average_HT_Goals_AwayTeam,
                                  Average_SH_Goals_HomeTeam = cmpHA.Average_SH_Goals_HomeTeam,
                                  Average_SH_Goals_AwayTeam = cmpHA.Average_SH_Goals_AwayTeam,
                                  Away_FT_05_Over = cmpHA.Away_FT_05_Over,
                                  Away_FT_15_Over = cmpHA.Away_FT_15_Over,
                                  Away_HT_05_Over = cmpHA.Away_HT_05_Over,
                                  Away_HT_15_Over = cmpHA.Away_HT_15_Over,
                                  Away_SH_05_Over = cmpHA.Away_SH_05_Over,
                                  Away_SH_15_Over = cmpHA.Away_SH_15_Over,
                                  Home_FT_05_Over = cmpHA.Home_FT_05_Over,
                                  Home_FT_15_Over = cmpHA.Home_FT_15_Over,
                                  Home_HT_05_Over = cmpHA.Home_HT_05_Over,
                                  Home_HT_15_Over = cmpHA.Home_HT_15_Over,
                                  Home_SH_05_Over = cmpHA.Home_SH_05_Over,
                                  Home_SH_15_Over = cmpHA.Home_SH_15_Over,
                                  Away_Win_Any_Half = cmpHA.Away_Win_Any_Half,
                                  Home_Win_Any_Half = cmpHA.Home_Win_Any_Half,
                                  FT_15_Over = cmpHA.FT_15_Over,
                                  FT_25_Over = cmpHA.FT_25_Over,
                                  FT_35_Over = cmpHA.FT_35_Over,
                                  HT_05_Over = cmpHA.HT_05_Over,
                                  HT_15_Over = cmpHA.HT_15_Over,
                                  SH_05_Over = cmpHA.SH_05_Over,
                                  SH_15_Over = cmpHA.SH_15_Over,
                                  FT_GG = cmpHA.FT_GG,
                                  HT_GG = cmpHA.HT_GG,
                                  SH_GG = cmpHA.SH_GG,
                                  Is_FT_Win1 = cmpHA.Is_FT_Win1,
                                  Is_FT_X = cmpHA.Is_FT_X,
                                  Is_FT_Win2 = cmpHA.Is_FT_Win2,
                                  Is_HT_Win1 = cmpHA.Is_HT_Win1,
                                  Is_HT_X = cmpHA.Is_HT_X,
                                  Is_HT_Win2 = cmpHA.Is_HT_Win2,
                                  Is_SH_Win1 = cmpHA.Is_SH_Win1,
                                  Is_SH_X = cmpHA.Is_SH_X,
                                  Is_SH_Win2 = cmpHA.Is_SH_Win2,

                                  Average_FT_Corners_AwayTeam = cmpHA.Average_FT_Corners_AwayTeam,
                                  Average_FT_Corners_HomeTeam = cmpHA.Average_FT_Corners_HomeTeam,
                                  Average_FT_ShutOnTarget_AwayTeam = cmpHA.Average_FT_ShutOnTarget_AwayTeam,
                                  Average_FT_ShutOnTarget_HomeTeam = cmpHA.Average_FT_ShutOnTarget_HomeTeam,
                                  Average_FT_Shut_AwayTeam = cmpHA.Average_FT_Shut_AwayTeam,
                                  Average_FT_Shut_HomeTeam = cmpHA.Average_FT_Shut_HomeTeam,
                                  Away_FT_Corner_35_Over = cmpHA.Corner_Away_3_5_Over,
                                  Away_FT_Corner_45_Over = cmpHA.Corner_Away_4_5_Over,
                                  Home_FT_Corner_35_Over = cmpHA.Corner_Home_3_5_Over,
                                  Home_FT_Corner_45_Over = cmpHA.Corner_Home_4_5_Over,
                                  FT_Corner_75_Over = cmpHA.Corner_7_5_Over,
                                  FT_Corner_85_Over = cmpHA.Corner_8_5_Over,
                                  FT_Corner_95_Over = cmpHA.Corner_9_5_Over,
                                  Is_FT_CornerWin1 = cmpHA.Is_Corner_FT_Win1,
                                  Is_FT_CornerX = cmpHA.Is_Corner_FT_X,
                                  Is_FT_CornerWin2 = cmpHA.Is_Corner_FT_Win2,
                                  Home_Possesion = cmpHA.Home_Possesion,
                                  Away_Possesion = cmpHA.Away_Possesion
                              },
                              ComparisonGeneral = new ComparisonModelDto
                              {
                                  Average_FT_Goals_HomeTeam = cmpGN.Average_FT_Goals_HomeTeam,
                                  Average_FT_Goals_AwayTeam = cmpGN.Average_FT_Goals_AwayTeam,
                                  Average_HT_Goals_HomeTeam = cmpGN.Average_HT_Goals_HomeTeam,
                                  Average_HT_Goals_AwayTeam = cmpGN.Average_HT_Goals_AwayTeam,
                                  Average_SH_Goals_HomeTeam = cmpGN.Average_SH_Goals_HomeTeam,
                                  Average_SH_Goals_AwayTeam = cmpGN.Average_SH_Goals_AwayTeam,
                                  Away_FT_05_Over = cmpGN.Away_FT_05_Over,
                                  Away_FT_15_Over = cmpGN.Away_FT_15_Over,
                                  Away_HT_05_Over = cmpGN.Away_HT_05_Over,
                                  Away_HT_15_Over = cmpGN.Away_HT_15_Over,
                                  Away_SH_05_Over = cmpGN.Away_SH_05_Over,
                                  Away_SH_15_Over = cmpGN.Away_SH_15_Over,
                                  Home_FT_05_Over = cmpGN.Home_FT_05_Over,
                                  Home_FT_15_Over = cmpGN.Home_FT_15_Over,
                                  Home_HT_05_Over = cmpGN.Home_HT_05_Over,
                                  Home_HT_15_Over = cmpGN.Home_HT_15_Over,
                                  Home_SH_05_Over = cmpGN.Home_SH_05_Over,
                                  Home_SH_15_Over = cmpGN.Home_SH_15_Over,
                                  Away_Win_Any_Half = cmpGN.Away_Win_Any_Half,
                                  Home_Win_Any_Half = cmpGN.Home_Win_Any_Half,
                                  FT_15_Over = cmpGN.FT_15_Over,
                                  FT_25_Over = cmpGN.FT_25_Over,
                                  FT_35_Over = cmpGN.FT_35_Over,
                                  HT_05_Over = cmpGN.HT_05_Over,
                                  HT_15_Over = cmpGN.HT_15_Over,
                                  SH_05_Over = cmpGN.SH_05_Over,
                                  SH_15_Over = cmpGN.SH_15_Over,
                                  FT_GG = cmpGN.FT_GG,
                                  HT_GG = cmpGN.HT_GG,
                                  SH_GG = cmpGN.SH_GG,
                                  Is_FT_Win1 = cmpGN.Is_FT_Win1,
                                  Is_FT_X = cmpGN.Is_FT_X,
                                  Is_FT_Win2 = cmpGN.Is_FT_Win2,
                                  Is_HT_Win1 = cmpGN.Is_HT_Win1,
                                  Is_HT_X = cmpGN.Is_HT_X,
                                  Is_HT_Win2 = cmpGN.Is_HT_Win2,
                                  Is_SH_Win1 = cmpGN.Is_SH_Win1,
                                  Is_SH_X = cmpGN.Is_SH_X,
                                  Is_SH_Win2 = cmpGN.Is_SH_Win2,

                                  Average_FT_Corners_AwayTeam = cmpGN.Average_FT_Corners_AwayTeam,
                                  Average_FT_Corners_HomeTeam = cmpGN.Average_FT_Corners_HomeTeam,
                                  Average_FT_ShutOnTarget_AwayTeam = cmpGN.Average_FT_ShutOnTarget_AwayTeam,
                                  Average_FT_ShutOnTarget_HomeTeam = cmpGN.Average_FT_ShutOnTarget_HomeTeam,
                                  Average_FT_Shut_AwayTeam = cmpGN.Average_FT_Shut_AwayTeam,
                                  Average_FT_Shut_HomeTeam = cmpGN.Average_FT_Shut_HomeTeam,
                                  Away_FT_Corner_35_Over = cmpGN.Corner_Away_3_5_Over,
                                  Away_FT_Corner_45_Over = cmpGN.Corner_Away_4_5_Over,
                                  Home_FT_Corner_35_Over = cmpGN.Corner_Home_3_5_Over,
                                  Home_FT_Corner_45_Over = cmpGN.Corner_Home_4_5_Over,
                                  FT_Corner_75_Over = cmpGN.Corner_7_5_Over,
                                  FT_Corner_85_Over = cmpGN.Corner_8_5_Over,
                                  FT_Corner_95_Over = cmpGN.Corner_9_5_Over,
                                  Is_FT_CornerWin1 = cmpGN.Is_Corner_FT_Win1,
                                  Is_FT_CornerX = cmpGN.Is_Corner_FT_X,
                                  Is_FT_CornerWin2 = cmpGN.Is_Corner_FT_Win2,
                                  Home_Possesion = cmpGN.Home_Possesion,
                                  Away_Possesion = cmpGN.Away_Possesion
                              },
                              HomeTeamPerformanceAtHome = new PerformanceModelDto
                              {
                                  Average_FT_Goals_Team = prfHomeHA.Average_FT_Goals_Team,
                                  Average_HT_Goals_Team = prfHomeHA.Average_HT_Goals_Team,
                                  Average_SH_Goals_Team = prfHomeHA.Average_SH_Goals_Team,
                                  Average_FT_Conceded_Goals_Team = prfHomeHA.Average_FT_Conceded_Goals_Team,
                                  Average_HT_Conceded_Goals_Team = prfHomeHA.Average_HT_Conceded_Goals_Team,
                                  Average_SH_Conceded_Goals_Team = prfHomeHA.Average_SH_Conceded_Goals_Team,
                                  Average_FT_GK_Saves_Team = prfHomeHA.Average_FT_GK_Saves_Team,

                                  FT_15_Over = prfHomeHA.FT_15_Over,
                                  FT_25_Over = prfHomeHA.FT_25_Over,
                                  FT_35_Over = prfHomeHA.FT_35_Over,
                                  HT_05_Over = prfHomeHA.HT_05_Over,
                                  HT_15_Over = prfHomeHA.HT_15_Over,
                                  SH_05_Over = prfHomeHA.SH_05_Over,
                                  SH_15_Over = prfHomeHA.SH_15_Over,
                                  Team_FT_05_Over = prfHomeHA.Team_FT_05_Over,
                                  Team_FT_15_Over = prfHomeHA.Team_FT_15_Over,
                                  Team_HT_05_Over = prfHomeHA.Team_HT_05_Over,
                                  Team_HT_15_Over = prfHomeHA.Team_HT_15_Over,
                                  Team_SH_05_Over = prfHomeHA.Team_SH_05_Over,
                                  Team_SH_15_Over = prfHomeHA.Team_SH_15_Over,
                                  Team_Win_Any_Half = prfHomeHA.Team_Win_Any_Half,
                                  FT_GG = prfHomeHA.FT_GG,
                                  HT_GG = prfHomeHA.HT_GG,
                                  SH_GG = prfHomeHA.SH_GG,
                                  Is_FT_Win = prfHomeHA.Is_FT_Win,
                                  Is_FT_X = prfHomeHA.Is_FT_X,
                                  Is_HT_Win = prfHomeHA.Is_HT_Win,
                                  Is_HT_X = prfHomeHA.Is_HT_X,
                                  Is_SH_Win = prfHomeHA.Is_SH_Win,
                                  Is_SH_X = prfHomeHA.Is_SH_X,

                                  Average_FT_Corners_Team = prfHomeHA.Average_FT_Corners_Team,
                                  Average_FT_ShutOnTarget_Team = prfHomeHA.Average_FT_ShutOnTarget_Team,
                                  Average_FT_Shut_Team = prfHomeHA.Average_FT_Shut_Team,
                                  Team_FT_Corner_35_Over = prfHomeHA.Corner_Team_3_5_Over,
                                  Team_FT_Corner_45_Over = prfHomeHA.Corner_Team_4_5_Over,
                                  FT_Corner_75_Over = prfHomeHA.Corner_7_5_Over,
                                  FT_Corner_85_Over = prfHomeHA.Corner_8_5_Over,
                                  FT_Corner_95_Over = prfHomeHA.Corner_9_5_Over,
                                  Is_FT_CornerWinTeam = prfHomeHA.Is_Corner_FT_Win,
                                  Is_FT_CornerX = prfHomeHA.Is_Corner_FT_X,
                                  Team_Possesion = prfHomeHA.Team_Possesion
                              },
                              AwayTeamPerformanceAtAway = new PerformanceModelDto
                              {
                                  Average_FT_Goals_Team = prfAwayHA.Average_FT_Goals_Team,
                                  Average_HT_Goals_Team = prfAwayHA.Average_HT_Goals_Team,
                                  Average_SH_Goals_Team = prfAwayHA.Average_SH_Goals_Team,
                                  Average_FT_Conceded_Goals_Team = prfAwayHA.Average_FT_Conceded_Goals_Team,
                                  Average_HT_Conceded_Goals_Team = prfAwayHA.Average_HT_Conceded_Goals_Team,
                                  Average_SH_Conceded_Goals_Team = prfAwayHA.Average_SH_Conceded_Goals_Team,
                                  FT_15_Over = prfAwayHA.FT_15_Over,
                                  FT_25_Over = prfAwayHA.FT_25_Over,
                                  FT_35_Over = prfAwayHA.FT_35_Over,
                                  HT_05_Over = prfAwayHA.HT_05_Over,
                                  HT_15_Over = prfAwayHA.HT_15_Over,
                                  SH_05_Over = prfAwayHA.SH_05_Over,
                                  SH_15_Over = prfAwayHA.SH_15_Over,
                                  Team_FT_05_Over = prfAwayHA.Team_FT_05_Over,
                                  Team_FT_15_Over = prfAwayHA.Team_FT_15_Over,
                                  Team_HT_05_Over = prfAwayHA.Team_HT_05_Over,
                                  Team_HT_15_Over = prfAwayHA.Team_HT_15_Over,
                                  Team_SH_05_Over = prfAwayHA.Team_SH_05_Over,
                                  Team_SH_15_Over = prfAwayHA.Team_SH_15_Over,
                                  Team_Win_Any_Half = prfAwayHA.Team_Win_Any_Half,
                                  FT_GG = prfAwayHA.FT_GG,
                                  HT_GG = prfAwayHA.HT_GG,
                                  SH_GG = prfAwayHA.SH_GG,
                                  Is_FT_Win = prfAwayHA.Is_FT_Win,
                                  Is_FT_X = prfAwayHA.Is_FT_X,
                                  Is_HT_Win = prfAwayHA.Is_HT_Win,
                                  Is_HT_X = prfAwayHA.Is_HT_X,
                                  Is_SH_Win = prfAwayHA.Is_SH_Win,
                                  Is_SH_X = prfAwayHA.Is_SH_X,

                                  Average_FT_Corners_Team = prfAwayHA.Average_FT_Corners_Team,
                                  Average_FT_GK_Saves_Team = prfAwayHA.Average_FT_GK_Saves_Team,
                                  Average_FT_ShutOnTarget_Team = prfAwayHA.Average_FT_ShutOnTarget_Team,
                                  Average_FT_Shut_Team = prfAwayHA.Average_FT_Shut_Team,
                                  Team_FT_Corner_35_Over = prfAwayHA.Corner_Team_3_5_Over,
                                  Team_FT_Corner_45_Over = prfAwayHA.Corner_Team_4_5_Over,
                                  FT_Corner_75_Over = prfAwayHA.Corner_7_5_Over,
                                  FT_Corner_85_Over = prfAwayHA.Corner_8_5_Over,
                                  FT_Corner_95_Over = prfAwayHA.Corner_9_5_Over,
                                  Is_FT_CornerWinTeam = prfAwayHA.Is_Corner_FT_Win,
                                  Is_FT_CornerX = prfAwayHA.Is_Corner_FT_X,
                                  Team_Possesion = prfAwayHA.Team_Possesion
                              },
                              HomeTeamPerformanceGeneral = new PerformanceModelDto
                              {
                                  Average_FT_Goals_Team = prfHomeGN.Average_FT_Goals_Team,
                                  Average_HT_Goals_Team = prfHomeGN.Average_HT_Goals_Team,
                                  Average_SH_Goals_Team = prfHomeGN.Average_SH_Goals_Team,
                                  Average_FT_Conceded_Goals_Team = prfHomeGN.Average_FT_Conceded_Goals_Team,
                                  Average_HT_Conceded_Goals_Team = prfHomeGN.Average_HT_Conceded_Goals_Team,
                                  Average_SH_Conceded_Goals_Team = prfHomeGN.Average_SH_Conceded_Goals_Team,
                                  FT_15_Over = prfHomeGN.FT_15_Over,
                                  FT_25_Over = prfHomeGN.FT_25_Over,
                                  FT_35_Over = prfHomeGN.FT_35_Over,
                                  HT_05_Over = prfHomeGN.HT_05_Over,
                                  HT_15_Over = prfHomeGN.HT_15_Over,
                                  SH_05_Over = prfHomeGN.SH_05_Over,
                                  SH_15_Over = prfHomeGN.SH_15_Over,
                                  Team_FT_05_Over = prfHomeGN.Team_FT_05_Over,
                                  Team_FT_15_Over = prfHomeGN.Team_FT_15_Over,
                                  Team_HT_05_Over = prfHomeGN.Team_HT_05_Over,
                                  Team_HT_15_Over = prfHomeGN.Team_HT_15_Over,
                                  Team_SH_05_Over = prfHomeGN.Team_SH_05_Over,
                                  Team_SH_15_Over = prfHomeGN.Team_SH_15_Over,
                                  Team_Win_Any_Half = prfHomeGN.Team_Win_Any_Half,
                                  FT_GG = prfHomeGN.FT_GG,
                                  HT_GG = prfHomeGN.HT_GG,
                                  SH_GG = prfHomeGN.SH_GG,
                                  Is_FT_Win = prfHomeGN.Is_FT_Win,
                                  Is_FT_X = prfHomeGN.Is_FT_X,
                                  Is_HT_Win = prfHomeGN.Is_HT_Win,
                                  Is_HT_X = prfHomeGN.Is_HT_X,
                                  Is_SH_Win = prfHomeGN.Is_SH_Win,
                                  Is_SH_X = prfHomeGN.Is_SH_X,

                                  Average_FT_Corners_Team = prfHomeGN.Average_FT_Corners_Team,
                                  Average_FT_GK_Saves_Team = prfHomeGN.Average_FT_GK_Saves_Team,
                                  Average_FT_ShutOnTarget_Team = prfHomeGN.Average_FT_ShutOnTarget_Team,
                                  Average_FT_Shut_Team = prfHomeGN.Average_FT_Shut_Team,
                                  Team_FT_Corner_35_Over = prfHomeGN.Corner_Team_3_5_Over,
                                  Team_FT_Corner_45_Over = prfHomeGN.Corner_Team_4_5_Over,
                                  FT_Corner_75_Over = prfHomeGN.Corner_7_5_Over,
                                  FT_Corner_85_Over = prfHomeGN.Corner_8_5_Over,
                                  FT_Corner_95_Over = prfHomeGN.Corner_9_5_Over,
                                  Is_FT_CornerWinTeam = prfHomeGN.Is_Corner_FT_Win,
                                  Is_FT_CornerX = prfHomeGN.Is_Corner_FT_X,
                                  Team_Possesion = prfHomeGN.Team_Possesion
                              },
                              AwayTeamPerformanceGeneral = new PerformanceModelDto
                              {
                                  Average_FT_Goals_Team = prfAwayGN.Average_FT_Goals_Team,
                                  Average_HT_Goals_Team = prfAwayGN.Average_HT_Goals_Team,
                                  Average_SH_Goals_Team = prfAwayGN.Average_SH_Goals_Team,
                                  Average_FT_Conceded_Goals_Team = prfAwayGN.Average_FT_Conceded_Goals_Team,
                                  Average_HT_Conceded_Goals_Team = prfAwayGN.Average_HT_Conceded_Goals_Team,
                                  Average_SH_Conceded_Goals_Team = prfAwayGN.Average_SH_Conceded_Goals_Team,
                                  FT_15_Over = prfAwayGN.FT_15_Over,
                                  FT_25_Over = prfAwayGN.FT_25_Over,
                                  FT_35_Over = prfAwayGN.FT_35_Over,
                                  HT_05_Over = prfAwayGN.HT_05_Over,
                                  HT_15_Over = prfAwayGN.HT_15_Over,
                                  SH_05_Over = prfAwayGN.SH_05_Over,
                                  SH_15_Over = prfAwayGN.SH_15_Over,
                                  Team_FT_05_Over = prfAwayGN.Team_FT_05_Over,
                                  Team_FT_15_Over = prfAwayGN.Team_FT_15_Over,
                                  Team_HT_05_Over = prfAwayGN.Team_HT_05_Over,
                                  Team_HT_15_Over = prfAwayGN.Team_HT_15_Over,
                                  Team_SH_05_Over = prfAwayGN.Team_SH_05_Over,
                                  Team_SH_15_Over = prfAwayGN.Team_SH_15_Over,
                                  Team_Win_Any_Half = prfAwayGN.Team_Win_Any_Half,
                                  FT_GG = prfAwayGN.FT_GG,
                                  HT_GG = prfAwayGN.HT_GG,
                                  SH_GG = prfAwayGN.SH_GG,
                                  Is_FT_Win = prfAwayGN.Is_FT_Win,
                                  Is_FT_X = prfAwayGN.Is_FT_X,
                                  Is_HT_Win = prfAwayGN.Is_HT_Win,
                                  Is_HT_X = prfAwayGN.Is_HT_X,
                                  Is_SH_Win = prfAwayGN.Is_SH_Win,
                                  Is_SH_X = prfAwayGN.Is_SH_X,

                                  Average_FT_Corners_Team = prfAwayGN.Average_FT_Corners_Team,
                                  Average_FT_GK_Saves_Team = prfAwayGN.Average_FT_GK_Saves_Team,
                                  Average_FT_ShutOnTarget_Team = prfAwayGN.Average_FT_ShutOnTarget_Team,
                                  Average_FT_Shut_Team = prfAwayGN.Average_FT_Shut_Team,
                                  Team_FT_Corner_35_Over = prfAwayGN.Corner_Team_3_5_Over,
                                  Team_FT_Corner_45_Over = prfAwayGN.Corner_Team_4_5_Over,
                                  FT_Corner_75_Over = prfAwayGN.Corner_7_5_Over,
                                  FT_Corner_85_Over = prfAwayGN.Corner_8_5_Over,
                                  FT_Corner_95_Over = prfAwayGN.Corner_9_5_Over,
                                  Is_FT_CornerWinTeam = prfAwayGN.Is_Corner_FT_Win,
                                  Is_FT_CornerX = prfAwayGN.Is_Corner_FT_X,
                                  Team_Possesion = prfAwayGN.Team_Possesion
                              },
                              LeagueStat = new LeagueModelDto
                              {
                                  FT_GoalsAverage = lg.FT_GoalsAverage,
                                  HT_GoalsAverage = lg.HT_GoalsAverage,
                                  SH_GoalsAverage = lg.SH_GoalsAverage,
                                  FT_Over15_Percentage = lg.FT_Over15_Percentage,
                                  FT_Over25_Percentage = lg.FT_Over25_Percentage,
                                  FT_Over35_Percentage = lg.FT_Over35_Percentage,
                                  GG_Percentage = lg.GG_Percentage,
                                  HT_Over05_Percentage = lg.HT_Over05_Percentage,
                                  HT_Over15_Percentage = lg.HT_Over15_Percentage,
                                  SH_Over05_Percentage = lg.SH_Over05_Percentage,
                                  SH_Over15_Percentage = lg.SH_Over15_Percentage
                              }
                          }).FirstOrDefault();
            return result;

        }

        public ComparisonResponseModel GetComparisonStatistics(int serial, int bySide)
        {
            var result = (from mid in Context.MatchIdentifiers
                          join cmp in Context.ComparisonStatisticsHolders on mid.Id equals cmp.MatchIdentifierId
                          where cmp.BySideType == bySide
                          where mid.Serial == serial
                          select new ComparisonResponseModel
                          {
                              HomeTeam = mid.HomeTeam,
                              AwayTeam = mid.AwayTeam,
                              Average_FT_Goals_HomeTeam = cmp.Average_FT_Goals_HomeTeam,
                              Average_FT_Goals_AwayTeam = cmp.Average_FT_Goals_AwayTeam,
                              Average_HT_Goals_HomeTeam = cmp.Average_HT_Goals_HomeTeam,
                              Average_HT_Goals_AwayTeam = cmp.Average_HT_Goals_AwayTeam,
                              Average_SH_Goals_HomeTeam = cmp.Average_SH_Goals_HomeTeam,
                              Average_SH_Goals_AwayTeam = cmp.Average_SH_Goals_AwayTeam,
                              Average_FT_Conceded_Goals_HomeTeam = cmp.Average_FT_Conceded_Goals_HomeTeam,
                              Average_FT_Conceded_Goals_AwayTeam = cmp.Average_FT_Conceded_Goals_AwayTeam,
                              Average_HT_Conceded_Goals_HomeTeam = cmp.Average_HT_Conceded_Goals_HomeTeam,
                              Average_HT_Conceded_Goals_AwayTeam = cmp.Average_HT_Conceded_Goals_AwayTeam,
                              Average_SH_Conceded_Goals_HomeTeam = cmp.Average_SH_Conceded_Goals_HomeTeam,
                              Average_SH_Conceded_Goals_AwayTeam = cmp.Average_SH_Conceded_Goals_AwayTeam,
                              Away_FT_05_Over = cmp.Away_FT_05_Over,
                              Away_FT_15_Over = cmp.Away_FT_15_Over,
                              Away_HT_05_Over = cmp.Away_HT_05_Over,
                              Away_HT_15_Over = cmp.Away_HT_15_Over,
                              Away_SH_05_Over = cmp.Away_SH_05_Over,
                              Away_SH_15_Over = cmp.Away_SH_15_Over,
                              Home_FT_05_Over = cmp.Home_FT_05_Over,
                              Home_FT_15_Over = cmp.Home_FT_15_Over,
                              Home_HT_05_Over = cmp.Home_HT_05_Over,
                              Home_HT_15_Over = cmp.Home_HT_15_Over,
                              Home_SH_05_Over = cmp.Home_SH_05_Over,
                              Home_SH_15_Over = cmp.Home_SH_15_Over,
                              Away_Win_Any_Half = cmp.Away_Win_Any_Half,
                              Home_Win_Any_Half = cmp.Home_Win_Any_Half,
                              FT_15_Over = cmp.FT_15_Over,
                              FT_25_Over = cmp.FT_25_Over,
                              FT_35_Over = cmp.FT_35_Over,
                              HT_05_Over = cmp.HT_05_Over,
                              HT_15_Over = cmp.HT_15_Over,
                              SH_05_Over = cmp.SH_05_Over,
                              SH_15_Over = cmp.SH_15_Over,
                              FT_GG = cmp.FT_GG,
                              HT_GG = cmp.HT_GG,
                              SH_GG = cmp.SH_GG,
                              Is_FT_Win1 = cmp.Is_FT_Win1,
                              Is_FT_X = cmp.Is_FT_X,
                              Is_FT_Win2 = cmp.Is_FT_Win2,
                              Is_HT_Win1 = cmp.Is_HT_Win1,
                              Is_HT_X = cmp.Is_HT_X,
                              Is_HT_Win2 = cmp.Is_HT_Win2,
                              Is_SH_Win1 = cmp.Is_SH_Win1,
                              Is_SH_X = cmp.Is_SH_X,
                              Is_SH_Win2 = cmp.Is_SH_Win2
                          }).FirstOrDefault();
            return result;
        }

        public PerformanceResponseModel GetPerformanceStatistics(int serial, int bySide, int homeOrAway)
        {
            var result = (from mid in Context.MatchIdentifiers
                          join prfTeam in Context.TeamPerformanceStatisticsHolders on mid.Id equals prfTeam.MatchIdentifierId
                          where prfTeam.BySideType == bySide && prfTeam.HomeOrAway == homeOrAway
                          where mid.Serial == serial
                          select new PerformanceResponseModel
                          {
                              TeamName = homeOrAway == 1 ? mid.HomeTeam : mid.AwayTeam,
                              Average_FT_Goals_Team = prfTeam.Average_FT_Goals_Team,
                              Average_HT_Goals_Team = prfTeam.Average_HT_Goals_Team,
                              Average_SH_Goals_Team = prfTeam.Average_SH_Goals_Team,
                              Average_FT_Conceded_Goals_Team = prfTeam.Average_FT_Conceded_Goals_Team,
                              Average_HT_Conceded_Goals_Team = prfTeam.Average_HT_Conceded_Goals_Team,
                              Average_SH_Conceded_Goals_Team = prfTeam.Average_SH_Conceded_Goals_Team,
                              Average_FT_GK_Saves_Team = prfTeam.Average_FT_GK_Saves_Team,
                              FT_15_Over = prfTeam.FT_15_Over,
                              FT_25_Over = prfTeam.FT_25_Over,
                              FT_35_Over = prfTeam.FT_35_Over,
                              HT_05_Over = prfTeam.HT_05_Over,
                              HT_15_Over = prfTeam.HT_15_Over,
                              SH_05_Over = prfTeam.SH_05_Over,
                              SH_15_Over = prfTeam.SH_15_Over,
                              Team_FT_05_Over = prfTeam.Team_FT_05_Over,
                              Team_FT_15_Over = prfTeam.Team_FT_15_Over,
                              Team_HT_05_Over = prfTeam.Team_HT_05_Over,
                              Team_HT_15_Over = prfTeam.Team_HT_15_Over,
                              Team_SH_05_Over = prfTeam.Team_SH_05_Over,
                              Team_SH_15_Over = prfTeam.Team_SH_15_Over,
                              Team_Win_Any_Half = prfTeam.Team_Win_Any_Half,
                              FT_GG = prfTeam.FT_GG,
                              HT_GG = prfTeam.HT_GG,
                              SH_GG = prfTeam.SH_GG,
                              Is_FT_Win = prfTeam.Is_FT_Win,
                              Is_FT_X = prfTeam.Is_FT_X,
                              Is_HT_Win = prfTeam.Is_HT_Win,
                              Is_HT_X = prfTeam.Is_HT_X,
                              Is_SH_Win = prfTeam.Is_SH_Win,
                              Is_SH_X = prfTeam.Is_SH_X,
                              Average_FT_Corners_Team = prfTeam.Average_FT_Corners_Team,
                              Average_FT_ShutOnTarget_Team = prfTeam.Average_FT_ShutOnTarget_Team,
                              Average_FT_Shut_Team = prfTeam.Average_FT_Shut_Team,
                              Team_FT_Corner_35_Over = prfTeam.Corner_Team_3_5_Over,
                              Team_FT_Corner_45_Over = prfTeam.Corner_Team_4_5_Over,
                              FT_Corner_75_Over = prfTeam.Corner_7_5_Over,
                              FT_Corner_85_Over = prfTeam.Corner_8_5_Over,
                              FT_Corner_95_Over = prfTeam.Corner_9_5_Over,
                              Is_FT_CornerWinTeam = prfTeam.Is_Corner_FT_Win,
                              Is_FT_CornerX = prfTeam.Is_Corner_FT_X,
                              Team_Possesion = prfTeam.Team_Possesion
                          }).FirstOrDefault();
            return result;
        }

        public LeagueStatisticsResponseModel GetLeagueStatistics(int serial)
        {
            var result = (from mid in Context.MatchIdentifiers
                          join cmp in Context.ComparisonStatisticsHolders on mid.Id equals cmp.MatchIdentifierId
                          where cmp.BySideType == 1
                          join lg in Context.LeagueStatisticsHolders on cmp.LeagueStaisticsHolderId equals lg.Id
                          where mid.Serial == serial
                          select new LeagueStatisticsResponseModel
                          {
                              FT_GoalsAverage = lg.FT_GoalsAverage,
                              HT_GoalsAverage = lg.HT_GoalsAverage,
                              SH_GoalsAverage = lg.SH_GoalsAverage,
                              FT_Over15_Percentage = lg.FT_Over15_Percentage,
                              FT_Over25_Percentage = lg.FT_Over25_Percentage,
                              FT_Over35_Percentage = lg.FT_Over35_Percentage,
                              GG_Percentage = lg.GG_Percentage,
                              HT_Over05_Percentage = lg.HT_Over05_Percentage,
                              HT_Over15_Percentage = lg.HT_Over15_Percentage,
                              SH_Over05_Percentage= lg.SH_Over05_Percentage,
                              SH_Over15_Percentage = lg.SH_Over15_Percentage
                          }).FirstOrDefault();
            return result;
        }
    }
}

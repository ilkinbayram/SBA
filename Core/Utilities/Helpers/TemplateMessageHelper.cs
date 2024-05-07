using Core.Entities.Concrete.ComplexModels.Sql;
using Core.Entities.Concrete.ExternalDbEntities;

namespace Core.Utilities.Helpers
{
    public static class TemplateMessageHelper
    {
        public static string PrepareStatistics(List<StatisticInfoHolder> statisticInfoHolders)
        {
            string result = "\r\n-----------------------------------------------------------\r\n⚠️ Diqqət: Bu Maç üçün statistika tapılmadı.";

            if (statisticInfoHolders == null || statisticInfoHolders.Count == 0) return result;

            int foundCount = Convert.ToInt32(statisticInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found").HomePercent);

            if (foundCount <= 10) return result;

            var extraDetails = new TeamLeagueMixedStat();

            try
            {
                extraDetails = new TeamLeagueMixedStat
                {
                    Average_FT_Goals_HomeTeam = statisticInfoHolders.FirstOrDefault(x => x.Title == "Ind_Avg_Goal_FT").HomePercent,
                    Average_FT_Goals_AwayTeam = statisticInfoHolders.FirstOrDefault(x => x.Title == "Ind_Avg_Goal_FT").AwayPercent,
                    Average_HT_Goals_HomeTeam = statisticInfoHolders.FirstOrDefault(x => x.Title == "Ind_Avg_Goal_HT").HomePercent,
                    Average_HT_Goals_AwayTeam = statisticInfoHolders.FirstOrDefault(x => x.Title == "Ind_Avg_Goal_HT").AwayPercent,
                    Average_SH_Goals_HomeTeam = statisticInfoHolders.FirstOrDefault(x => x.Title == "Ind_Avg_Goal_SH").HomePercent,
                    Average_SH_Goals_AwayTeam = statisticInfoHolders.FirstOrDefault(x => x.Title == "Ind_Avg_Goal_SH").AwayPercent,
                    Average_FT_Conceeded_Goals_HomeTeam = statisticInfoHolders.FirstOrDefault(x => x.Title == "Ind_Avg_Conc_Goal_FT").HomePercent,
                    Average_FT_Conceeded_Goals_AwayTeam = statisticInfoHolders.FirstOrDefault(x => x.Title == "Ind_Avg_Conc_Goal_FT").AwayPercent,
                    Average_HT_Conceeded_Goals_HomeTeam = statisticInfoHolders.FirstOrDefault(x => x.Title == "Ind_Avg_Conc_Goal_HT").HomePercent,
                    Average_HT_Conceeded_Goals_AwayTeam = statisticInfoHolders.FirstOrDefault(x => x.Title == "Ind_Avg_Conc_Goal_HT").AwayPercent,
                    Average_SH_Conceeded_Goals_HomeTeam = statisticInfoHolders.FirstOrDefault(x => x.Title == "Ind_Avg_Conc_Goal_SH").HomePercent,
                    Average_SH_Conceeded_Goals_AwayTeam = statisticInfoHolders.FirstOrDefault(x => x.Title == "Ind_Avg_Conc_Goal_SH").AwayPercent,
                    HomeInd_FT_05_Over = Convert.ToInt32(statisticInfoHolders.FirstOrDefault(x => x.Title == "Ind_FT_05").HomePercent),
                    AwayInd_FT_05_Over = Convert.ToInt32(statisticInfoHolders.FirstOrDefault(x => x.Title == "Ind_FT_05").AwayPercent),
                    HomeInd_FT_15_Over = Convert.ToInt32(statisticInfoHolders.FirstOrDefault(x => x.Title == "Ind_FT_15").HomePercent),
                    AwayInd_FT_15_Over = Convert.ToInt32(statisticInfoHolders.FirstOrDefault(x => x.Title == "Ind_FT_15").AwayPercent),
                    HomeInd_HT_05_Over = Convert.ToInt32(statisticInfoHolders.FirstOrDefault(x => x.Title == "Ind_HT_05").HomePercent),
                    AwayInd_HT_05_Over = Convert.ToInt32(statisticInfoHolders.FirstOrDefault(x => x.Title == "Ind_HT_05").AwayPercent),
                    HomeInd_SH_05_Over = Convert.ToInt32(statisticInfoHolders.FirstOrDefault(x => x.Title == "Ind_SH_05").HomePercent),
                    AwayInd_SH_05_Over = Convert.ToInt32(statisticInfoHolders.FirstOrDefault(x => x.Title == "Ind_SH_05").AwayPercent),
                    FT_15_Over_Home = Convert.ToInt32(statisticInfoHolders.FirstOrDefault(x => x.Title == "FT_15").HomePercent),
                    FT_15_Over_Away = Convert.ToInt32(statisticInfoHolders.FirstOrDefault(x => x.Title == "FT_15").AwayPercent),
                    FT_25_Over_Home = Convert.ToInt32(statisticInfoHolders.FirstOrDefault(x => x.Title == "FT_25").HomePercent),
                    FT_25_Over_Away = Convert.ToInt32(statisticInfoHolders.FirstOrDefault(x => x.Title == "FT_25").AwayPercent),
                    FT_35_Over_Home = Convert.ToInt32(statisticInfoHolders.FirstOrDefault(x => x.Title == "FT_35").HomePercent),
                    FT_35_Over_Away = Convert.ToInt32(statisticInfoHolders.FirstOrDefault(x => x.Title == "FT_35").AwayPercent),
                    HT_05_Over_Home = Convert.ToInt32(statisticInfoHolders.FirstOrDefault(x => x.Title == "HT_05").HomePercent),
                    HT_05_Over_Away = Convert.ToInt32(statisticInfoHolders.FirstOrDefault(x => x.Title == "HT_05").AwayPercent),
                    SH_05_Over_Home = Convert.ToInt32(statisticInfoHolders.FirstOrDefault(x => x.Title == "SH_05").HomePercent),
                    SH_05_Over_Away = Convert.ToInt32(statisticInfoHolders.FirstOrDefault(x => x.Title == "SH_05").AwayPercent),
                    FT_GG_Home = Convert.ToInt32(statisticInfoHolders.FirstOrDefault(x => x.Title == "FT_GG").HomePercent),
                    FT_GG_Away = Convert.ToInt32(statisticInfoHolders.FirstOrDefault(x => x.Title == "FT_GG").AwayPercent)
                };
            }
            catch (Exception)
            {
                extraDetails = null;
            }

            bool notAvailable = extraDetails == null ||
                                (extraDetails.Average_FT_Goals_HomeTeam <= 0 && 
                                extraDetails.Average_FT_Goals_AwayTeam <= 0 && 
                                extraDetails.Average_FT_Conceeded_Goals_HomeTeam <= 0 && 
                                extraDetails.Average_FT_Conceeded_Goals_AwayTeam <= 0 &&
                                extraDetails.FT_15_Over_Home <= 0 &&
                                extraDetails.FT_15_Over_Away <= 0);

            if (notAvailable) return result;

            result = $"\r\n\r\n-----------------------------------------------------------\r\n                  📊  Statistika  📊\r\n-----------------------------------------------------------\r\n{extraDetails.Average_FT_Goals_HomeTeam}         -MS.At.Gol.Or-         {extraDetails.Average_FT_Goals_AwayTeam}\r\n{extraDetails.Average_FT_Conceeded_Goals_HomeTeam}         -MS.Ye.Gol.Or-         {extraDetails.Average_FT_Conceeded_Goals_AwayTeam}\r\n{extraDetails.Average_HT_Goals_HomeTeam}         -BH.At.Gol.Or-         {extraDetails.Average_HT_Goals_AwayTeam}\r\n{extraDetails.Average_HT_Conceeded_Goals_HomeTeam}         -BH.Ye.Gol.Or-         {extraDetails.Average_HT_Conceeded_Goals_AwayTeam}\r\n{extraDetails.Average_SH_Goals_HomeTeam}          -İH.At.Gol.Or-          {extraDetails.Average_SH_Goals_AwayTeam}\r\n{extraDetails.Average_SH_Conceeded_Goals_HomeTeam}          -İH.Ye.Gol.Or-         {extraDetails.Average_SH_Conceeded_Goals_AwayTeam}\r\n{extraDetails.HomeInd_FT_05_Over}%         -Tkm.MS.0,5Ü-         {extraDetails.AwayInd_FT_05_Over}%\r\n{extraDetails.HomeInd_FT_15_Over}%         -Tkm.MS.1,5Ü-         {extraDetails.AwayInd_FT_15_Over}%\r\n{extraDetails.HomeInd_HT_05_Over}%         -Tkm.BH.0,5Ü-         {extraDetails.AwayInd_HT_05_Over}%\r\n{extraDetails.HomeInd_SH_05_Over}%          -Tkm.İH.0,5Ü-          {extraDetails.AwayInd_SH_05_Over}%\r\n{extraDetails.FT_GG_Home}%               -Gol/Gol-              {extraDetails.FT_GG_Away}%\r\n{extraDetails.FT_15_Over_Home}%              -MS.1,5Ü-              {extraDetails.FT_15_Over_Away}%\r\n{extraDetails.FT_25_Over_Home}%              -MS.2,5Ü-              {extraDetails.FT_25_Over_Away}%\r\n{extraDetails.FT_35_Over_Home}%              -MS.3,5Ü-              {extraDetails.FT_35_Over_Away}%\r\n{extraDetails.HT_05_Over_Home}%              -BH.0,5Ü-              {extraDetails.HT_05_Over_Away}%\r\n{extraDetails.SH_05_Over_Home}%               -İH.0,5Ü-               {extraDetails.SH_05_Over_Away}%\r\n";

            return result;
        }
    }
}

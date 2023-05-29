using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete.ExternalDbEntities;
using Core.Entities.Concrete.SqlEntities.FunctionViewProcModels;
using Core.Entities.Dtos.ComplexDataes.UIData;
using Core.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SBA.ExternalDataAccess.Abstract;
using SBA.ExternalDataAccess.Concrete.EntityFramework.Contexts;

namespace SBA.ExternalDataAccess.Concrete
{
    public class EfForecastDal : EfEntityRepositoryBase<Forecast, ExternalAppDbContext>, IForecastDal
    {
        public EfForecastDal(ExternalAppDbContext applicationContext) : base(applicationContext)
        {
        }

        public async Task<int> AddPossibleForecastsAsync(List<PossibleForecast> possibleForecasts)
        {
            await Context.PossibleForecasts.AddRangeAsync(possibleForecasts);
            return await Context.SaveChangesAsync();
        }

        public async Task<ForecastDataContainer> SelectForecastContainerInfoAsync(bool isCheckedItems)
        {
            try
            {
                var result = new ForecastDataContainer();

                var paramIsChecked = new SqlParameter("@paramIsChecked", isCheckedItems);
                var functionResult = Context.Set<MatchForecastFM>()
                                    .FromSqlRaw("SELECT * FROM fn_MatchForecast(@paramIsChecked)", paramIsChecked)
                                    .ToList();

                var groupedMatchForecast = functionResult.GroupBy(mf => mf.Serial)
                    .Select(g => new MatchForecast
                    {
                        Serial = g.Key,
                        MatchIdentityId = g.First().MatchIdentityId,
                        HomeTeam = g.First().HomeTeam,
                        AwayTeam = g.First().AwayTeam,
                        CountryLeague = g.First().CountryLeague,
                        HT_Result = g.First().HT_Result,
                        FT_Result = g.First().FT_Result,
                        Forecasts = g.Select(mf => new ForecastDTO
                        {
                            IsSuccess = mf.IsSuccess,
                            IsChecked = mf.IsChecked,
                            Description = mf.Description.TranslateResource(2)
                        }).ToList()
                    }).ToList();

                for (int i = 0; i < groupedMatchForecast.Count; i++)
                {
                    if (groupedMatchForecast[i].Forecasts.Count > 0)
                        result.MatchForecasts.Add(groupedMatchForecast[i]);
                }

                return result;
            }
            catch (Exception ex)
            {
                await Context.Logs.AddAsync(new Core.Entities.Concrete.Log
                {
                    Path = "SBA.ExternalDataAccess.Concrete -> SelectForecastContainerInfoAsync(bool isCheckedItems)",
                    Description = ex.Message,
                    Importance = Core.Resources.Enums.LogImportance.Critical
                });

                await Context.SaveChangesAsync();

                return null;
            }
        }

        public async Task<List<string>> SelectForecastsBySerialAsync(int serial)
        {
            var matchIdentities = await (from mid in Context.MatchIdentifiers
                                         join fc in Context.Forecasts
                                         on mid.Id equals fc.MatchIdentifierId
                                         where mid.Serial == serial
                                         select fc).ToListAsync();

            var result = matchIdentities.Select(x => x.Key.TranslateResource(2)).ToList();

            return result;
        }
    }
}

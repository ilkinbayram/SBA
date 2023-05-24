using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete.ExternalDbEntities;
using Core.Entities.Dtos.ComplexDataes.UIData;
using Core.Extensions;
using Microsoft.EntityFrameworkCore;
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
            var result = new ForecastDataContainer();

            var matchIdentities = await (from mid in Context.MatchIdentifiers
                                         join pfc in Context.PossibleForecasts
                                         on mid.Serial equals pfc.Serial
                                         select mid).Include(x => x.Forecasts).ToListAsync();

            for (int i = 0; i < matchIdentities.Count; i++)
            {
                var matchIdentity = matchIdentities[i];

                var forecasts = matchIdentity.Forecasts;

                var matchForecast = new MatchForecast
                {
                    HomeTeam = matchIdentity.HomeTeam,
                    AwayTeam = matchIdentity.AwayTeam,
                    Forecasts = forecasts.Select(x => new ForecastDTO
                    {
                        IsChecked = x.IsChecked,
                        IsSuccess = x.IsSuccess,
                        Description = x.Key
                    }).Where(x => x.IsChecked == isCheckedItems).ToList(),
                    Serial = matchIdentity.Serial,
                    MatchIdentityId = matchIdentity.Id
                };

                if (matchForecast.Forecasts.Count > 0)
                    result.MatchForecasts.Add(matchForecast);
            }

            return result;
        }

        public async Task<List<string>> SelectForecastsBySerialAsync(int serial)
        {
            var matchIdentities = await (from mid in Context.MatchIdentifiers
                                         join fc in Context.Forecasts
                                         on mid.Id equals fc.MatchIdentifierId
                                         where mid.Serial == serial
                                         select fc).ToListAsync();

            var result = matchIdentities.Select(x=>x.Key.TranslateResource(2)).ToList();

            return result;
        }
    }
}

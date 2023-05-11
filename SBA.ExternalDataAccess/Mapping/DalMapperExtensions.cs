using Core.Entities.Concrete.ExternalDbEntities;
using Core.Entities.Dtos.ComplexDataes.MobileUI;
using Core.Extensions;

namespace SBA.ExternalDataAccess.Mapping
{
    public static class DalMapperExtensions
    {
        public static List<StatisticInfo> MapList(this List<StatisticInfoHolder> entities, int lang)
        {
            var result = new List<StatisticInfo>();
            for (int i = 0; i < entities.Count; i++)
            {
                result.Add(entities[i].Map(lang));
            }
            return result.OrderBy(x=>x.Order).ToList();
        }

        public static StatisticInfo Map(this StatisticInfoHolder entity, int lang)
        {
            return new StatisticInfo
            {
                Title = entity.Title.TranslateResource(lang),
                Order = entity.Order,
                AwayPercent = entity.AwayPercent,
                AwayValue = entity.AwayValue,
                HomePercent = entity.HomePercent,
                HomeValue = entity.HomeValue
            };
        }
    }
}

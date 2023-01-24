using Core.Resources.Enums;
using Core.Utilities.UsableModel.BaseModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Core.Utilities.Helpers
{
    public class QuickConvert
    {
        public List<T> MixRange<T>(List<T> src, TeamSide teamSide)
            where T : BaseComparerContainerModel, new()
        {
            if (src == null || src.Count == 0) return null;

            var validSide = teamSide == TeamSide.Home
                ? src.Where(x => x.HomeTeam == x.UnchangableHomeTeam).ToList()
                : src.Where(x => x.AwayTeam == x.UnchangableAwayTeam).ToList();

            var reversableSide = teamSide == TeamSide.Home
                ? src.Where(x => x.AwayTeam == x.UnchangableHomeTeam).ToList().ReverseContent()
                : src.Where(x => x.HomeTeam == x.UnchangableAwayTeam).ToList().ReverseContent();

            validSide.AddRange(reversableSide);

            return validSide;
        }

        public decimal ConvertToDecimal(string value)
        {
            if (Decimal.TryParse(value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal _res))
            {
                return Decimal.Parse(value,
   NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
            }

            if (value.Contains(','))
            {
                value = value.Replace(',', '.');
            }
            else
            {
                value = value.Replace('.', ',');
            }

            if (Decimal.TryParse(value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal _res2nd))
            {
                return Decimal.Parse(value,
   NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
            }
            else
            {
                return (decimal)-1.00;
            }
        }
    }

    public static class ConversionExtension
    {
        public static List<T> ReverseContent<T>(this List<T> src)
            where T : BaseComparerContainerModel, new()
        {
            var result = new List<T>();

            src.ForEach(x =>
            {
                result.Add(new T
                {
                    AwayTeam = x.HomeTeam,
                    HomeTeam = x.AwayTeam,
                    FT_Goals_AwayTeam = x.FT_Goals_HomeTeam,
                    FT_Goals_HomeTeam = x.FT_Goals_AwayTeam,
                    HT_Goals_AwayTeam = x.HT_Goals_HomeTeam,
                    HT_Goals_HomeTeam = x.HT_Goals_AwayTeam,
                    Serial = x.Serial,
                    UnchangableAwayTeam = x.UnchangableAwayTeam,
                    UnchangableHomeTeam = x.UnchangableHomeTeam,
                });
            });

            return result;
        }
    }
}

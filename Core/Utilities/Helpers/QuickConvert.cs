using Core.Resources.Enums;
using Core.Utilities.UsableModel.BaseModels;
using System.Globalization;

namespace Core.Utilities.Helpers
{
    public class QuickConvert
    {
        public List<T> MixRange<T>(List<T> src, TeamSide teamSide)
    where T : BaseComparerContainerModel, new()
        {
            if (src is null || src.Count == 0)
            {
                return null;
            }

            var validSide = (teamSide == TeamSide.Home)
                ? src.Where(c => c.HomeTeam == c.UnchangableHomeTeam).ToList()
                : src.Where(c => c.AwayTeam == c.UnchangableAwayTeam).ToList();

            var reversableSide = (teamSide == TeamSide.Home)
                ? src.Where(c => c.AwayTeam == c.UnchangableHomeTeam).ToList().ReverseContent()
                : src.Where(c => c.HomeTeam == c.UnchangableAwayTeam).ToList().ReverseContent();

            return validSide.Concat(reversableSide).ToList();
        }

        public decimal ConvertToDecimal(string value)
        {
            if (decimal.TryParse(value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal result))
            {
                return result;
            }

            value = value.Contains('.') ? value.Replace(',', '.') : value.Replace('.', ',');

            if (decimal.TryParse(value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal result2))
            {
                return result2;
            }

            return -1.00M;
        }
    }

    public static class ConversionExtension
    {
        public static List<T> ReverseContent<T>(this List<T> src)
    where T : BaseComparerContainerModel, new()
        {
            if (src is null)
            {
                throw new ArgumentNullException(nameof(src));
            }

            var result = new List<T>(src.Count);

            foreach (var item in src)
            {
                var reversed = new T
                {
                    AwayTeam = item.HomeTeam,
                    HomeTeam = item.AwayTeam,
                    FT_Goals_AwayTeam = item.FT_Goals_HomeTeam,
                    FT_Goals_HomeTeam = item.FT_Goals_AwayTeam,
                    HT_Goals_AwayTeam = item.HT_Goals_HomeTeam,
                    HT_Goals_HomeTeam = item.HT_Goals_AwayTeam,
                    Serial = item.Serial,
                    UnchangableAwayTeam = item.UnchangableAwayTeam,
                    UnchangableHomeTeam = item.UnchangableHomeTeam
                };

                result.Add(reversed);
            }

            return result;
        }
    }
}

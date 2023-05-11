using Core.Resources.Enums;
using Core.Utilities.Helpers;
using Core.Utilities.UsableModel.TempTableModels.Country;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.Extensions
{
    public static class RegexExtensions
    {
        private static QuickConvert _converter = new QuickConvert();

        public static decimal ResolveOddByRegex(this string src, params Regex[] regexes)
        {
            foreach (var regex in regexes)
            {
                if (regex.IsMatch(src))
                    return _converter.ConvertToDecimal(regex.Matches(src)[0].Groups[1].Value);
                else
                    return (decimal)-1.00;
            }

            return (decimal)-1.00;
        }

        public static string ResolveDateByRegex(this string src, params Regex[] regexes)
        {
            foreach (var regex in regexes)
            {
                if (regex.IsMatch(src))
                {
                    var dateMatch = regex.Matches(src)[0].Groups[1].Value.Trim();
                    if (!dateMatch.Contains(".") || dateMatch.Split(".").Length != 3) continue;
                    var builder = new StringBuilder();

                    if(dateMatch.Split(".")[0].Length == 1) builder.Append($"0");
                    builder.Append($"{dateMatch.Split(".")[0]}.");

                    if (dateMatch.Split(".")[1].Length == 1) builder.Append($"0");
                    builder.Append($"{dateMatch.Split(".")[1]}.");

                    builder.Append(dateMatch.Split(".")[2]);

                    dateMatch = builder.ToString();

                    bool isValidDate = DateTime.TryParseExact(dateMatch, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime _date);

                    if (!isValidDate) continue;

                    return dateMatch;
                }
                else
                    return string.Empty;
            }

            return string.Empty;
        }

        public static DateTime? ResolveDateFormatByRegex(this string src, params Regex[] regexes)
        {
            foreach (var regex in regexes)
            {
                if (regex.IsMatch(src))
                {
                    var dateMatch = regex.Matches(src)[0].Groups[1].Value.Trim();
                    if (!dateMatch.Contains(".") || dateMatch.Split(".").Length != 3) continue;
                    var builder = new StringBuilder();

                    if (dateMatch.Split(".")[0].Length == 1) builder.Append($"0");
                    builder.Append($"{dateMatch.Split(".")[0]}.");

                    if (dateMatch.Split(".")[1].Length == 1) builder.Append($"0");
                    builder.Append($"{dateMatch.Split(".")[1]}.");

                    builder.Append(dateMatch.Split(".")[2]);

                    dateMatch = builder.ToString();

                    bool isValidDate = DateTime.TryParseExact(dateMatch, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime _date);

                    if (!isValidDate) continue;

                    return DateTime.Parse(dateMatch);
                }
                else
                    continue;
            }

            return null;
        }

        public static string ResolveTextByRegex(this string src, params Regex[] regexes)
        {
            foreach (var regex in regexes)
            {
                if (regex.IsMatch(src))
                    return regex.Matches(src)[0].Groups[1].Value.Trim();
                else
                    return string.Empty;
            }

            return string.Empty;
        }

        public static string ResolveCountryByRegex(this string src, CountryContainerTemp containerTemp, params Regex[] regexes)
        {
            foreach (var regex in regexes)
            {
                if (regex.IsMatch(src))
                {
                    string matchedResult = regex.Matches(src)[0].Groups[1].Value.Trim();

                    if (containerTemp.Countries.Any(name => matchedResult.Contains(name.Name)))
                    {
                        return containerTemp.Countries
                                .FirstOrDefault(x =>
                                    matchedResult.Contains(x.Name)).Name.Trim();
                    }
                    else
                    {
                        return matchedResult.Split("-")[2].Trim().Split(" ")[0];
                    }
                }
            }

            return "NONE";
        }

        public static string ResolveComparisonCountryByRegex(this string src, params Regex[] regexes)
        {
            foreach (var regex in regexes)
            {
                if (regex.IsMatch(src))
                {
                    return regex.Matches(src)[0].Groups[1].Value.Split('-')[0].Trim();
                }
            }

            return "NONE";
        }

        public static string ResolveLeagueByRegex(this string src, CountryContainerTemp containerTemp, params Regex[] regexes)
        {
            foreach (var regex in regexes)
            {
                if (regex.IsMatch(src))
                {
                    string matchedResult = regex.Matches(src)[0].Groups[1].Value.Trim();

                    if (containerTemp.Countries.Any(name => matchedResult.Contains(name.Name)))
                    {
                        var countryModel = containerTemp.Countries
                                .FirstOrDefault(x =>
                                    matchedResult.Contains(x.Name));
                        matchedResult = matchedResult
                            .Split(countryModel.Name)[1].Trim()
                            .Split($"@")[0];
                    }
                    else if(matchedResult.Split("-").Length > 2)
                    {
                        matchedResult = matchedResult
                            .Split(matchedResult.Split("-")[2].Trim()
                            .Split(" ")[0])[1]
                            .Split($"@")[0];
                    }

                    matchedResult = matchedResult.Trim()
                                  .Replace("2018/2019", "").Trim()
                                  .Replace("2019/2020", "").Trim()
                                  .Replace("2020/2021", "").Trim()
                                  .Replace("2021/2022", "").Trim()
                                  .Replace("2022/2023", "").Trim()
                                  .Replace("2023/2024", "").Trim()
                                  .Replace("Grup A", "").Trim()
                                  .Replace("Grup B", "").Trim()
                                  .Replace("Grup C", "").Trim()
                                  .Replace("Grup D", "").Trim()
                                  .Replace("Grup E", "").Trim()
                                  .Replace("Grup F", "").Trim()
                                  .Replace("Grup G", "").Trim()
                                  .Replace("Grup H", "").Trim()
                                  .Replace("Grup I", "").Trim()
                                  .Replace("Grup J", "").Trim()
                                  .Replace("Grup K", "").Trim()
                                  .Replace("Grup L", "").Trim()
                                  .Replace("Grup M", "").Trim()
                                  .Replace("Grup N", "").Trim()
                                  .Replace("Group A", "").Trim()
                                  .Replace("Group B", "").Trim()
                                  .Replace("Group C", "").Trim()
                                  .Replace("Group D", "").Trim()
                                  .Replace("Group E", "").Trim()
                                  .Replace("Group F", "").Trim()
                                  .Replace("Group G", "").Trim()
                                  .Replace("Group H", "").Trim()
                                  .Replace("Group I", "").Trim()
                                  .Replace("Group J", "").Trim()
                                  .Replace("Group K", "").Trim()
                                  .Replace("Group L", "").Trim()
                                  .Replace("Group M", "").Trim()
                                  .Replace("Group N", "").Trim()
                                  .Replace("Grup 1", "").Trim()
                                  .Replace("Grup 2", "").Trim()
                                  .Replace("Grup 3", "").Trim()
                                  .Replace("Grup 4", "").Trim()
                                  .Replace("Grup 5", "").Trim()
                                  .Replace("Grup 6", "").Trim()
                                  .Replace("Grup 7", "").Trim()
                                  .Replace("Grup 8", "").Trim()
                                  .Replace("Grup 9", "").Trim()
                                  .Replace("Grup 10", "").Trim()
                                  .Replace("Grup 11", "").Trim()
                                  .Replace("Grup 12", "").Trim()
                                  .Replace("Grup 13", "").Trim()
                                  .Replace("Grup 14", "").Trim()
                                  .Replace("Grup 15", "").Trim()
                                  .Replace("Grup 16", "").Trim()
                                  .Replace("Group 1", "").Trim()
                                  .Replace("Group 2", "").Trim()
                                  .Replace("Group 3", "").Trim()
                                  .Replace("Group 4", "").Trim()
                                  .Replace("Group 5", "").Trim()
                                  .Replace("Group 6", "").Trim()
                                  .Replace("Group 7", "").Trim()
                                  .Replace("Group 8", "").Trim()
                                  .Replace("Group 9", "").Trim()
                                  .Replace("Group 10", "").Trim()
                                  .Replace("Group 11", "").Trim()
                                  .Replace("Group 12", "").Trim()
                                  .Replace("Group 13", "").Trim()
                                  .Replace("Group 14", "").Trim()
                                  .Replace("Group 15", "").Trim()
                                  .Replace("Group 16", "").Trim()
                                  .Replace("2018", "").Trim()
                                  .Replace("2019", "").Trim()
                                  .Replace("2020", "").Trim()
                                  .Replace("2021", "").Trim()
                                  .Replace("2022", "").Trim()
                                  .Replace("2023", "").Trim()
                                  .Replace("2024", "").Trim()
                                  .Replace("Son 64 Turu", "").Trim()
                                  .Replace("Son 32 Turu", "").Trim()
                                  .Replace("Son 16 Turu", "").Trim()
                                  .Replace("Son 8 Turu", "").Trim()
                                  .Replace("Turu", "").Trim()
                                  .Replace("1. Tur", "").Trim()
                                  .Replace("2. Tur", "").Trim()
                                  .Replace("3. Tur", "").Trim()
                                  .Replace("4. Tur", "").Trim()
                                  .Replace("5. Tur", "").Trim()
                                  .Replace("6. Tur", "").Trim()
                                  .Replace("7. Tur", "").Trim()
                                  .Replace("8. Tur", "").Trim()
                                  .Replace("9. Tur", "").Trim()
                                  .Replace("10. Tur", "").Trim()
                                  .Replace("11. Tur", "").Trim()
                                  .Replace("12. Tur", "").Trim()
                                  .Replace("13. Tur", "").Trim()
                                  .Replace("14. Tur", "").Trim()
                                  .Replace("15. Tur", "").Trim()
                                  .Replace("16. Tur", "").Trim()
                                  .Replace("1.Tur", "").Trim()
                                  .Replace("2.Tur", "").Trim()
                                  .Replace("3.Tur", "").Trim()
                                  .Replace("4.Tur", "").Trim()
                                  .Replace("5.Tur", "").Trim()
                                  .Replace("6.Tur", "").Trim()
                                  .Replace("7.Tur", "").Trim()
                                  .Replace("8.Tur", "").Trim()
                                  .Replace("9.Tur", "").Trim()
                                  .Replace("10.Tur", "").Trim()
                                  .Replace("11.Tur", "").Trim()
                                  .Replace("12.Tur", "").Trim()
                                  .Replace("13.Tur", "").Trim()
                                  .Replace("14.Tur", "").Trim()
                                  .Replace("15.Tur", "").Trim()
                                  .Replace("16.Tur", "").Trim()
                                  .Replace("La Liga", "LaLiga").Trim()
                                  .Replace("Canlı", "").Trim();
                    return matchedResult;
                }
            }

            return "NONE";
        }

        public static string ResolveScoreByRegex(this string src, params Regex[] regexes)
        {
            foreach (var regex in regexes)
            {
                if (regex.IsMatch(src))
                {
                    var result = regex.Matches(src)[0].Groups[1].Value.Trim().Replace(" ", string.Empty);
                    if (result.Split("-").Length == 2)
                    {
                        return result;
                    }
                }
            }

            return string.Empty;
        }

        public static string ResolveUnchangableTeamSentenceRegex(this string src, TeamSide teamSide, params Regex[] regexes)
        {
            foreach (var regex in regexes)
            {
                if (regex.IsMatch(src))
                {
                    var result = teamSide == TeamSide.Home
                        ? regex.Matches(src)[0].Groups[1].Value.Split("Takımları")[0].Split("-")[0].Trim()
                        : regex.Matches(src)[0].Groups[1].Value.Split("Takımları")[0].Split("-")[1].Trim();

                    return result;
                }
            }

            return string.Empty;
        }
    }
}

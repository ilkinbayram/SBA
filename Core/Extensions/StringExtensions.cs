using Core.CrossCuttingConcerns.Caching;
using Core.DependencyResolvers;
using Core.Concrete;
using Core.Resources.Enums;
using Core.Utilities.Helpers;
using Core.Utilities.Helpers.Abstracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Utilities.UsableModel;
using Core.Utilities.UsableModel.Test;
using Core.Resources;
using System.Text;
using System.Collections;

namespace Core.Extensions
{
    public static class StringExtensions
    {
        public static string Translate(this string key)
        {
            var _cacheManager = CoreInstanceFactory.GetInstance<ICacheManager>();

            // TODO : Hard Code Should Be Refactored

            var serverLocalizationKey = ConfigHelper.GetSettingsDataStatic<string>(ParentKeySettings.ServerCache_ContainerKeyword.ToString(), ChildKeySettings.Server_Language_CachedForAll.ToString());

            var currentLangOid = Convert.ToInt16(ClientSideStorageHelper.GetLangOidStatic());

            var allResponse = _cacheManager.Get<Dictionary<short, Dictionary<string, string>>>(serverLocalizationKey);

            if (allResponse != null)
            {
                return allResponse.ContainsKey(currentLangOid) && allResponse[currentLangOid].ContainsKey(key)
                    ? allResponse[currentLangOid][key]
                    : key;
            }

            return key;
        }

        public static string TranslateResource(this string key, int lang)
        {
            string result = string.Empty;
            if (lang == 1)
                result = ResourceAz.ResourceManager.GetString(key);
            if (lang == 2)
                result = ResourceTr.ResourceManager.GetString(key);
            return string.IsNullOrEmpty(result) ? key : result;
        }

        public static string GetTextFileByFormat(this string srcFormat, string fileName)
        {
            fileName = string.Format("{0}.txt", fileName.Split('.')[0]);
            return string.Format(srcFormat, fileName);
        }

        public static string GetJsonFileByFormat(this string srcFormat, string fileName)
        {
            fileName = string.Format("{0}.json", fileName.Split('.')[0]);
            return string.Format(srcFormat, fileName);
        }

        public static string ConvertToTimeSpanFormat(this int minutes)
        {
            if (minutes < 0)
            {
                return $"00:0{minutes}";
            }
            else if (minutes >= 0 && minutes < 10)
            {
                return $"00:0{minutes}";
            }
            else if (minutes >= 10 && minutes <= 59)
            {
                return $"00:{minutes}";
            }
            else
            {
                throw new Exception("Your minute limit is not defined!");
            }
        }

        public static string ToHtmlVisualPercentage(this PercentageComplainer input, int minimumPercentage)
        {
            if (input.Percentage >= minimumPercentage)
                return string.Format("<span>{0}</span></br><span>{1} %</span>", input.FeatureName, input.Percentage.ToString());
            else
                return string.Format("<span class='text-danger'><i class='fas fa-times-circle'></i></span>");
        }

        public static string ToHtmlVisualPercentage(this PercentageComplainer input)
        {
            try
            {
                return string.Format("<span>{0}</span></br><span>{1} %</span>", input.FeatureName, input.Percentage.ToString());
            }
            catch
            {
                return string.Format("<span class='text-danger'><i class='fas fa-times-circle'></i></span>");
            }
        }

        public static string ToIntHtmlVisual(this int input)
        {
            return string.Format("<span>{0}</span>", input);
        }

        public static string ToDecimalHtmlVisual(this decimal input)
        {
            return string.Format("<span>{0}</span>", input.ToString("0.00"));
        }

        public static string ToStringHtmlVisual(this string input)
        {
            return string.Format("<span>{0}</span>", input.ToString());
        }

        public static string ToPercentage(this PercentageComplainer input, int minimumPercentage)
        {
            if (input == null) return null;

            if (input.Percentage >= minimumPercentage)
                return string.Format("{0} => {1} %", input.FeatureName, input.Percentage.ToString());
            else
                return null;
        }

        public static string ToPercentage(this PercentageComplainer input)
        {
            if (input != null)
                return string.Format("{0} => {1} %", input.FeatureName, input.Percentage.ToString());
            else
                return null;
        }

        public static string ToIntVisual(this int input)
        {
            return string.Format("{0}", input.ToString());
        }

        public static string ToDecimalVisual(this decimal input)
        {
            if (input >= 0)
                return string.Format("{0}", input.ToString("0.00"));
            else
                return string.Format("{0}", "XXX");
        }


        public static string GetReversedPropertyName(this string src)
        {

            if (src.Contains("Home"))
            {
                src = src.Replace("Home", "Away");
            }

            if (src.Contains("Away"))
            {
                src = src.Replace("Away", "Home");
            }

            return src.Trim().Split("__")[1];
        }

        public static string GetPropertyHomeAwayType(this string src)
        {
            return src.Trim().Split("__")[0];
        }

        public static void InitializeIsCrashable(this ModelTestPercentageUI input, int minimumPercentage)
        {
            if (input.Price1_Perc.Percentage < minimumPercentage &&
                input.Price2_Perc.Percentage < minimumPercentage &&
                input.Price3_Perc.Percentage < minimumPercentage &&
                input.Price4_Perc.Percentage < minimumPercentage)
                throw new Exception("No Valid Property Value Found");
        }


        public static string Translate(this string key, short lang_oid)
        {
            var _cacheManager = CoreInstanceFactory.GetInstance<ICacheManager>();

            var serverLocalizationKey = ConfigHelper.GetSettingsDataStatic<string>(ParentKeySettings.ServerCache_ContainerKeyword.ToString(), ChildKeySettings.Server_Language_CachedForAll.ToString());

            var allResponse = _cacheManager.Get<Dictionary<short, Dictionary<string, string>>>(serverLocalizationKey);

            if (allResponse != null)
            {
                return allResponse.ContainsKey(lang_oid) && allResponse[lang_oid].ContainsKey(key)
                    ? allResponse[lang_oid][key]
                    : key;
            }

            return key;
        }

        public static string Translate(this string key, params string[] insteadParameters)
        {
            var _cacheManager = CoreInstanceFactory.GetInstance<ICacheManager>();

            insteadParameters.ToList().ForEach(x =>
            {
                x = x.Translate();
            });

            var serverLocalizationKey = ConfigHelper.GetSettingsDataStatic<string>(ParentKeySettings.ServerCache_ContainerKeyword.ToString(), ChildKeySettings.Server_Language_CachedForAll.ToString());

            var currentLangOid = Convert.ToInt16(ClientSideStorageHelper.GetLangOidStatic());

            var allResponse = _cacheManager.Get<Dictionary<short, Dictionary<string, string>>>(serverLocalizationKey);

            if (allResponse != null)
            {
                return allResponse.ContainsKey(currentLangOid) && allResponse[currentLangOid].ContainsKey(key)
                    ? string.Format(allResponse[currentLangOid][key], insteadParameters)
                    : key;
            }

            return key;
        }

        public static string Translate(this string key, string insteadParameter)
        {
            var _cacheManager = CoreInstanceFactory.GetInstance<ICacheManager>();

            // TODO : Hard Code Should Be Refactored

            var serverLocalizationKey = ConfigHelper.GetSettingsDataStatic<string>(ParentKeySettings.ServerCache_ContainerKeyword.ToString(), ChildKeySettings.Server_Language_CachedForAll.ToString());

            var currentLangOid = Convert.ToInt16(ClientSideStorageHelper.GetLangOidStatic());

            var allResponse = _cacheManager.Get<Dictionary<short, Dictionary<string, string>>>(serverLocalizationKey);

            if (allResponse != null)
            {
                return allResponse.ContainsKey(currentLangOid) && allResponse[currentLangOid].ContainsKey(key)
                    ? string.Format(allResponse[currentLangOid][key], insteadParameter.Translate())
                    : key;
            }

            return key;
        }

        public static string GetStaticMediaURL(this string configKey)
        {
            var resultRead = ConfigHelper.GetSettingsDataStatic<string>(ParentKeySettings.MediaServiceURL_ContainerKeyword.ToString(), configKey);

            return resultRead;
        }

        public static string ToResponseOverVisualise(this PercentageComplainer input)
        {
            if (input.Percentage >= 50)
            {
                if (input.FeatureName.ToLower() == "true")
                {
                    return $"{input.Percentage}% Over";
                }
                else
                {
                    return $"{input.Percentage}% Under";
                }
            }
            else
            {
                if (input.FeatureName.ToLower() == "true")
                {
                    return $"{100 - input.Percentage}% Under";
                }
                else
                {
                    return $"{100 - input.Percentage}% Over";
                }
            }
        }

        public static string ToResponseOverVisualise(this PercentageComplainer input, string propertyName)
        {
            if (input == null)
                return $"{propertyName} => FUCKED UP!!! XXXXXX";

            if (input.Percentage >= 50)
            {
                if (input.FeatureName.ToLower() == "true")
                {
                    return $"{input.Percentage}% Üst";
                }
                else
                {
                    return $"{input.Percentage}% Alt";
                }
            }
            else
            {
                if (input.FeatureName.ToLower() == "true")
                {
                    return $"{100 - input.Percentage}% Alt";
                }
                else
                {
                    return $"{100 - input.Percentage}% Üst";
                }
            }
        }

        public static string ToResponseWinLoseVisualise(this PercentageComplainer input)
        {
            if (input.Percentage >= 50)
            {
                if(input.FeatureName.ToLower() == "true")
                {
                    return $"{input.Percentage}% Bəli";
                }
                else
                {
                    return $"{input.Percentage}% Xeyr";
                }
            }
            else
            {
                if (input.FeatureName.ToLower() == "true")
                {
                    return $"{100 - input.Percentage}% Xeyr";
                }
                else
                {
                    return $"{100 - input.Percentage}% Bəli";
                }
            }
        }

        public static string ToResponseWinLoseVisualise(this PercentageComplainer input, string propertyName)
        {
            if (input == null)
                return $"{propertyName} => FUCKED UP!!! XXXXXX";

            if (input.Percentage >= 50)
            {
                if (input.FeatureName.ToLower() == "true")
                {
                    return $"{input.Percentage}% Bəli";
                }
                else
                {
                    return $"{input.Percentage}% Xeyr";
                }
            }
            else
            {
                if (input.FeatureName.ToLower() == "true")
                {
                    return $"{100 - input.Percentage}% Xeyr";
                }
                else
                {
                    return $"{100 - input.Percentage}% Bəli";
                }
            }
        }

        public static string ToResponseOverVisualise(this int input)
        {
            if (input > 50)
            {
                return $"{input}% Over";
            }
            else
            {
                return $"{100 - input}% Under";
            }
        }

        public static string ToResponseWinLoseVisualise(this int input)
        {
            if (input >= 50)
            {
                return $"{input}% Win";
            }
            else
            {
                return $"{100 - input}% Lose";
            }
        }

        public static string ToResponseBothGoalVisualise(this PercentageComplainer input)
        {
            if (input.Percentage >= 50)
            {
                if (input.FeatureName.ToLower() == "true")
                {
                    return $"{input.Percentage}% Yes";
                }
                else
                {
                    return $"{input.Percentage}% No";
                }
            }
            else
            {
                if (input.FeatureName.ToLower() == "true")
                {
                    return $"{100 - input.Percentage}% Yes";
                }
                else
                {
                    return $"{100 - input.Percentage}% No";
                }
            }
        }

        public static string ToResponseBothGoalVisualise(this int input)
        {
            if(input > 50)
            {
                return $"{input}% Yes";
            }
            else
            {
                return $"{100 - input}% No";
            }
        }


        public static string SerializeSpecial(this object obj)
        {
            StringBuilder sb = new StringBuilder();
            SerializeSpecialInternal(obj, sb);
            return sb.ToString();
        }

        private static void SerializeSpecialInternal(object obj, StringBuilder sb)
        {
            Type objType = obj.GetType();
            var properties = objType.GetProperties();

            sb.Append("[");
            bool isFirst = true;
            foreach (var property in properties)
            {
                object value = property.GetValue(obj);

                if (value == null || (IsList(value) && ((IList)value).Count == 0)) continue;

                if (!isFirst) sb.Append(",");
                isFirst = false;

                sb.Append(property.Name);
                sb.Append("=");

                if (IsSimpleType(property.PropertyType))
                {
                    if (property.PropertyType == typeof(string))
                    {
                        sb.Append($"'{value}'");
                    }
                    else
                    {
                        sb.Append(value);
                        if (property.Name.EndsWith("Percent"))
                        {
                            sb.Append("%");
                        }
                    }
                }
                else if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
                {
                    sb.Append("(");
                    bool isFirstItem = true;
                    foreach (var item in (IEnumerable)value)
                    {
                        if (!isFirstItem) sb.Append(",");
                        isFirstItem = false;
                        SerializeSpecialInternal(item, sb);
                    }
                    sb.Append(")");
                }
                else
                {
                    SerializeSpecialInternal(value, sb);
                }
            }
            sb.Append("]");
        }

        private static bool IsSimpleType(Type type) =>
            type.IsPrimitive || type == typeof(string) || type == typeof(decimal) || type == typeof(DateTime);

        private static bool IsList(object obj) => typeof(IList).IsAssignableFrom(obj.GetType());
    }
}

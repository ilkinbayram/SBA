using Core.Utilities.UsableModel;
using System;
using System.Collections.Generic;

namespace Core.Extensions
{
    public static class BetGeneralModelExtensions
    {
        public static List<TimeSerialContainer> MapToNewListTimeSerials(this List<TimeSerialContainer> timeSerials, bool analyseAnyTime = false)
        {
            List<TimeSerialContainer> result = new List<TimeSerialContainer>();

            foreach (var item in timeSerials)
            {
                if (!item.IsAnalysed)
                {
                    if (CheckIsAddable(item, analyseAnyTime))
                        result.Add(new TimeSerialContainer { Serial = item.Serial, Time = item.Time });
                }
            }

            return result;
        }

        private static bool CheckIsAddable(TimeSerialContainer model, bool analyseAnyTime = false)
        {
            if (analyseAnyTime) return analyseAnyTime;

            if (model.IsAnalysed || string.IsNullOrEmpty(model.Time) || model.Time.Length < 5) return false;

            var info = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
            DateTimeOffset localServerTime = DateTimeOffset.Now;
            DateTimeOffset localTime = TimeZoneInfo.ConvertTime(localServerTime, info);

            TimeSpan nowTime = localTime.TimeOfDay;

            TimeSpan minLimit = nowTime.Subtract(TimeSpan.Parse("00:05"));
            TimeSpan maxLimit = nowTime.Add(TimeSpan.Parse("00:20"));
            TimeSpan matchTime = TimeSpan.Parse(model.Time);

            return minLimit <= matchTime && maxLimit >= matchTime;
        }
    }
}

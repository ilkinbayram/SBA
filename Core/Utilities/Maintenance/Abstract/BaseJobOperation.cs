using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.UsableModel.TempTableModels.Country;
using Core.Utilities.UsableModel.TempTableModels.Initialization;
using System;
using System.Collections.Generic;

namespace Core.Utilities.Maintenance.Abstract
{
    public abstract class BaseJobOperation
    {
        public BaseJobOperation()
        {
        }

        public virtual void Execute()
        {
        }

        public virtual void ExecuteTTT(List<FilterResult> filterResults, string path)
        {
        }

        public virtual void ExecuteTTT2(List<string> serials, string path, LeagueContainer league, CountryContainerTemp countryContainer)
        {
        }

        public virtual void Execute_TEST()
        {
        }

        public bool CompareIsAnalysable(string time, int addMinute, bool analyseAnyTime = false)
        {
            if (analyseAnyTime) return analyseAnyTime;

            var info = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
            DateTimeOffset localServerTime = DateTimeOffset.Now;
            DateTimeOffset localTime = TimeZoneInfo.ConvertTime(localServerTime, info);

            TimeSpan nowTime = localTime.TimeOfDay;
            TimeSpan generatedTimeNow = nowTime.Add(TimeSpan.Parse(addMinute.ConvertToTimeSpanFormat()));
            TimeSpan matchTime = TimeSpan.Parse(time);

            return matchTime <= generatedTimeNow;
        }

    }
}

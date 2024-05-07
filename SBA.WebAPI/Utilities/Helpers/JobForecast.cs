using Core.Entities.Concrete.ExternalDbEntities;
using Newtonsoft.Json;

namespace SBA.WebAPI.Utilities.Helpers
{
    public class JobForecast
    {
        public JobForecast()
        {
            IsFT_25Over = new ForecastDetails();
            IsFT_15Over = new ForecastDetails();
            IsFT_GG = new ForecastDetails();
            IsFT_NG = new ForecastDetails();
            IsHT_05Over = new ForecastDetails();
            IsSH_05Over = new ForecastDetails();
            IsFT_35Under = new ForecastDetails();
            IsHT_15Under = new ForecastDetails();
            IsSH_15Under = new ForecastDetails();
            IsHome_FT_05_Over = new ForecastDetails();
            IsHome_FT_15_Over = new ForecastDetails();
            IsHome_HT_05_Over = new ForecastDetails();
            IsHome_SH_05_Over = new ForecastDetails();
            IsAway_FT_05_Over = new ForecastDetails();
            IsAway_FT_15_Over = new ForecastDetails();
            IsAway_HT_05_Over = new ForecastDetails();
            IsAway_SH_05_Over = new ForecastDetails();
            IsHome_FT_Win = new ForecastDetails();
            IsHome_HT_Win = new ForecastDetails();
            IsHome_SH_Win = new ForecastDetails();
            IsAway_FT_Win = new ForecastDetails();
            IsAway_HT_Win = new ForecastDetails();
            IsAway_SH_Win = new ForecastDetails();
            Is_FT_Draw = new ForecastDetails();
            Is_HT_Draw = new ForecastDetails();
            Is_SH_Draw = new ForecastDetails();
            Is_FT_Win1_Or_X = new ForecastDetails();
            Is_HT_Win1_Or_X = new ForecastDetails();
            Is_SH_Win1_Or_X = new ForecastDetails();
            Is_FT_X_Or_Win2 = new ForecastDetails();
            Is_HT_X_Or_Win2 = new ForecastDetails();
            Is_SH_X_Or_Win2 = new ForecastDetails();
            Is_FT_Win1_Or_Win2 = new ForecastDetails();
            Is_HT_Win1_Or_Win2 = new ForecastDetails();
            Is_SH_Win1_Or_Win2 = new ForecastDetails();
        }

        public string Link { get; set; }
        public string Match { get; set; }
        public string Country { get; set; }
        public string League { get; set; }
        public string ExtraDetails { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ForecastDetails IsFT_25Over { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ForecastDetails IsFT_15Over { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ForecastDetails IsFT_GG { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ForecastDetails IsFT_NG { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ForecastDetails IsHT_05Over { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ForecastDetails IsSH_05Over { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ForecastDetails IsFT_35Under { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ForecastDetails IsHT_15Under { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ForecastDetails IsSH_15Under { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ForecastDetails IsHome_FT_05_Over { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ForecastDetails IsHome_FT_15_Over { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ForecastDetails IsHome_HT_05_Over { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ForecastDetails IsHome_SH_05_Over { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ForecastDetails IsAway_FT_05_Over { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ForecastDetails IsAway_FT_15_Over { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ForecastDetails IsAway_HT_05_Over { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ForecastDetails IsAway_SH_05_Over { get; set; }


        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ForecastDetails IsHome_FT_Win { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ForecastDetails IsHome_HT_Win { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ForecastDetails IsHome_SH_Win { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ForecastDetails IsAway_FT_Win { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ForecastDetails IsAway_HT_Win { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ForecastDetails IsAway_SH_Win { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ForecastDetails Is_FT_Draw { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ForecastDetails Is_HT_Draw { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ForecastDetails Is_SH_Draw { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ForecastDetails Is_FT_Win1_Or_X { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ForecastDetails Is_HT_Win1_Or_X { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ForecastDetails Is_SH_Win1_Or_X { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ForecastDetails Is_FT_X_Or_Win2 { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ForecastDetails Is_HT_X_Or_Win2 { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ForecastDetails Is_SH_X_Or_Win2 { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ForecastDetails Is_FT_Win1_Or_Win2 { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ForecastDetails Is_HT_Win1_Or_Win2 { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ForecastDetails Is_SH_Win1_Or_Win2 { get; set; }


        public MatchIdentifier MatchIdentifier { get; set; }
    }

    public class ForecastDetails
    {
        public ForecastDetails()
        {
            IsAcceptable = false;
            Value = 0;
        }

        public bool IsAcceptable { get; set; }
        public int Value { get; set; }
    }
}

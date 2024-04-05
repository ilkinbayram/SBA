using Core.Entities.Concrete.Base;

namespace Core.Entities.Concrete.ExternalDbEntities
{
    public class MatchIdentifier : Identifier, IEntity
    {
        public MatchIdentifier()
        {
            if (string.IsNullOrEmpty(HomeTeam)) HomeTeam = string.Empty;
            if (string.IsNullOrEmpty(AwayTeam)) AwayTeam = string.Empty;
            if (string.IsNullOrEmpty(HT_Result)) HT_Result = string.Empty;
            if (string.IsNullOrEmpty(FT_Result)) FT_Result = string.Empty;
        }
        public int Serial { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string HT_Result { get; set; }
        public string FT_Result { get; set; }
        public DateTime MatchDateTime { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime ModifiedDateTime { get; set; }

        public string? CreatedBy { get; set; }

        public string? ModifiedBy { get; set; }

        public virtual List<Forecast> Forecasts { get; set; }
    }
}

using Core.Entities.Concrete.Base;

namespace Core.Entities.Concrete.ExternalDbEntities
{
    public class MatchIdentifier : Identifier, IEntity
    {
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

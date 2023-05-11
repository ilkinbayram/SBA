using Core.Entities.Concrete.Base;

namespace Core.Entities.Concrete.ExternalDbEntities
{
    public class Forecast : Identifier, IEntity
    {
        public Forecast()
        {            
        }

        public Forecast(string key, MatchIdentifier matchIdentity)
        {
            Key = key;
            IsSuccess = false;
            IsChecked = false;
            MatchIdentifierId = matchIdentity.Id;
            MatchIdentifier = matchIdentity;
        }


        public string Key { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsChecked { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime ModifiedDateTime { get; set; }

        public string? CreatedBy { get; set; }

        public string? ModifiedBy { get; set; }

        public int MatchIdentifierId { get; set; }
        public virtual MatchIdentifier MatchIdentifier { get; set; }
    }
}

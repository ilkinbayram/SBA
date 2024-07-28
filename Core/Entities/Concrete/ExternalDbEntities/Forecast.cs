using Core.Entities.Concrete.Base;

namespace Core.Entities.Concrete.ExternalDbEntities
{
    public class Forecast : Identifier, IEntity
    {
        public Forecast()
        {            
        }

        public Forecast(string key, int serial, MatchIdentifier matchIdentity)
        {
            Key = key;
            IsSuccess = false;
            IsChecked = false;
            MatchIdentifierId = matchIdentity.Id;
            Serial = serial;
            Is99Percent = false;
        }

        public Forecast(string key, MatchIdentifier matchIdentity)
        {
            Key = key;
            IsSuccess = false;
            IsChecked = false;
            MatchIdentifierId = matchIdentity.Id;
            Serial = matchIdentity.Serial;
            Is99Percent = false;
        }

        public Forecast(string key, int serial, MatchIdentifier matchIdentity, bool is99Percent)
        {
            Key = key;
            IsSuccess = false;
            IsChecked = false;
            MatchIdentifierId = matchIdentity.Id;
            Serial = serial;
            Is99Percent = is99Percent;
        }


        public int Serial { get; set; }
        public string Key { get; set; }

        public bool IsSuccess { get; set; }
        public bool IsChecked { get; set; }

        public bool Is99Percent { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime ModifiedDateTime { get; set; }

        public string? CreatedBy { get; set; }

        public string? ModifiedBy { get; set; }

        public int MatchIdentifierId { get; set; }
        public virtual MatchIdentifier MatchIdentifier { get; set; }
    }
}

using Core.Entities.Concrete.Base;

namespace Core.Entities.Concrete.ExternalDbEntities
{
    public class MatchIdentifier : Identifier, IEntity
    {
        public int Serial { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public DateTime MatchDateTime { get; set; }
    }
}

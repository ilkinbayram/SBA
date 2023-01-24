using System;

namespace Core.Entities.Concrete.SqlEntities.QueryModels
{
    public class MatchBetQM
    {
        public MatchBetQM()
        {
        }

        public string Country { get; set; }
        public int SerialUniqueID { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string HT_Match_Result { get; set; }
        public string FT_Match_Result { get; set; }
        public DateTime MatchDate { get; set; }
    }
}

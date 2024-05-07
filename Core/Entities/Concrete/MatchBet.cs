using Core.Concrete.Base;
using Core.Resources.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Entities.Concrete
{
    public class MatchBet : BaseEntity, IEntity
    {
        public MatchBet()
        {
            ModelType = ProjectModelType.MatchBet;
        }

        [BsonElement("country")]
        public string Country { get; set; }

        [BsonElement("serialUniqueID")]
        public int SerialUniqueID { get; set; }

        [BsonElement("leagueName")]
        public string LeagueName { get; set; }

        [BsonElement("leagueId")]
        public int LeagueId { get; set; }

        [BsonElement("homeTeam")]
        public string HomeTeam { get; set; }

        [BsonElement("awayTeam")]
        public string AwayTeam { get; set; }

        [BsonElement("ht_Match_Result")]
        public string HT_Match_Result { get; set; }

        [BsonElement("ft_Match_Result")]
        public string FT_Match_Result { get; set; }


        [BsonElement("ftWin1_Odd")]
        public decimal FTWin1_Odd { get; set; }

        [BsonElement("ftDraw_Odd")]
        public decimal FTDraw_Odd { get; set; }

        [BsonElement("ftWin2_Odd")]
        public decimal FTWin2_Odd { get; set; }

        [BsonElement("htWin1_Odd")]
        public decimal HTWin1_Odd { get; set; }

        [BsonElement("htDraw_Odd")]
        public decimal HTDraw_Odd { get; set; }

        [BsonElement("htWin2_Odd")]
        public decimal HTWin2_Odd { get; set; }

        [BsonElement("ht_Under_1_5_Odd")]
        public decimal HT_Under_1_5_Odd { get; set; }

        [BsonElement("ht_Over_1_5_Odd")]
        public decimal HT_Over_1_5_Odd { get; set; }

        [BsonElement("ft_Under_1_5_Odd")]
        public decimal FT_Under_1_5_Odd { get; set; }

        [BsonElement("ft_Over_1_5_Odd")]
        public decimal FT_Over_1_5_Odd { get; set; }

        [BsonElement("ft_Under_2_5_Odd")]
        public decimal FT_Under_2_5_Odd { get; set; }

        [BsonElement("ft_Over_2_5_Odd")]
        public decimal FT_Over_2_5_Odd { get; set; }

        [BsonElement("ft_Under_3_5_Odd")]
        public decimal FT_Under_3_5_Odd { get; set; }

        [BsonElement("ft_Over_3_5_Odd")]
        public decimal FT_Over_3_5_Odd { get; set; }

        [BsonElement("ft_GG_Odd")]
        public decimal FT_GG_Odd { get; set; }

        [BsonElement("ft_NG_Odd")]
        public decimal FT_NG_Odd { get; set; }

        [BsonElement("ft_01_Odd")]
        public decimal FT_01_Odd { get; set; }

        [BsonElement("ft_23_Odd")]
        public decimal FT_23_Odd { get; set; }

        [BsonElement("ft_45_Odd")]
        public decimal FT_45_Odd { get; set; }

        [BsonElement("ft_6_Odd")]
        public decimal FT_6_Odd { get; set; }

        [BsonElement("matchDate")]
        public DateTime MatchDate { get; set; }

        [BsonElement("isCountryLeagueUpdated")]
        public bool IsCountryLeagueUpdated { get; set; }
    }
}

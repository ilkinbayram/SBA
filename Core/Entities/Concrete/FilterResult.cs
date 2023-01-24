using Core.Concrete.Base;
using Core.Resources.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Entities.Concrete
{
    public class FilterResult : BaseEntity, IEntity
    {
        public FilterResult()
        {
            ModelType = ProjectModelType.FilterResult;
        }

        [BsonElement("serialUniqueID")]
        public int SerialUniqueID { get; set; }

        [BsonElement("ht_Result")]
        public int HT_Result { get; set; }

        [BsonElement("sh_Result")]
        public int SH_Result { get; set; }

        [BsonElement("ft_Result")]
        public int FT_Result { get; set; }

        [BsonElement("moreGoalsBetweenTimes")]
        public int MoreGoalsBetweenTimes { get; set; }

        [BsonElement("home_HT_0_5_Over")]
        public bool Home_HT_0_5_Over { get; set; }

        [BsonElement("home_HT_1_5_Over")]
        public bool Home_HT_1_5_Over { get; set; }

        [BsonElement("home_SH_0_5_Over")]
        public bool Home_SH_0_5_Over { get; set; }

        [BsonElement("home_SH_1_5_Over")]
        public bool Home_SH_1_5_Over { get; set; }

        [BsonElement("away_HT_0_5_Over")]
        public bool Away_HT_0_5_Over { get; set; }

        [BsonElement("away_HT_1_5_Over")]
        public bool Away_HT_1_5_Over { get; set; }

        [BsonElement("away_SH_0_5_Over")]
        public bool Away_SH_0_5_Over { get; set; }

        [BsonElement("away_SH_1_5_Over")]
        public bool Away_SH_1_5_Over { get; set; }

        [BsonElement("away_Win_Any_Half")]
        public bool Away_Win_Any_Half { get; set; }

        [BsonElement("home_Win_Any_Half")]
        public bool Home_Win_Any_Half { get; set; }

        [BsonElement("away_FT_0_5_Over")]
        public bool Away_FT_0_5_Over { get; set; }

        [BsonElement("away_FT_1_5_Over")]
        public bool Away_FT_1_5_Over { get; set; }

        [BsonElement("home_FT_0_5_Over")]
        public bool Home_FT_0_5_Over { get; set; }

        [BsonElement("home_FT_1_5_Over")]
        public bool Home_FT_1_5_Over { get; set; }

        [BsonElement("ht_ft_Result")]
        public HalfFullResultEnum HT_FT_Result { get; set; }

        [BsonElement("ht_0_5_Over")]
        public bool HT_0_5_Over { get; set; }

        [BsonElement("ht_1_5_Over")]
        public bool HT_1_5_Over { get; set; }

        [BsonElement("sh_0_5_Over")]
        public bool SH_0_5_Over { get; set; }

        [BsonElement("sh_1_5_Over")]
        public bool SH_1_5_Over { get; set; }

        [BsonElement("ft_1_5_Over")]
        public bool FT_1_5_Over { get; set; }

        [BsonElement("ft_2_5_Over")]
        public bool FT_2_5_Over { get; set; }

        [BsonElement("ft_3_5_Over")]
        public bool FT_3_5_Over { get; set; }

        [BsonElement("ft_GG")]
        public bool FT_GG { get; set; }

        [BsonElement("ht_GG")]
        public bool HT_GG { get; set; }

        [BsonElement("sh_GG")]
        public bool SH_GG { get; set; }

        [BsonElement("ft_TotalBetween")]
        public int FT_TotalBetween { get; set; }
    }
}
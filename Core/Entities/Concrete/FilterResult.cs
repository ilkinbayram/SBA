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

        [BsonElement("isCornerFound")]
        public bool IsCornerFound { get; set; }

        [BsonElement("corner_7_5_Over")]
        public bool Corner_7_5_Over { get; set; }

        [BsonElement("corner_8_5_Over")]
        public bool Corner_8_5_Over { get; set; }

        [BsonElement("corner_9_5_Over")]
        public bool Corner_9_5_Over { get; set; }

        [BsonElement("corner_Home_3_5_Over")]
        public bool Corner_Home_3_5_Over { get; set; }

        [BsonElement("corner_Home_4_5_Over")]
        public bool Corner_Home_4_5_Over { get; set; }

        [BsonElement("corner_Home_5_5_Over")]
        public bool Corner_Home_5_5_Over { get; set; }

        [BsonElement("corner_Away_3_5_Over")]
        public bool Corner_Away_3_5_Over { get; set; }

        [BsonElement("corner_Away_4_5_Over")]
        public bool Corner_Away_4_5_Over { get; set; }

        [BsonElement("corner_Away_5_5_Over")]
        public bool Corner_Away_5_5_Over { get; set; }

        [BsonElement("homeCornerCount")]
        public int HomeCornerCount { get; set; }

        [BsonElement("awayCornerCount")]
        public int AwayCornerCount { get; set; }


        [BsonElement("homePossesion")]
        public int HomePossesion { get; set; }

        [BsonElement("awayPossesion")]
        public int AwayPossesion { get; set; }


        [BsonElement("homeShotCount")]
        public int HomeShotCount { get; set; }

        [BsonElement("awayShotCount")]
        public int AwayShotCount { get; set; }



        [BsonElement("homeShotOnTargetCount")]
        public int HomeShotOnTargetCount { get; set; }

        [BsonElement("awayShotOnTargetCount")]
        public int AwayShotOnTargetCount { get; set; }


        [BsonElement("isPossesionFound")]
        public bool IsPossesionFound { get; set; }

        [BsonElement("isShotFound")]
        public bool IsShotFound { get; set; }

        [BsonElement("isShotOnTargetFound")]
        public bool IsShotOnTargetFound { get; set; }


        [BsonElement("is_Corner_FT_Win1")]
        public bool Is_Corner_FT_Win1 { get; set; }

        [BsonElement("is_Corner_FT_X")]
        public bool Is_Corner_FT_X { get; set; }

        [BsonElement("is_Corner_FT_Win2")]
        public bool Is_Corner_FT_Win2 { get; set; }


        [BsonElement("is_FT_Win1")]
        public bool Is_FT_Win1 { get; set; }

        [BsonElement("is_FT_X")]
        public bool Is_FT_X { get; set; }

        [BsonElement("is_FT_Win2")]
        public bool Is_FT_Win2 { get; set; }


        [BsonElement("is_HT_Win1")]
        public bool Is_HT_Win1 { get; set; }

        [BsonElement("is_HT_X")]
        public bool Is_HT_X { get; set; }

        [BsonElement("is_HT_Win2")]
        public bool Is_HT_Win2 { get; set; }


        [BsonElement("is_SH_Win1")]
        public bool Is_SH_Win1 { get; set; }

        [BsonElement("is_SH_X")]
        public bool Is_SH_X { get; set; }

        [BsonElement("is_SH_Win2")]
        public bool Is_SH_Win2 { get; set; }
    }
}
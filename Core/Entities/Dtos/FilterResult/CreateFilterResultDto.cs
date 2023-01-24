using Core.Entities.Dtos.Base;
using Core.Resources.Enums;
using Core.Utilities.UsableModel;
using System.Collections.Generic;

namespace Core.Entities.Dtos.FilterResult
{
    public class CreateFilterResultDto : BaseDto
    {
        public CreateFilterResultDto()
        {
            ResponseMessages = new List<AlertResult>();
            ModelType = ProjectModelType.FilterResult;
        }
        public int SerialUniqueID { get; set; }
        public int HT_Result { get; set; }
        public int SH_Result { get; set; }
        public int FT_Result { get; set; }
        public int MoreGoalsBetweenTimes { get; set; }
        public bool Home_0_5_Over { get; set; }
        public bool Home_1_5_Over { get; set; }
        public bool Away_0_5_Over { get; set; }
        public bool Away_1_5_Over { get; set; }
        public HalfFullResultEnum HT_FT_Result { get; set; }
        public bool HT_0_5_Over { get; set; }
        public bool HT_1_5_Over { get; set; }
        public bool HT_2_5_Over { get; set; }
        public bool SH_0_5_Over { get; set; }
        public bool SH_1_5_Over { get; set; }
        public bool SH_2_5_Over { get; set; }
        public bool FT_1_5_Over { get; set; }
        public bool FT_2_5_Over { get; set; }
        public bool FT_3_5_Over { get; set; }
        public bool FT_4_5_Over { get; set; }
        public bool FT_GG { get; set; }
        public bool HT_GG { get; set; }
        public bool SH_GG { get; set; }
        public int FT_TotalBetween { get; set; }

        public List<AlertResult> ResponseMessages { get; set; }
    }
}

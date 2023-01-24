using Core.Entities.Dtos.Base;
using Core.Resources.Enums;

namespace Core.Entities.Dtos.UnstartedMatchBetTemp
{
    public class GetUnstartedMatchBetTempDto : BaseDto
    {
        public GetUnstartedMatchBetTempDto()
        {
            ModelType = ProjectModelType.UnstartedMatchBetTemp;
        }

        public TempMatchBetDefiner TempDefinerChanges { get; set; }
        public string Country { get; set; }
        public int SerialUniqueID { get; set; }
        public string LeagueName { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }

        public decimal FTWin1_Odd { get; set; }
        public decimal FTDraw_Odd { get; set; }
        public decimal FTWin2_Odd { get; set; }
        public decimal HTWin1_Odd { get; set; }
        public decimal HTDraw_Odd { get; set; }
        public decimal HTWin2_Odd { get; set; }
        public decimal HT_Under_1_5_Odd { get; set; }
        public decimal HT_Over_1_5_Odd { get; set; }
        public decimal FT_Under_1_5_Odd { get; set; }
        public decimal FT_Over_1_5_Odd { get; set; }
        public decimal FT_Under_2_5_Odd { get; set; }
        public decimal FT_Over_2_5_Odd { get; set; }
        public decimal FT_Under_3_5_Odd { get; set; }
        public decimal FT_Over_3_5_Odd { get; set; }
        public decimal FT_GG_Odd { get; set; }
        public decimal FT_NG_Odd { get; set; }
        public decimal FT_01_Odd { get; set; }
        public decimal FT_23_Odd { get; set; }
        public decimal FT_45_Odd { get; set; }
        public decimal FT_6_Odd { get; set; }
    }
}

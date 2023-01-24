using Core.Entities.Dtos.Base;
using Core.Resources.Enums;
using Core.Utilities.UsableModel;
using System;
using System.Collections.Generic;

namespace Core.Entities.Dtos.MatchBet
{
    public class CreateMatchBetDto : BaseDto
    {
        public CreateMatchBetDto()
        {
            ResponseMessages = new List<AlertResult>();
            ModelType = ProjectModelType.MatchBet;
        }

        public string Country { get; set; }
        public int SerialUniqueID { get; set; }
        public string LeagueName { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string HT_Match_Result { get; set; }
        public string FT_Match_Result { get; set; }

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
        public DateTime MatchDate { get; set; }

        public List<AlertResult> ResponseMessages { get; set; }
    }
}

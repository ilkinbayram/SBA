using Core.Entities.Dtos.Base;
using Core.Resources.Enums;

namespace Core.Entities.Dtos.OddChangesEqualiser
{
    public class EditOddChangesEqualiserDto : BaseDto
    {
        public EditOddChangesEqualiserDto()
        {
            ModelType = ProjectModelType.OddChangesEqualiser;
        }

        public decimal FTWin1_Odd__Increment { get; set; }
        public decimal FTDraw_Odd__Increment { get; set; }
        public decimal FTWin2_Odd__Increment { get; set; }
        public decimal HTWin1_Odd__Increment { get; set; }
        public decimal HTDraw_Odd__Increment { get; set; }
        public decimal HTWin2_Odd__Increment { get; set; }
        public decimal HT_Under_1_5_Odd__Increment { get; set; }
        public decimal HT_Over_1_5_Odd__Increment { get; set; }
        public decimal FT_Under_1_5_Odd__Increment { get; set; }
        public decimal FT_Over_1_5_Odd__Increment { get; set; }
        public decimal FT_Under_2_5_Odd__Increment { get; set; }
        public decimal FT_Over_2_5_Odd__Increment { get; set; }
        public decimal FT_Under_3_5_Odd__Increment { get; set; }
        public decimal FT_Over_3_5_Odd__Increment { get; set; }
        public decimal FT_GG_Odd__Increment { get; set; }
        public decimal FT_NG_Odd__Increment { get; set; }
        public decimal FT_01_Odd__Increment { get; set; }
        public decimal FT_23_Odd__Increment { get; set; }
        public decimal FT_45_Odd__Increment { get; set; }
        public decimal FT_6_Odd__Increment { get; set; }


        public decimal FTWin1_Odd__Decrement { get; set; }
        public decimal FTDraw_Odd__Decrement { get; set; }
        public decimal FTWin2_Odd__Decrement { get; set; }
        public decimal HTWin1_Odd__Decrement { get; set; }
        public decimal HTDraw_Odd__Decrement { get; set; }
        public decimal HTWin2_Odd__Decrement { get; set; }
        public decimal HT_Under_1_5_Odd__Decrement { get; set; }
        public decimal HT_Over_1_5_Odd__Decrement { get; set; }
        public decimal FT_Under_1_5_Odd__Decrement { get; set; }
        public decimal FT_Over_1_5_Odd__Decrement { get; set; }
        public decimal FT_Under_2_5_Odd__Decrement { get; set; }
        public decimal FT_Over_2_5_Odd__Decrement { get; set; }
        public decimal FT_Under_3_5_Odd__Decrement { get; set; }
        public decimal FT_Over_3_5_Odd__Decrement { get; set; }
        public decimal FT_GG_Odd__Decrement { get; set; }
        public decimal FT_NG_Odd__Decrement { get; set; }
        public decimal FT_01_Odd__Decrement { get; set; }
        public decimal FT_23_Odd__Decrement { get; set; }
        public decimal FT_45_Odd__Decrement { get; set; }
        public decimal FT_6_Odd__Decrement { get; set; }
    }
}

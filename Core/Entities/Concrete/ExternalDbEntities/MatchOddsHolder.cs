using Core.Entities.Concrete.Base;

namespace Core.Entities.Concrete.ExternalDbEntities
{
    public class MatchOddsHolder : Identifier, IEntity
    {
        public int Serial { get; set; }
        public decimal FT_Win1 { get; set; }
        public decimal FT_Draw { get; set; }
        public decimal FT_Win2 { get; set; }
        public decimal HT_FT_Home_Home { get; set; }
        public decimal HT_FT_Home_Draw { get; set; }
        public decimal HT_FT_Home_Away { get; set; }
        public decimal HT_FT_Draw_Home { get; set; }
        public decimal HT_FT_Draw_Draw { get; set; }
        public decimal HT_FT_Draw_Away { get; set; }
        public decimal HT_FT_Away_Home { get; set; }
        public decimal HT_FT_Away_Draw { get; set; }
        public decimal HT_FT_Away_Away { get; set; }
        public decimal FT_Win1_Under_15 { get; set; }
        public decimal FT_Draw_Under_15 { get; set; }
        public decimal FT_Win2_Under_15 { get; set; }
        public decimal FT_Win1_Over_15 { get; set; }
        public decimal FT_Draw_Over_15 { get; set; }
        public decimal FT_Win2_Over_15 { get; set; }
        public decimal FT_Win1_Under_25 { get; set; }
        public decimal FT_Draw_Under_25 { get; set; }
        public decimal FT_Win2_Under_25 { get; set; }
        public decimal FT_Win1_Over_25 { get; set; }
        public decimal FT_Draw_Over_25 { get; set; }
        public decimal FT_Win2_Over_25 { get; set; }
        public decimal FT_Win1_Under_35 { get; set; }
        public decimal FT_Draw_Under_35 { get; set; }
        public decimal FT_Win2_Under_35 { get; set; }
        public decimal FT_Win1_Over_35 { get; set; }
        public decimal FT_Draw_Over_35 { get; set; }
        public decimal FT_Win2_Over_35 { get; set; }
        public decimal FT_Win1_Under_45 { get; set; }
        public decimal FT_Draw_Under_45 { get; set; }
        public decimal FT_Win2_Under_45 { get; set; }
        public decimal FT_Win1_Over_45 { get; set; }
        public decimal FT_Draw_Over_45 { get; set; }
        public decimal FT_Win2_Over_45 { get; set; }
        public decimal Handicap_04_Win1 { get; set; }
        public decimal Handicap_04_Draw { get; set; }
        public decimal Handicap_04_Win2 { get; set; }
        public decimal Handicap_03_Win1 { get; set; }
        public decimal Handicap_03_Draw { get; set; }
        public decimal Handicap_03_Win2 { get; set; }
        public decimal Handicap_02_Win1 { get; set; }
        public decimal Handicap_02_Draw { get; set; }
        public decimal Handicap_02_Win2 { get; set; }
        public decimal Handicap_01_Win1 { get; set; }
        public decimal Handicap_01_Draw { get; set; }
        public decimal Handicap_01_Win2 { get; set; }

        public decimal Handicap_40_Win1 { get; set; }
        public decimal Handicap_40_Draw { get; set; }
        public decimal Handicap_40_Win2 { get; set; }
        public decimal Handicap_30_Win1 { get; set; }
        public decimal Handicap_30_Draw { get; set; }
        public decimal Handicap_30_Win2 { get; set; }
        public decimal Handicap_20_Win1 { get; set; }
        public decimal Handicap_20_Draw { get; set; }
        public decimal Handicap_20_Win2 { get; set; }
        public decimal Handicap_10_Win1 { get; set; }
        public decimal Handicap_10_Draw { get; set; }
        public decimal Handicap_10_Win2 { get; set; }


        public decimal FT_Double_1_X { get; set; }
        public decimal FT_Double_1_2 { get; set; }
        public decimal FT_Double_X_2 { get; set; }

        public decimal FirstGoal_Home { get; set; }
        public decimal FirstGoal_None { get; set; }
        public decimal FirstGoal_Away { get; set; }

        public decimal FT_4_5_Under { get; set; }
        public decimal FT_4_5_Over { get; set; }
        public decimal FT_5_5_Under { get; set; }
        public decimal FT_5_5_Over { get; set; }
        public decimal HT_0_5_Under { get; set; }
        public decimal HT_0_5_Over { get; set; }
        public decimal HT_2_5_Under { get; set; }
        public decimal HT_2_5_Over { get; set; }

        public decimal Home_2_5_Under { get; set; }
        public decimal Home_2_5_Over { get; set; }
        public decimal Home_3_5_Under { get; set; }
        public decimal Home_3_5_Over { get; set; }
        public decimal Home_4_5_Under { get; set; }
        public decimal Home_4_5_Over { get; set; }
        public decimal Away_2_5_Under { get; set; }
        public decimal Away_2_5_Over { get; set; }
        public decimal Away_3_5_Under { get; set; }
        public decimal Away_3_5_Over { get; set; }
        public decimal Away_4_5_Under { get; set; }
        public decimal Away_4_5_Over { get; set; }

        public decimal Even_Tek { get; set; }
        public decimal Odd_Cut { get; set; }

        public decimal Score_0_0 { get; set; }
        public decimal Score_1_0 { get; set; }
        public decimal Score_2_0 { get; set; }
        public decimal Score_3_0 { get; set; }
        public decimal Score_4_0 { get; set; }
        public decimal Score_5_0 { get; set; }
        public decimal Score_6_0 { get; set; }
        public decimal Score_0_1 { get; set; }
        public decimal Score_0_2 { get; set; }
        public decimal Score_0_3 { get; set; }
        public decimal Score_0_4 { get; set; }
        public decimal Score_0_5 { get; set; }
        public decimal Score_0_6 { get; set; }
        public decimal Score_1_1 { get; set; }
        public decimal Score_2_1 { get; set; }
        public decimal Score_3_1 { get; set; }
        public decimal Score_4_1 { get; set; }
        public decimal Score_5_1 { get; set; }
        public decimal Score_1_2 { get; set; }
        public decimal Score_1_3 { get; set; }
        public decimal Score_1_4 { get; set; }
        public decimal Score_1_5 { get; set; }
        public decimal Score_2_2 { get; set; }
        public decimal Score_3_2 { get; set; }
        public decimal Score_4_2 { get; set; }
        public decimal Score_2_3 { get; set; }
        public decimal Score_2_4 { get; set; }
        public decimal Score_3_3 { get; set; }
        public decimal Score_Other { get; set; }

        public decimal MoreGoal_1st { get; set; }
        public decimal MoreGoal_Equal { get; set; }
        public decimal MoreGoal_2nd { get; set; }
        // TODO : DAVAM

        public decimal HT_Corners_4_5_Over { get; set; }
        public decimal HT_Corners_4_5_Under { get; set; }
        public decimal HT_Corners_8_5_Over { get; set; }
        public decimal HT_Corners_8_5_Under { get; set; }
        public decimal HT_Corners_9_5_Over { get; set; }
        public decimal HT_Corners_9_5_Under { get; set; }
        public decimal HT_Corners_10_5_Over { get; set; }
        public decimal HT_Corners_10_5_Under { get; set; }

        public decimal FT_MoreCorner_Home { get; set; }
        public decimal FT_MoreCorner_Equal { get; set; }
        public decimal FT_MoreCorner_Away { get; set; }

        public decimal HT_MoreCorner_Home { get; set; }
        public decimal HT_MoreCorner_Equal { get; set; }
        public decimal HT_MoreCorner_Away { get; set; }

        public decimal FirstCorner_Home { get; set; }
        public decimal FirstCorner_Never { get; set; }
        public decimal FirstCorner_Away { get; set; }

        public decimal FT_Corners_Range_0_8 { get; set; }
        public decimal FT_Corners_Range_9_11 { get; set; }
        public decimal FT_Corners_Range_12 { get; set; }

        public decimal HT_Corners_Range_0_4 { get; set; }
        public decimal HT_Corners_Range_5_6 { get; set; }
        public decimal HT_Corners_Range_7 { get; set; }

        public decimal Cards_3_5_Over { get; set; }
        public decimal Cards_3_5_Under { get; set; }
        public decimal Cards_4_5_Over { get; set; }
        public decimal Cards_4_5_Under { get; set; }
        public decimal Cards_5_5_Over { get; set; }
        public decimal Cards_5_5_Under { get; set; }

        public decimal HT_Win1 { get; set; }
        public decimal HT_Draw { get; set; }
        public decimal HT_Win2 { get; set; }

        public decimal SH_Win1 { get; set; }
        public decimal SH_Draw { get; set; }
        public decimal SH_Win2 { get; set; }

        public decimal HT_Double_1_X { get; set; }
        public decimal HT_Double_1_2 { get; set; }
        public decimal HT_Double_X_2 { get; set; }

        public decimal Home_1_5_Under { get; set; }
        public decimal Home_1_5_Over { get; set; }

        public decimal Away_1_5_Under { get; set; }
        public decimal Away_1_5_Over { get; set; }

        public decimal HT_1_5_Under { get; set; }
        public decimal HT_1_5_Over { get; set; }
        public decimal FT_1_5_Under { get; set; }
        public decimal FT_1_5_Over { get; set; }
        public decimal FT_2_5_Under { get; set; }
        public decimal FT_2_5_Over { get; set; }
        public decimal FT_3_5_Under { get; set; }
        public decimal FT_3_5_Over { get; set; }

        public decimal FT_GG { get; set; }
        public decimal FT_NG { get; set; }

        public decimal Goals01 { get; set; }
        public decimal Goals23 { get; set; }
        public decimal Goals45 { get; set; }
        public decimal Goals6 { get; set; }

    }
}

namespace Core.Entities.FilterData
{
    public class OddCheckBoxCheckerContainer
    {
        public bool FTWin1IsChecked { get; set; }
        public bool FTDrawIsChecked { get; set; }
        public bool FTWin2IsChecked { get; set; }
        public bool HTWin1IsChecked { get; set; }
        public bool HTDrawIsChecked { get; set; }
        public bool HTWin2IsChecked { get; set; }
        public bool FTUnder25IsChecked { get; set; }
        public bool FTOver25IsChecked { get; set; }
        public bool FTGGIsChecked { get; set; }
        public bool FTNGIsChecked { get; set; }
        public bool FT01IsChecked { get; set; }
        public bool FT23IsChecked { get; set; }
        public bool FT45IsChecked { get; set; }
        public bool FT6IsChecked { get; set; }

        public OddCheckBoxCheckerContainer()
        {
            FTWin1IsChecked = false;
            FTDrawIsChecked = false;
            FTWin2IsChecked = false;
            HTWin1IsChecked = false;
            HTDrawIsChecked = false;
            HTWin2IsChecked = false;
            FTUnder25IsChecked = false;
            FTOver25IsChecked = false;
            FTGGIsChecked = false;
            FTNGIsChecked = false;
            FT01IsChecked = false;
            FT23IsChecked = false;
            FT45IsChecked = false;
            FT6IsChecked = false;
        }
    }
}

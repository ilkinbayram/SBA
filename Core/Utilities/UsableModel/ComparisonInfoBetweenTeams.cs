namespace Core.Utilities.UsableModel
{
    public class ComparisonInfoBetweenTeams
    {
        public ComparisonInfoBetweenTeams()
        {
            BySide_MatchesCount = 0;

            BySide_HT_Goals_Count = 0;
            BySide_FT_Goals_Count = 0;
            BySide_SH_Goals_Count = 0;

            BySide_Home_HT_Goals_Count = 0;
            BySide_Home_FT_Goals_Count = 0;
            BySide_Home_SH_Goals_Count = 0;
            BySide_Away_SH_Goals_Count = 0;
            BySide_Away_HT_Goals_Count = 0;
            BySide_Away_FT_Goals_Count = 0;

            General_MatchesCount = 0;

            General_HT_Goals_Count = 0;
            General_FT_Goals_Count = 0;
            General_SH_Goals_Count = 0;

            General_Home_HT_Goals_Count = 0;
            General_Home_FT_Goals_Count = 0;
            General_Home_SH_Goals_Count = 0;
            General_Away_SH_Goals_Count = 0;
            General_Away_HT_Goals_Count = 0;
            General_Away_FT_Goals_Count = 0;

            Serial = string.Empty;
            Home = string.Empty;
            Away = string.Empty;
        }

        public string Serial { get; set; }
        public string Home { get; set; }
        public string Away { get; set; }

        public int BySide_MatchesCount { get; set; }
        public int General_MatchesCount { get; set; }
        public int BySide_HT_Goals_Count { get; set; }
        public int General_HT_Goals_Count { get; set; }
        public int BySide_SH_Goals_Count { get; set; }
        public int General_SH_Goals_Count { get; set; }

        public int BySide_FT_Goals_Count { get; set; }
        public int BySide_Home_HT_Goals_Count { get; set; }
        public int BySide_Home_FT_Goals_Count { get; set; }
        public int BySide_Home_SH_Goals_Count { get; set; }
        public int BySide_Away_SH_Goals_Count { get; set; }
        public int BySide_Away_HT_Goals_Count { get; set; }
        public int BySide_Away_FT_Goals_Count { get; set; }

        public int General_FT_Goals_Count { get; set; }
        public int General_Home_HT_Goals_Count { get; set; }
        public int General_Home_FT_Goals_Count { get; set; }
        public int General_Home_SH_Goals_Count { get; set; }
        public int General_Away_SH_Goals_Count { get; set; }
        public int General_Away_HT_Goals_Count { get; set; }
        public int General_Away_FT_Goals_Count { get; set; }
    }
}

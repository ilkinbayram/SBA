namespace Core.Resources.Constants
{
    public static class PatternConstant
    {
        public static class StartedMatchPattern
        {
            public const string Serial = @"canonical[\s\S]*?href[\s\S]*?arsiv\.mackolik\.com[\s\S]*?Mac[\/](\d\d\d\d\d\d\d)";

            public const string FT_Win1 = @">Maç Sonucu <[\s\S]*?>1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Draw = @">Maç Sonucu <[\s\S]*?>X<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Win2 = @">Maç Sonucu <[\s\S]*?>2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";


            public const string HT_Win1 = @">1. Yarı Sonucu <[\s\S]*?>1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_Draw = @">1. Yarı Sonucu <[\s\S]*?>X<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_Win2 = @">1. Yarı Sonucu <[\s\S]*?>2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";


            public const string HT_FT_Home_Home = @">İlk Yarı\/Maç Sonucu <[\s\S]*?>1\/1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_FT_Home_Draw = @">İlk Yarı\/Maç Sonucu <[\s\S]*?>1\/X<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_FT_Home_Away = @">İlk Yarı\/Maç Sonucu <[\s\S]*?>1\/2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_FT_Draw_Home = @">İlk Yarı\/Maç Sonucu <[\s\S]*?>X\/1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_FT_Draw_Draw = @">İlk Yarı\/Maç Sonucu <[\s\S]*?>X\/X<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_FT_Draw_Away = @">İlk Yarı\/Maç Sonucu <[\s\S]*?>X\/2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_FT_Away_Home = @">İlk Yarı\/Maç Sonucu <[\s\S]*?>2\/1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_FT_Away_Draw = @">İlk Yarı\/Maç Sonucu <[\s\S]*?>2\/X<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_FT_Away_Away = @">İlk Yarı\/Maç Sonucu <[\s\S]*?>2\/2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";


            public const string FT_Win1_Under_15 = @">Maç Sonucu ve \(1,5\) Alt\/Üst <[\s\S]*?>1 ve Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Draw_Under_15 = @">Maç Sonucu ve \(1,5\) Alt\/Üst <[\s\S]*?>X ve Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Win2_Under_15 = @">Maç Sonucu ve \(1,5\) Alt\/Üst <[\s\S]*?>2 ve Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Win1_Over_15 = @">Maç Sonucu ve \(1,5\) Alt\/Üst <[\s\S]*?>1 ve Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Draw_Over_15 = @">Maç Sonucu ve \(1,5\) Alt\/Üst <[\s\S]*?>X ve Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Win2_Over_15 = @">Maç Sonucu ve \(1,5\) Alt\/Üst <[\s\S]*?>2 ve Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string FT_Win1_Under_25 = @">Maç Sonucu ve \(2,5\) Alt\/Üst <[\s\S]*?>1 ve Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Draw_Under_25 = @">Maç Sonucu ve \(2,5\) Alt\/Üst <[\s\S]*?>X ve Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Win2_Under_25 = @">Maç Sonucu ve \(2,5\) Alt\/Üst <[\s\S]*?>2 ve Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Win1_Over_25 = @">Maç Sonucu ve \(2,5\) Alt\/Üst <[\s\S]*?>1 ve Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Draw_Over_25 = @">Maç Sonucu ve \(2,5\) Alt\/Üst <[\s\S]*?>X ve Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Win2_Over_25 = @">Maç Sonucu ve \(2,5\) Alt\/Üst <[\s\S]*?>2 ve Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string FT_Win1_Under_35 = @">Maç Sonucu ve \(3,5\) Alt\/Üst <[\s\S]*?>1 ve Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Draw_Under_35 = @">Maç Sonucu ve \(3,5\) Alt\/Üst <[\s\S]*?>X ve Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Win2_Under_35 = @">Maç Sonucu ve \(3,5\) Alt\/Üst <[\s\S]*?>2 ve Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Win1_Over_35 = @">Maç Sonucu ve \(3,5\) Alt\/Üst <[\s\S]*?>1 ve Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Draw_Over_35 = @">Maç Sonucu ve \(3,5\) Alt\/Üst <[\s\S]*?>X ve Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Win2_Over_35 = @">Maç Sonucu ve \(3,5\) Alt\/Üst <[\s\S]*?>2 ve Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string FT_Win1_Under_45 = @">Maç Sonucu ve \(4,5\) Alt\/Üst <[\s\S]*?>1 ve Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Draw_Under_45 = @">Maç Sonucu ve \(4,5\) Alt\/Üst <[\s\S]*?>X ve Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Win2_Under_45 = @">Maç Sonucu ve \(4,5\) Alt\/Üst <[\s\S]*?>2 ve Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Win1_Over_45 = @">Maç Sonucu ve \(4,5\) Alt\/Üst <[\s\S]*?>1 ve Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Draw_Over_45 = @">Maç Sonucu ve \(4,5\) Alt\/Üst <[\s\S]*?>X ve Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Win2_Over_45 = @">Maç Sonucu ve \(4,5\) Alt\/Üst <[\s\S]*?>2 ve Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string Handicap_04_Win1 = @">Handikaplı Maç Sonucu \(0:4\) <[\s\S]*?>1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_04_Draw = @">Handikaplı Maç Sonucu \(0:4\) <[\s\S]*?>X<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_04_Win2 = @">Handikaplı Maç Sonucu \(0:4\) <[\s\S]*?>2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_03_Win1 = @">Handikaplı Maç Sonucu \(0:3\) <[\s\S]*?>1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_03_Draw = @">Handikaplı Maç Sonucu \(0:3\) <[\s\S]*?>X<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_03_Win2 = @">Handikaplı Maç Sonucu \(0:3\) <[\s\S]*?>2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_02_Win1 = @">Handikaplı Maç Sonucu \(0:2\) <[\s\S]*?>1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_02_Draw = @">Handikaplı Maç Sonucu \(0:2\) <[\s\S]*?>X<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_02_Win2 = @">Handikaplı Maç Sonucu \(0:2\) <[\s\S]*?>2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_01_Win1 = @">Handikaplı Maç Sonucu \(0:1\) <[\s\S]*?>1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_01_Draw = @">Handikaplı Maç Sonucu \(0:1\) <[\s\S]*?>X<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_01_Win2 = @">Handikaplı Maç Sonucu \(0:1\) <[\s\S]*?>2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string Handicap_40_Win1 = @">Handikaplı Maç Sonucu \(4:0\) <[\s\S]*?>1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_40_Draw = @">Handikaplı Maç Sonucu \(4:0\) <[\s\S]*?>X<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_40_Win2 = @">Handikaplı Maç Sonucu \(4:0\) <[\s\S]*?>2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_30_Win1 = @">Handikaplı Maç Sonucu \(3:0\) <[\s\S]*?>1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_30_Draw = @">Handikaplı Maç Sonucu \(3:0\) <[\s\S]*?>X<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_30_Win2 = @">Handikaplı Maç Sonucu \(3:0\) <[\s\S]*?>2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_20_Win1 = @">Handikaplı Maç Sonucu \(2:0\) <[\s\S]*?>1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_20_Draw = @">Handikaplı Maç Sonucu \(2:0\) <[\s\S]*?>X<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_20_Win2 = @">Handikaplı Maç Sonucu \(2:0\) <[\s\S]*?>2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_10_Win1 = @">Handikaplı Maç Sonucu \(1:0\) <[\s\S]*?>1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_10_Draw = @">Handikaplı Maç Sonucu \(1:0\) <[\s\S]*?>X<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_10_Win2 = @">Handikaplı Maç Sonucu \(1:0\) <[\s\S]*?>2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string FT_Double_1_X = @">Çifte Şans <[\s\S]*?>1-X<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Double_1_2 = @">Çifte Şans <[\s\S]*?>1-2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Double_X_2 = @">Çifte Şans <[\s\S]*?>X-2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string FirstGoal_Home = @">İlk Gol <[\s\S]*?>1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FirstGoal_None = @">İlk Gol <[\s\S]*?>Olmaz<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FirstGoal_Away = @">İlk Gol <[\s\S]*?>2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string FT_4_5_Under = @">4,5 Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_4_5_Over = @">4,5 Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string FT_5_5_Under = @">5,5 Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_5_5_Over = @">5,5 Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string HT_0_5_Under = @">1. Yarı 0,5 Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_0_5_Over = @">1. Yarı 0,5 Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string HT_2_5_Under = @">1. Yarı 2,5 Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_2_5_Over = @">1. Yarı 2,5 Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string Home_2_5_Under = @">Evsahibi 2,5 Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Home_2_5_Over = @">Evsahibi 2,5 Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Home_3_5_Under = @">Evsahibi 3,5 Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Home_3_5_Over = @">Evsahibi 3,5 Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Home_4_5_Under = @">Evsahibi 4,5 Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Home_4_5_Over = @">Evsahibi 4,5 Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string Away_2_5_Under = @">Deplasman 2,5 Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Away_2_5_Over = @">Deplasman 2,5 Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Away_3_5_Under = @">Deplasman 3,5 Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Away_3_5_Over = @">Deplasman 3,5 Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Away_4_5_Under = @">Deplasman 4,5 Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Away_4_5_Over = @">Deplasman 4,5 Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string Even_Tek = @">Tek\/Çift <[\s\S]*?>Tek<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Odd_Cut = @">Tek\/Çift <[\s\S]*?>Çift<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string Score_0_0 = @">Maç Skoru <[\s\S]*?>0-0<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_1_0 = @">Maç Skoru <[\s\S]*?>1-0<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_2_0 = @">Maç Skoru <[\s\S]*?>2-0<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_3_0 = @">Maç Skoru <[\s\S]*?>3-0<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_4_0 = @">Maç Skoru <[\s\S]*?>4-0<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_5_0 = @">Maç Skoru <[\s\S]*?>5-0<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_6_0 = @">Maç Skoru <[\s\S]*?>6-0<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_0_1 = @">Maç Skoru <[\s\S]*?>0-1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_0_2 = @">Maç Skoru <[\s\S]*?>0-2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_0_3 = @">Maç Skoru <[\s\S]*?>0-3<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_0_4 = @">Maç Skoru <[\s\S]*?>0-4<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_0_5 = @">Maç Skoru <[\s\S]*?>0-5<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_0_6 = @">Maç Skoru <[\s\S]*?>0-6<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_1_1 = @">Maç Skoru <[\s\S]*?>1-1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_2_1 = @">Maç Skoru <[\s\S]*?>2-1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_3_1 = @">Maç Skoru <[\s\S]*?>3-1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_4_1 = @">Maç Skoru <[\s\S]*?>4-1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_5_1 = @">Maç Skoru <[\s\S]*?>5-1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_1_2 = @">Maç Skoru <[\s\S]*?>1-2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_1_3 = @">Maç Skoru <[\s\S]*?>1-3<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_1_4 = @">Maç Skoru <[\s\S]*?>1-4<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_1_5 = @">Maç Skoru <[\s\S]*?>1-5<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_2_2 = @">Maç Skoru <[\s\S]*?>2-2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_3_2 = @">Maç Skoru <[\s\S]*?>3-2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_4_2 = @">Maç Skoru <[\s\S]*?>4-2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_2_3 = @">Maç Skoru <[\s\S]*?>2-3<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_2_4 = @">Maç Skoru <[\s\S]*?>2-4<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_3_3 = @">Maç Skoru <[\s\S]*?>3-3<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_Other = @">Maç Skoru <[\s\S]*?>Diğer<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string MoreGoal_1st = @">Daha Çok Gol Olacak Yarı <[\s\S]*?>1.Y<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string MoreGoal_Equal = @">Daha Çok Gol Olacak Yarı <[\s\S]*?>Eşit<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string MoreGoal_2nd = @">Daha Çok Gol Olacak Yarı <[\s\S]*?>2.Y<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string HT_Corners_4_5_Over = @">1.Yarı \(4,5\) Korner Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_Corners_4_5_Under = @">1.Yarı \(4,5\) Korner Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string Corners_8_5_Over = @">\(8,5\) Korner Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Corners_8_5_Under = @">\(8,5\) Korner Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Corners_9_5_Over = @">\(9,5\) Korner Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Corners_9_5_Under = @">\(9,5\) Korner Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Corners_10_5_Over = @">\(10,5\) Korner Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Corners_10_5_Under = @">\(10,5\) Korner Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string FT_MoreCorner_Home = @">En Çok Korner <[\s\S]*?>1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_MoreCorner_Equal = @">En Çok Korner <[\s\S]*?>X<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_MoreCorner_Away = @">En Çok Korner <[\s\S]*?>2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";


            public const string HT_MoreCorner_Home = @">1. Yarı En Çok Korner <[\s\S]*?>1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_MoreCorner_Equal = @">1. Yarı En Çok Korner <[\s\S]*?>X<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_MoreCorner_Away = @">1. Yarı En Çok Korner <[\s\S]*?>2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string FirstCorner_Home = @">İlk Korner <[\s\S]*?>2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FirstCorner_Never = @">İlk Korner <[\s\S]*?>Olmaz<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FirstCorner_Away = @">İlk Korner <[\s\S]*?>2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string FT_Corners_Range_0_8 = @">Toplam Korner Aralığı <[\s\S]*?>0-8<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Corners_Range_9_11 = @">Toplam Korner Aralığı <[\s\S]*?>9-11<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Corners_Range_12 = @">Toplam Korner Aralığı <[\s\S]*?>12\+<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string HT_Corners_Range_0_4 = @">1. Yarı Korner Aralığı <[\s\S]*?>0-4<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_Corners_Range_5_6 = @">1. Yarı Korner Aralığı <[\s\S]*?>5-6<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_Corners_Range_7 = @">1. Yarı Korner Aralığı <[\s\S]*?>7\+<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string Cards_3_5_Over = @">\(3,5\) Kart Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Cards_3_5_Under = @">\(3,5\) Kart Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Cards_4_5_Over = @">\(4,5\) Kart Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Cards_4_5_Under = @">\(4,5\) Kart Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Cards_5_5_Over = @">\(5,5\) Kart Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Cards_5_5_Under = @">\(5,5\) Kart Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";



            public const string Home_1_5_Under = @">Evsahibi 1,5 Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Home_1_5_Over = @">Evsahibi 1,5 Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string Away_1_5_Under = @">Deplasman 1,5 Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Away_1_5_Over = @">Deplasman 1,5 Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";


            public const string SH_Win1 = @">2. Yarı Sonucu <[\s\S]*?>1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string SH_Draw = @">2. Yarı Sonucu <[\s\S]*?>X<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string SH_Win2 = @">2. Yarı Sonucu <[\s\S]*?>2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string HT_Double_1_X = @">1. Yarı Çifte Şans <[\s\S]*?>1-X<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_Double_1_2 = @">1. Yarı Çifte Şans <[\s\S]*?>1-2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_Double_X_2 = @">1. Yarı Çifte Şans <[\s\S]*?>X-2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";


            public const string HT_1_5_Under = @">1. Yarı 1,5 Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_1_5_Over = @">1. Yarı 1,5 Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string FT_1_5_Under = @">1,5 Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_1_5_Over = @">1,5 Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string FT_2_5_Under = @">2,5 Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_2_5_Over = @">2,5 Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string FT_3_5_Under = @">3,5 Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_3_5_Over = @">3,5 Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";



            public const string GG = @">Karşılıklı Gol <[\s\S]*?>Var<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string NG = @">Karşılıklı Gol <[\s\S]*?>Yok<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";


            public const string Goals01 = @">Toplam Gol Aralığı <[\s\S]*?>0-1 Gol<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Goals23 = @">Toplam Gol Aralığı <[\s\S]*?>2-3 Gol<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Goals45 = @">Toplam Gol Aralığı <[\s\S]*?>4-5 Gol<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Goals6 = @">Toplam Gol Aralığı <[\s\S]*?>6\+ Gol<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string FTResultMatch = @"match-info-wrapper[\s\S]*?match-info-wrapper-center[\s\S]*?match-score[\s\S]*?dvScoreText[\s\S]*?>[\s\S]*?(.+?(?=<\/div>))";

            public const string FTResultMatch2ndCheck = @"MS[\s\S]*?match-score[\s\S]*?dvScoreText[\s\S]*?>[\s\S]*?(.+?(?=<))";

            public const string HTResultMatch = @"dvScoreText[\s\S]*?mac-ht>[\s\S]*?;(\d\D*\d)";
            public const string HTResultMatch2ndCheck = @"MS[\s\S]*?mac-score[\s\S]*?dvStatusText[\s\S]*?mac-ht[\s\S]*?nbsp;(.+?(?=<))";


            public const string DateMatch = @"!DOCTYPE[\s\S]*?<head>[\s\S]*?<title>[\s\S]*?\([\s\S]*?(\d\d.\d\d.\d\d\d\d)";
            public const string DateMatch2nd = @"!DOCTYPE[\s\S]*?<head>[\s\S]*?title[\s\S]*?\([\s\S]*?(.+?(?=\)))";



            public const string HomeTeam = @"match-info-date[\s\S]*?match-detail-logo-temp[\s\S]*?left-block-team-name[\s\S]*?>[\s\S]*?(.+?(?=<))";
            public const string AwayTeam = @"match-info-date[\s\S]*?match-detail-logo-temp[\s\S]*?left-block-team-name[\s\S]*?r-match-detail-logo-temp[\s\S]*?r-left-block-team-name[\s\S]*?>[\s\S]*?(.+?(?=<))";

            public const string Country = @"dvStanding[\s\S]*?card[\s\S]*?standing-title[\s\S]*?Puan-Durumu[\s\S]*?title=[\s\S]*?(.+?(?= alt))";
            public const string League = @"dvStanding[\s\S]*?card[\s\S]*?standing-title[\s\S]*?Puan-Durumu[\s\S]*?title=[\s\S]*?vertical-align[\s\S]*?> [\s\S]*?(.+?(?= <\/a>))";

            public const string CountryAndLeague = @"<html[\s\S]*?<head>[\s\S]*?title[\s\S]*?>([^\W][^<]+)";
        }


        public static class UnstartedMatchPattern
        {
            public const string Serial = @"canonical[\s\S]*?href[\s\S]*?arsiv\.mackolik\.com[\s\S]*?Mac[\/](\d\d\d\d\d\d\d)";

            public const string FT_Win1 = @">Maç Sonucu <[\s\S]*?>1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Draw = @">Maç Sonucu <[\s\S]*?>X<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Win2 = @">Maç Sonucu <[\s\S]*?>2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string Time = @"match-info-date[\s\S]*?(\d\d:\d\d)";


            public const string HT_FT_Home_Home = @">İlk Yarı\/Maç Sonucu <[\s\S]*?>1\/1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_FT_Home_Draw = @">İlk Yarı\/Maç Sonucu <[\s\S]*?>1\/X<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_FT_Home_Away = @">İlk Yarı\/Maç Sonucu <[\s\S]*?>1\/2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_FT_Draw_Home = @">İlk Yarı\/Maç Sonucu <[\s\S]*?>X\/1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_FT_Draw_Draw = @">İlk Yarı\/Maç Sonucu <[\s\S]*?>X\/X<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_FT_Draw_Away = @">İlk Yarı\/Maç Sonucu <[\s\S]*?>X\/2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_FT_Away_Home = @">İlk Yarı\/Maç Sonucu <[\s\S]*?>2\/1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_FT_Away_Draw = @">İlk Yarı\/Maç Sonucu <[\s\S]*?>2\/X<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_FT_Away_Away = @">İlk Yarı\/Maç Sonucu <[\s\S]*?>2\/2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string FT_Win1_Under_15 = @">Maç Sonucu ve \(1,5\) Alt\/Üst <[\s\S]*?>1 ve Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Draw_Under_15 = @">Maç Sonucu ve \(1,5\) Alt\/Üst <[\s\S]*?>X ve Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Win2_Under_15 = @">Maç Sonucu ve \(1,5\) Alt\/Üst <[\s\S]*?>2 ve Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Win1_Over_15 = @">Maç Sonucu ve \(1,5\) Alt\/Üst <[\s\S]*?>1 ve Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Draw_Over_15 = @">Maç Sonucu ve \(1,5\) Alt\/Üst <[\s\S]*?>X ve Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Win2_Over_15 = @">Maç Sonucu ve \(1,5\) Alt\/Üst <[\s\S]*?>2 ve Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string FT_Win1_Under_25 = @">Maç Sonucu ve \(2,5\) Alt\/Üst <[\s\S]*?>1 ve Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Draw_Under_25 = @">Maç Sonucu ve \(2,5\) Alt\/Üst <[\s\S]*?>X ve Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Win2_Under_25 = @">Maç Sonucu ve \(2,5\) Alt\/Üst <[\s\S]*?>2 ve Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Win1_Over_25 = @">Maç Sonucu ve \(2,5\) Alt\/Üst <[\s\S]*?>1 ve Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Draw_Over_25 = @">Maç Sonucu ve \(2,5\) Alt\/Üst <[\s\S]*?>X ve Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Win2_Over_25 = @">Maç Sonucu ve \(2,5\) Alt\/Üst <[\s\S]*?>2 ve Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string FT_Win1_Under_35 = @">Maç Sonucu ve \(3,5\) Alt\/Üst <[\s\S]*?>1 ve Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Draw_Under_35 = @">Maç Sonucu ve \(3,5\) Alt\/Üst <[\s\S]*?>X ve Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Win2_Under_35 = @">Maç Sonucu ve \(3,5\) Alt\/Üst <[\s\S]*?>2 ve Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Win1_Over_35 = @">Maç Sonucu ve \(3,5\) Alt\/Üst <[\s\S]*?>1 ve Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Draw_Over_35 = @">Maç Sonucu ve \(3,5\) Alt\/Üst <[\s\S]*?>X ve Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Win2_Over_35 = @">Maç Sonucu ve \(3,5\) Alt\/Üst <[\s\S]*?>2 ve Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string FT_Win1_Under_45 = @">Maç Sonucu ve \(4,5\) Alt\/Üst <[\s\S]*?>1 ve Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Draw_Under_45 = @">Maç Sonucu ve \(4,5\) Alt\/Üst <[\s\S]*?>X ve Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Win2_Under_45 = @">Maç Sonucu ve \(4,5\) Alt\/Üst <[\s\S]*?>2 ve Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Win1_Over_45 = @">Maç Sonucu ve \(4,5\) Alt\/Üst <[\s\S]*?>1 ve Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Draw_Over_45 = @">Maç Sonucu ve \(4,5\) Alt\/Üst <[\s\S]*?>X ve Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Win2_Over_45 = @">Maç Sonucu ve \(4,5\) Alt\/Üst <[\s\S]*?>2 ve Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string Handicap_04_Win1 = @">Handikaplı Maç Sonucu \(0:4\) <[\s\S]*?>1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_04_Draw = @">Handikaplı Maç Sonucu \(0:4\) <[\s\S]*?>X<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_04_Win2 = @">Handikaplı Maç Sonucu \(0:4\) <[\s\S]*?>2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_03_Win1 = @">Handikaplı Maç Sonucu \(0:3\) <[\s\S]*?>1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_03_Draw = @">Handikaplı Maç Sonucu \(0:3\) <[\s\S]*?>X<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_03_Win2 = @">Handikaplı Maç Sonucu \(0:3\) <[\s\S]*?>2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_02_Win1 = @">Handikaplı Maç Sonucu \(0:2\) <[\s\S]*?>1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_02_Draw = @">Handikaplı Maç Sonucu \(0:2\) <[\s\S]*?>X<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_02_Win2 = @">Handikaplı Maç Sonucu \(0:2\) <[\s\S]*?>2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_01_Win1 = @">Handikaplı Maç Sonucu \(0:1\) <[\s\S]*?>1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_01_Draw = @">Handikaplı Maç Sonucu \(0:1\) <[\s\S]*?>X<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_01_Win2 = @">Handikaplı Maç Sonucu \(0:1\) <[\s\S]*?>2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string Handicap_40_Win1 = @">Handikaplı Maç Sonucu \(4:0\) <[\s\S]*?>1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_40_Draw = @">Handikaplı Maç Sonucu \(4:0\) <[\s\S]*?>X<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_40_Win2 = @">Handikaplı Maç Sonucu \(4:0\) <[\s\S]*?>2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_30_Win1 = @">Handikaplı Maç Sonucu \(3:0\) <[\s\S]*?>1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_30_Draw = @">Handikaplı Maç Sonucu \(3:0\) <[\s\S]*?>X<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_30_Win2 = @">Handikaplı Maç Sonucu \(3:0\) <[\s\S]*?>2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_20_Win1 = @">Handikaplı Maç Sonucu \(2:0\) <[\s\S]*?>1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_20_Draw = @">Handikaplı Maç Sonucu \(2:0\) <[\s\S]*?>X<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_20_Win2 = @">Handikaplı Maç Sonucu \(2:0\) <[\s\S]*?>2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_10_Win1 = @">Handikaplı Maç Sonucu \(1:0\) <[\s\S]*?>1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_10_Draw = @">Handikaplı Maç Sonucu \(1:0\) <[\s\S]*?>X<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Handicap_10_Win2 = @">Handikaplı Maç Sonucu \(1:0\) <[\s\S]*?>2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string FT_Double_1_X = @">Çifte Şans <[\s\S]*?>1-X<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Double_1_2 = @">Çifte Şans <[\s\S]*?>1-2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Double_X_2 = @">Çifte Şans <[\s\S]*?>X-2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string FirstGoal_Home = @">İlk Gol <[\s\S]*?>1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FirstGoal_None = @">İlk Gol <[\s\S]*?>Olmaz<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FirstGoal_Away = @">İlk Gol <[\s\S]*?>2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string FT_4_5_Under = @">4,5 Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_4_5_Over = @">4,5 Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string FT_5_5_Under = @">5,5 Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_5_5_Over = @">5,5 Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string HT_0_5_Under = @">1. Yarı 0,5 Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_0_5_Over = @">1. Yarı 0,5 Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string HT_2_5_Under = @">1. Yarı 2,5 Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_2_5_Over = @">1. Yarı 2,5 Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string Home_2_5_Under = @">Evsahibi 2,5 Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Home_2_5_Over = @">Evsahibi 2,5 Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Home_3_5_Under = @">Evsahibi 3,5 Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Home_3_5_Over = @">Evsahibi 3,5 Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Home_4_5_Under = @">Evsahibi 4,5 Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Home_4_5_Over = @">Evsahibi 4,5 Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string Away_2_5_Under = @">Deplasman 2,5 Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Away_2_5_Over = @">Deplasman 2,5 Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Away_3_5_Under = @">Deplasman 3,5 Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Away_3_5_Over = @">Deplasman 3,5 Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Away_4_5_Under = @">Deplasman 4,5 Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Away_4_5_Over = @">Deplasman 4,5 Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string Even_Tek = @">Tek\/Çift <[\s\S]*?>Tek<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Odd_Cut = @">Tek\/Çift <[\s\S]*?>Çift<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string Score_0_0 = @">Maç Skoru <[\s\S]*?>0-0<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_1_0 = @">Maç Skoru <[\s\S]*?>1-0<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_2_0 = @">Maç Skoru <[\s\S]*?>2-0<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_3_0 = @">Maç Skoru <[\s\S]*?>3-0<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_4_0 = @">Maç Skoru <[\s\S]*?>4-0<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_5_0 = @">Maç Skoru <[\s\S]*?>5-0<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_6_0 = @">Maç Skoru <[\s\S]*?>6-0<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_0_1 = @">Maç Skoru <[\s\S]*?>0-1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_0_2 = @">Maç Skoru <[\s\S]*?>0-2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_0_3 = @">Maç Skoru <[\s\S]*?>0-3<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_0_4 = @">Maç Skoru <[\s\S]*?>0-4<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_0_5 = @">Maç Skoru <[\s\S]*?>0-5<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_0_6 = @">Maç Skoru <[\s\S]*?>0-6<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_1_1 = @">Maç Skoru <[\s\S]*?>1-1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_2_1 = @">Maç Skoru <[\s\S]*?>2-1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_3_1 = @">Maç Skoru <[\s\S]*?>3-1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_4_1 = @">Maç Skoru <[\s\S]*?>4-1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_5_1 = @">Maç Skoru <[\s\S]*?>5-1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_1_2 = @">Maç Skoru <[\s\S]*?>1-2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_1_3 = @">Maç Skoru <[\s\S]*?>1-3<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_1_4 = @">Maç Skoru <[\s\S]*?>1-4<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_1_5 = @">Maç Skoru <[\s\S]*?>1-5<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_2_2 = @">Maç Skoru <[\s\S]*?>2-2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_3_2 = @">Maç Skoru <[\s\S]*?>3-2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_4_2 = @">Maç Skoru <[\s\S]*?>4-2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_2_3 = @">Maç Skoru <[\s\S]*?>2-3<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_2_4 = @">Maç Skoru <[\s\S]*?>2-4<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_3_3 = @">Maç Skoru <[\s\S]*?>3-3<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Score_Other = @">Maç Skoru <[\s\S]*?>Diğer<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string MoreGoal_1st = @">Daha Çok Gol Olacak Yarı <[\s\S]*?>1.Y<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string MoreGoal_Equal = @">Daha Çok Gol Olacak Yarı <[\s\S]*?>Eşit<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string MoreGoal_2nd = @">Daha Çok Gol Olacak Yarı <[\s\S]*?>2.Y<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string HT_Corners_4_5_Over = @">1.Yarı \(4,5\) Korner Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_Corners_4_5_Under = @">1.Yarı \(4,5\) Korner Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string Corners_8_5_Over = @">\(8,5\) Korner Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Corners_8_5_Under = @">\(8,5\) Korner Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Corners_9_5_Over = @">\(9,5\) Korner Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Corners_9_5_Under = @">\(9,5\) Korner Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Corners_10_5_Over = @">\(10,5\) Korner Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Corners_10_5_Under = @">\(10,5\) Korner Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string FT_MoreCorner_Home = @">En Çok Korner <[\s\S]*?>1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_MoreCorner_Equal = @">En Çok Korner <[\s\S]*?>X<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_MoreCorner_Away = @">En Çok Korner <[\s\S]*?>2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string HT_MoreCorner_Home = @">1. Yarı En Çok Korner <[\s\S]*?>1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_MoreCorner_Equal = @">1. Yarı En Çok Korner <[\s\S]*?>X<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_MoreCorner_Away = @">1. Yarı En Çok Korner <[\s\S]*?>2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string FirstCorner_Home = @">İlk Korner <[\s\S]*?>2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FirstCorner_Never = @">İlk Korner <[\s\S]*?>Olmaz<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FirstCorner_Away = @">İlk Korner <[\s\S]*?>2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string FT_Corners_Range_0_8 = @">Toplam Korner Aralığı <[\s\S]*?>0-8<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Corners_Range_9_11 = @">Toplam Korner Aralığı <[\s\S]*?>9-11<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_Corners_Range_12 = @">Toplam Korner Aralığı <[\s\S]*?>12\+<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string HT_Corners_Range_0_4 = @">1. Yarı Korner Aralığı <[\s\S]*?>0-4<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_Corners_Range_5_6 = @">1. Yarı Korner Aralığı <[\s\S]*?>5-6<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_Corners_Range_7 = @">1. Yarı Korner Aralığı <[\s\S]*?>7\+<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string Cards_3_5_Over = @">\(3,5\) Kart Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Cards_3_5_Under = @">\(3,5\) Kart Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Cards_4_5_Over = @">\(4,5\) Kart Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Cards_4_5_Under = @">\(4,5\) Kart Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Cards_5_5_Over = @">\(5,5\) Kart Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Cards_5_5_Under = @">\(5,5\) Kart Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string HT_Win1 = @">1. Yarı Sonucu <[\s\S]*?>1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_Draw = @">1. Yarı Sonucu <[\s\S]*?>X<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_Win2 = @">1. Yarı Sonucu <[\s\S]*?>2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string Home_1_5_Under = @">Evsahibi 1,5 Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Home_1_5_Over = @">Evsahibi 1,5 Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string Away_1_5_Under = @">Deplasman 1,5 Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Away_1_5_Over = @">Deplasman 1,5 Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";


            public const string SH_Win1 = @">2. Yarı Sonucu <[\s\S]*?>1<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string SH_Draw = @">2. Yarı Sonucu <[\s\S]*?>X<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string SH_Win2 = @">2. Yarı Sonucu <[\s\S]*?>2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string HT_Double_1_X = @">1. Yarı Çifte Şans <[\s\S]*?>1-X<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_Double_1_2 = @">1. Yarı Çifte Şans <[\s\S]*?>1-2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_Double_X_2 = @">1. Yarı Çifte Şans <[\s\S]*?>X-2<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string HT_1_5_Under = @">1. Yarı 1,5 Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string HT_1_5_Over = @">1. Yarı 1,5 Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string FT_1_5_Under = @">1,5 Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_1_5_Over = @">1,5 Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string FT_2_5_Under = @">2,5 Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_2_5_Over = @">2,5 Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string FT_3_5_Under = @">3,5 Alt\/Üst <[\s\S]*?>Alt<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string FT_3_5_Over = @">3,5 Alt\/Üst <[\s\S]*?>Üst<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string GG = @">Karşılıklı Gol <[\s\S]*?>Var<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string NG = @">Karşılıklı Gol <[\s\S]*?>Yok<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";


            public const string Goals01 = @">Toplam Gol Aralığı <[\s\S]*?>0-1 Gol<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Goals23 = @">Toplam Gol Aralığı <[\s\S]*?>2-3 Gol<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Goals45 = @">Toplam Gol Aralığı <[\s\S]*?>4-5 Gol<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";
            public const string Goals6 = @">Toplam Gol Aralığı <[\s\S]*?>6\+ Gol<[\s\S]*?compare-rate-bg-up[\s\S]*?[\>](-|\d\d*\.\d\d|\d\d*)";

            public const string DateMatch = @"!DOCTYPE[\s\S]*?<head>[\s\S]*?<title>[\s\S]*?\([\s\S]*?(\d\d.\d\d.\d\d\d\d)";
            public const string DateMatch2nd = @"!DOCTYPE[\s\S]*?<head>[\s\S]*?title[\s\S]*?\([\s\S]*?(.+?(?=\)))";

            public const string HomeTeam = @"match-info-wrapper[\s\S]*?match-info-wrapper-center[\s\S]*?left-block-team-name[\s\S]*?>([^\W][^<]+)";
            public const string AwayTeam = @"match-info-wrapper[\s\S]*?match-info-wrapper-center[\s\S]*?r-left-block-team-name[\s\S]*?>([^\W][^<]+)";

            public const string Country = @"dvStanding[\s\S]*?card[\s\S]*?standing-title[\s\S]*?Puan-Durumu[\s\S]*?title=[\s\S]*?(.+?(?= alt))";
            public const string League = @"dvStanding[\s\S]*?card[\s\S]*?standing-title[\s\S]*?Puan-Durumu[\s\S]*?title=[\s\S]*?vertical-align[\s\S]*?> [\s\S]*?(.+?(?= <\/a>))";
            public const string CountryAndLeague = @"<html[\s\S]*?<head>[\s\S]*?title[\s\S]*?>([^\W][^<]+)";
        }


        public static class ComparisonInfoPattern
        {
            public const string HomeTeam = @"MS[\s\S]*?target=_blank[\s\S]*?>([^\W][^<]+)";
            public const string AwayTeam = @"MS[\s\S]*?target=_blank[\s\S]*?target=_blank[\s\S]*?target=_blank[\s\S]*?>([^\W][^<]+)";

            public const string HT_Result = @"MS[\s\S]*?target=_blank[\s\S]*?target=_blank[\s\S]*?><b>[\s\S]*?align=center[\s\S]*?>(.+?(?=<))";
            public const string FT_Result = @"MS[\s\S]*?target=_blank[\s\S]*?target=_blank[\s\S]*?><b>[\s\S]*?(.+?(?=<\/b>))";

            public const string TeamsNames = @"page-title-out[\s\S]*?page-title[\s\S]*?>([^\W][^<]+)";

            public const string CountryName = @"<td>[\s\S]*?<td[\s\S]*?title=[\s\S]*?([^\W][^>]+)";

            public const string MatchDate = @"center[\s\S]*?>([^\W][^<]+)";

            public const string Season = @"<td>([^\W][^<]+)";
        }

        public static class StandingInfoPattern
        {
            public static class UpTeam
            {
                public const string Team = @"dvStanding[\s\S]*?tblStanding[\s\S]*?alt[\s\S]*?fff3a5[\s\S]*?bold>[\s\S]*?(.+?(?=<))";
                public const string Order = @"dvStanding[\s\S]*?tblStanding[\s\S]*?alt[\s\S]*?fff3a5[\s\S]*?<b>[\s\S]*?(.+?(?=<))";
                public const string PlayedMatchesCount = @"dvStanding[\s\S]*?tblStanding[\s\S]*?alt[\s\S]*?fff3a5[\s\S]*?bold>[\s\S]*?right>[\s\S]*?(.+?(?=<))";
                public const string WinsCount = @"dvStanding[\s\S]*?tblStanding[\s\S]*?alt[\s\S]*?fff3a5[\s\S]*?bold>[\s\S]*?right>[\s\S]*?right>[\s\S]*?(.+?(?=<))";
                public const string DrawsCount = @"dvStanding[\s\S]*?tblStanding[\s\S]*?alt[\s\S]*?fff3a5[\s\S]*?bold>[\s\S]*?right>[\s\S]*?right>[\s\S]*?right>[\s\S]*?(.+?(?=<))";
                public const string LostsCount = @"dvStanding[\s\S]*?tblStanding[\s\S]*?alt[\s\S]*?fff3a5[\s\S]*?bold>[\s\S]*?right>[\s\S]*?right>[\s\S]*?right>[\s\S]*?right>[\s\S]*?(.+?(?=<))";
                public const string Ponints = @"dvStanding[\s\S]*?tblStanding[\s\S]*?alt[\s\S]*?fff3a5[\s\S]*?bold>[\s\S]*?right>[\s\S]*?right>[\s\S]*?right>[\s\S]*?right>[\s\S]*?right>[\s\S]*?<b>[\s\S]*?(.+?(?=<))";
            }

            public static class DownTeam
            {
                public const string Team = @"dvStanding[\s\S]*?tblStanding[\s\S]*?alt[\s\S]*?fff3a5[\s\S]*?bold>[\s\S]*?fff3a5[\s\S]*?bold>[\s\S]*?(.+?(?=<))";
                public const string Order = @"dvStanding[\s\S]*?tblStanding[\s\S]*?alt[\s\S]*?fff3a5[\s\S]*?bold>[\s\S]*?fff3a5[\s\S]*?<b>[\s\S]*?(.+?(?=<))";
                public const string PlayedMatchesCount = @"dvStanding[\s\S]*?tblStanding[\s\S]*?alt[\s\S]*?fff3a5[\s\S]*?bold>[\s\S]*?fff3a5[\s\S]*?bold>[\s\S]*?right>[\s\S]*?(.+?(?=<))";
                public const string WinsCount = @"dvStanding[\s\S]*?tblStanding[\s\S]*?alt[\s\S]*?fff3a5[\s\S]*?bold>[\s\S]*?fff3a5[\s\S]*?bold>[\s\S]*?right>[\s\S]*?right>[\s\S]*?(.+?(?=<))";
                public const string DrawsCount = @"dvStanding[\s\S]*?tblStanding[\s\S]*?alt[\s\S]*?fff3a5[\s\S]*?bold>[\s\S]*?fff3a5[\s\S]*?bold>[\s\S]*?right>[\s\S]*?right>[\s\S]*?right>[\s\S]*?(.+?(?=<))";
                public const string LostsCount = @"dvStanding[\s\S]*?tblStanding[\s\S]*?alt[\s\S]*?fff3a5[\s\S]*?bold>[\s\S]*?fff3a5[\s\S]*?bold>[\s\S]*?right>[\s\S]*?right>[\s\S]*?right>[\s\S]*?right>[\s\S]*?(.+?(?=<))";
                public const string Ponints = @"dvStanding[\s\S]*?tblStanding[\s\S]*?alt[\s\S]*?fff3a5[\s\S]*?bold>[\s\S]*?fff3a5[\s\S]*?bold>[\s\S]*?right>[\s\S]*?right>[\s\S]*?right>[\s\S]*?right>[\s\S]*?right>[\s\S]*?<b>[\s\S]*?(.+?(?=<))";
            }
        }


        public static class RegSrcMix
        {
            public const string ScoreFromPerformance = @"\b(0|[1-9]\d*)-(0|[1-9]\d*)\b";
            public const string TeamsCollector = @"title=""[\s\S]*?([^\W][^\(]+)";
        }
    }
}

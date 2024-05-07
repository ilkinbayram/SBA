using DataAccess.Concrete.EntityFramework.Contexts;
using SBA.Business.FunctionalServices.Concrete;
using System.Diagnostics;
using System.Text.RegularExpressions;




//var _httpClient = new HttpClient();
//var trClient = new GoogleTranslationService("AIzaSyCUgz3xO-A0UoK-yaVBZ3dIzjZeBBLuHS0");

//while (true)
//{
//    Console.Write("Would you like continue ? ___");

//    var res = Console.ReadLine();

//    if (res.ToLower() == "yes")
//    {
//        Console.Write("Text: ");

//        var text = Console.ReadLine();

//        string result = trClient.Translate(text, "tr", "az");

//        Console.WriteLine(result);
//    }


//}









//var hdn = new HelperBerro();
//Console.WriteLine(hdn.GetResult());

//var messages = new Messages.AdvisorGuessMessages();

//for (int i = 0; i < 10; i++)
//{
//    Console.WriteLine(messages.Message);
//}


//WebOperation webOperation = new WebOperation();
//var src = webOperation.GetMinifiedString($"http://arsiv.mackolik.com/Match/Default.aspx?id=3420065#mac-bilgisi");

//string stsNamePoss = "Topla Oynama";

//string stsNameAllShot = "Toplam Şut";

//string stsNameShotTrg = "İsabetli Şut";

//string stsNameKrn1 = "Korner";
//string stsNameKrn2 = "Köşe Vuruşu";


//var statPoss = ExtractFromStatistics(src, stsNamePoss);
//var statKorner = ExtractFromStatistics(src, "sdfgfg", stsNameKrn2, stsNameKrn1);
//var statAllShot = ExtractFromStatistics(src, stsNameAllShot);
//var statShotTrg = ExtractFromStatistics(src, stsNameShotTrg);

//Console.WriteLine($"{statPoss[0]}% <= Topla Oynama => {statPoss[1]}%");
//Console.WriteLine($"{statAllShot[0]} <= Toplam Şut => {statAllShot[1]}");
//Console.WriteLine($"{statShotTrg[0]} <= İsabetli Şut => {statShotTrg[1]}");
//Console.WriteLine($"{statKorner[0]} <= Korner => {statKorner[1]}");

Console.ReadLine();



//3784568


int[] ExtractFromStatistics(string src, params string[] statisticNames)
{
    int[] result = new int[2];
    result[0] = -1;
    result[1] = -1;

    for (int i = 0; i < statisticNames.Length; i++)
    {
        var statisticName = statisticNames[i];

        var regexStat = new Regex(">" + statisticName + "<[\\s\\S]*?class=team-2-statistics-text[\\s\\S]*?>[\\s\\S]*?(.+?(?=<))");

        var firstSrcPart = src.Split($">{statisticName}<")[0];

        var belongTeam1 = "";
        var belongTeam2 = "";

        try
        {
            belongTeam1 = firstSrcPart.Substring(firstSrcPart.Length - 40, 1).Trim().Trim('%');
            if (belongTeam1.Trim() == ">")
            {
                belongTeam1 = firstSrcPart.Substring(firstSrcPart.Length - 39, 1).Trim().Trim('%');
            }
            else
            {
                belongTeam1 = firstSrcPart.Substring(firstSrcPart.Length - 40, 2).Trim().Trim('%');
            }
            belongTeam2 = regexStat.Matches(src)[0].Groups[1].Value.Trim().Trim('%');

            result[0] = Convert.ToInt32(belongTeam1);
            result[1] = Convert.ToInt32(belongTeam2);

            break;
        }
        catch (Exception ex)
        {
            continue;
        }
    }

    return result;
}

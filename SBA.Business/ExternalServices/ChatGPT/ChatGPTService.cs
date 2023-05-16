using SBA.Business.ExternalServices.ChatGPT.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SBA.Business.ExternalServices.ChatGPT
{
    public class ChatGPTService
    {
        private readonly string _apiKey;

        public ChatGPTService(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<string> CallOpenAIAsync(string input, string homeTeam, string awayTeam)
        {
            string apiUrl = $"https://api.openai.com/v1/chat/completions";

            RequestGPT request = new RequestGPT();
            request.Messages = new RequestMessageGPT[]
            {
                new RequestMessageGPT()
                {
                     Role = "system",
                     Content = "Lütfen Türkçe dilini kullan ve soracağım soruya Türkçe cevap ver. Sana sundugum tüm istatistiklere dayanarak Maç Analizi yap ve bu maç ile ilgili tam garantili bahis tahmininde bulun. Tahminin istatistiklere dayali olarak ayni zamanda bir-biriyle tutarlı olmak zorunda! Verilen tüm istatistikleri dikkatlice analiz et ve bu istatistiklere göre, şu maç için en olası (garanti) tahmin nedir? Yapacagin tahmine gore yüksek miktarda bahis yapacagim o yuzden dikkatli ol!"
                },
                new RequestMessageGPT()
                {
                     Role = "user",
                     Content = $"Verilerle ilgili açıklama:\n* LeagueStatistics - Bu Ligde olan tüm takımların karşılaşmalarından toplanan verilerden çıkarılan istatistiktir.\n* General_H2H - {homeTeam} ve {awayTeam} takımları arasında bir-biriyle oynanan tüm maçlardan toplanan verilere dayanan istatistikleri.\n* HomeAtHome_AwayAtAway_H2H - {homeTeam} ve {awayTeam} takımları arasında bir-biriyle Ev sahibinin sadece evdeç Deplasmanın sadece deplasmanda oynadığı maçlardan toplanan verilere dayalı istatistikler.\n* General_Form_HomeTeam - {homeTeam}'in oynadığı son 10 maçın verilerinden oluşan istatistikler.\n* General_Form_AwayTeam - {awayTeam}'in oynadığı son 10 maçın verilerinden oluşan istatistikler.\n * HomeAtHome_Form_HomeTeam - {homeTeam}'in sadece evinde oynadığı son 6 maçın verilerinden oluşan istatistikler.\n * AwayAtAway_Form_AwayTeam - {awayTeam}'in sadece deplasmanda oynadığı son 6 maçın verilerinden oluşan istatistikler."
                },
                new RequestMessageGPT()
                {
                     Role = "user",
                     Content = input
                }
            };

            string requestData = JsonSerializer.Serialize(request);
            StringContent content = new StringContent(requestData, Encoding.UTF8, "application/json");

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
                    HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(apiUrl, content);

                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        string responseString = await httpResponseMessage.Content.ReadAsStringAsync();
                        ResponseGPT response = JsonSerializer.Deserialize<ResponseGPT>(responseString);
                        return response.Choices[0].Message.Content;
                    }
                    else
                    {
                        return $"Error: {httpResponseMessage.StatusCode} - {httpResponseMessage.ReasonPhrase}";
                    }
                }
            }
            catch (Exception ex)
            {
                return "INTERNAL SERVER ERROR!!!";
            }
        }

        public async Task<string> CheckForecastAsync(string statisticModel, string prediction)
        {
            string apiUrl = $"https://api.openai.com/v1/chat/completions";

            RequestGPT request = new RequestGPT();
            request.Messages = new RequestMessageGPT[]
            {
                new RequestMessageGPT()
                {
                     Role = "system",
                     Content = "Bu istatistikleri analiz et. Bu istatistiklere dayanarak tüm verilere göre ben sana kendi tahminimi gönderiyorum. Eğer benim bahis tahminimle tamamen razı isen o zaman sadece 'TRUE200' cevabı döndür? Eğer bahis tahminimin bu istatistiklere göre riskli ve gerçekleşme olasılığı az olan bir ihtimal olarak değerlendiriyorsan sadece 'FALSE400' cevabı döndür."
                },
                new RequestMessageGPT()
                {
                     Role = "user",
                     Content = statisticModel
                },
                new RequestMessageGPT()
                {
                     Role = "user",
                     Content = $"Benim BAHİS TAHMİNİM => {prediction}"
                }
            };

            string requestData = JsonSerializer.Serialize(request);
            StringContent content = new StringContent(requestData, Encoding.UTF8, "application/json");

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
                    HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(apiUrl, content);

                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        string responseString = await httpResponseMessage.Content.ReadAsStringAsync();
                        ResponseGPT response = JsonSerializer.Deserialize<ResponseGPT>(responseString);
                        return response.Choices[0].Message.Content;
                    }
                    else
                    {
                        return $"Error: {httpResponseMessage.StatusCode} - {httpResponseMessage.ReasonPhrase}";
                    }
                }
            }
            catch (Exception ex)
            {
                return "INTERNAL SERVER ERROR!!!";
            }
        }

        public async Task<string> CheckForecastAsync(string matchDataes, string leagueStatistics, string standingInfo, string h2hMatchDataes, string lastMatchesHome, string lastMatchesAway, string statisticsPercentage, string prediction)
        {
            string apiUrl = $"https://api.openai.com/v1/chat/completions";

            RequestGPT request = new RequestGPT();
            request.Messages = new RequestMessageGPT[]
            {
        new RequestMessageGPT()
        {
            Role = "system",
            Content = $"Hello ChatGPT! I am sending you a set of statistical dataes along with my own prediction. The set of the statistical dataes contains MATCH-DATAES, LEAGUE-STATISTICS, STANDING-INFO, H2H-10-MATCHES-DETAILS, LAST-10-MATCHES-DETAILS-OF-HOME-TEAM, LAST-10-MATCHES-DETAILS-OF-AWAY-TEAM and STATISTICS-WITH-PERCENTAGE. Please analyze the all the given information then compare it with my prediction. If you fully agree with my prediction and consider it as a well-evaluated and accurate forecast, please respond with 'TRUE200'. If you do not agree with my prediction or you do not believe it is a reliable forecast, please respond with 'FALSE400'."
        },
        new RequestMessageGPT()
        {
            Role = "user",
            Content = $"Here is the MATCH-DATAES:\n{matchDataes}"
        },
        new RequestMessageGPT()
        {
            Role = "user",
            Content = $"Here is the LEAGUE-STATISTICS:\n{leagueStatistics}"
        },
        new RequestMessageGPT()
        {
            Role = "user",
            Content = $"Here is the STANDING-INFO:\n{standingInfo}"
        },
        new RequestMessageGPT()
        {
            Role = "user",
            Content = $"Here is the H2H-10-MATCHES-DETAILS:\n{h2hMatchDataes}"
        },
        new RequestMessageGPT()
        {
            Role = "user",
            Content = $"Here is the LAST-10-MATCHES-DETAILS-OF-HOME-TEAM:\n{lastMatchesHome}"
        },
        new RequestMessageGPT()
        {
            Role = "user",
            Content = $"Here is the LAST-10-MATCHES-DETAILS-OF-AWAY-TEAM:\n{lastMatchesAway}"
        },
        new RequestMessageGPT()
        {
            Role = "user",
            Content = $"Here is the STATISTICS-WITH-PERCENTAGE:\n{statisticsPercentage}"
        },
        new RequestMessageGPT()
        {
            Role = "user",
            Content = $"And here is my prediction: {prediction}"
        }
            };

            string requestData = JsonSerializer.Serialize(request);
            StringContent content = new StringContent(requestData, Encoding.UTF8, "application/json");

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
                    HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(apiUrl, content);

                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        string responseString = await httpResponseMessage.Content.ReadAsStringAsync();
                        ResponseGPT response = JsonSerializer.Deserialize<ResponseGPT>(responseString);
                        return response.Choices[0].Message.Content;
                    }
                    else
                    {
                        return $"Error: {httpResponseMessage.StatusCode} - {httpResponseMessage.ReasonPhrase}";
                    }
                }
            }
            catch (Exception ex)
            {
                return "INTERNAL SERVER ERROR!!!";
            }
        }

        private string ConvertToContent(string statistics, string prediction)
        {
            return $"\n* Here is the STATISTICS-INFO: {statistics}\n\n* Here is MY-PREDICTION: {prediction}";
        }
    }
}

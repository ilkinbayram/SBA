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

        public async Task<string> CallOpenAIAsync(string input)
        {
            string apiUrl = $"https://api.openai.com/v1/chat/completions";

            RequestGPT request = new RequestGPT();
            request.Messages = new RequestMessageGPT[]
            {
                new RequestMessageGPT()
                {
                     Role = "system",
                     Content = "Lütfen Türkçe dilini kullan ve soracağım soruya Türkçe dilinde ve tüm tahminlerini maksimum kısa şekilde cevaplamaya özen göster. Tahminin istatistiklere dayali olarak ayni zamanda bir-biriyle tutarlı olmak zorunda! Verilen tüm istatistikleri dikkatlice analiz et ve bu istatistiklere göre, şu maç için en olası (garanti) tahmin nedir? Yapacagin tahmine gore bahis yapacagim o yuzden dikkatli ol!"
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
                     Content = "Bu istatistikler (STATISTICS-INFO) analiz et. Bu istatistiklere dayanarak bu maçla ilgili Garanti bir tahminde bulun. Sence Macta en az ve en fazla kac tane gol olur? Tahminini sadece rakam olarak ve koseli parantezler icinde yaz. Ornek cevap: [|5|10|]"
                },
                new RequestMessageGPT()
                {
                     Role = "user",
                     Content = ConvertToContent(statisticModel, "GARANTİ BAHİS TAHMİNİM => Maçta en az 14 tane gol olacaktır.")
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

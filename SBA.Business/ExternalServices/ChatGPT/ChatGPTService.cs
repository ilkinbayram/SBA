using Core.Utilities.Results;
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
                     Content = "Lütfen Türkçe dilini kullan ve soracağım soruya Türkçe cevap ver. Verilen istatistiklere göre, şu maç için daha olası (garanti) tahminler yapın. Dikkate alınması gereken tahminler: 1.5 üst, Ev sahibi takım herhangi bir yarıyı kazanır, Konuk takım herhangi bir yarıyı kazanır, Ev sahibi takım 1 gol atar, Konuk takım 1 gol atar, 3.5 alt gol olur vb."
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
    }
}

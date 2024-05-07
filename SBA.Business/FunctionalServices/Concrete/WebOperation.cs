using WebMarkupMin.Core;

namespace SBA.Business.FunctionalServices.Concrete
{
    public class WebOperation
    {
        private readonly HttpClient _client;
        private readonly HtmlMinifier _minifier;

        public WebOperation()
        {
            _client = _client ?? new HttpClient();
            _minifier = _minifier ?? new HtmlMinifier();
        }

        public async Task<string> GetMinifiedStringAsync(string url)
        {
            try
            {
                await Task.Delay(300);
                var webSrc = await _client.GetStringAsync(url);
                var result = _minifier.Minify(webSrc);

                if (result.Errors.Count > 0)
                { 
                    throw new System.Exception(); 
                }

                return result.MinifiedContent;
            }
            catch (System.Exception)
            {
                return string.Empty;
            }
        }

        public string GetMinifiedString(string url)
        {
            try
            {
                var webSrc = _client.GetStringAsync(url).Result;
                Thread.Sleep(200);
                var result = _minifier.Minify(webSrc);

                if (result.Errors.Count > 0)
                {
                    throw new System.Exception();
                }

                return result.MinifiedContent;
            }
            catch (System.Exception)
            {
                return string.Empty;
            }
        }
    }
}

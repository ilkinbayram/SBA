using Core.Extensions;
using Core.Utilities.UsableModel;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SBA.WebAPI.Controllers
{
    public class BaseWebApiController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public BaseWebApiController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<string> Countries
        {
            get
            {
                string jsonFormat = _configuration.GetSection("PathConstant").GetValue<string>("JsonPathFormat");

                List<CountryOrdership> countries = new List<CountryOrdership>();

                using (var srCacheReader = new StreamReader(jsonFormat.GetJsonFileByFormat("countriesForApiProj")))
                {
                    string content = srCacheReader.ReadToEnd();
                    countries = JsonConvert.DeserializeObject<List<CountryOrdership>>(content);
                }

                // Eğer cache dosyası boşsa veya yoksa veritabanından bilgi çek
                if (countries == null || countries.Count == 0)
                {
                    using (var context = new ApplicationDbContext())
                    {
                        countries = context.MatchBets
                                           .Select(x => x.Country)
                                           .Distinct()
                                           .Select(country => new CountryOrdership
                                           {
                                               Country = country,
                                               Count = country.Length
                                           })
                                           .OrderByDescending(x => x.Count)
                                           .ThenBy(x => x.Country)
                                           .ToList();

                        // Yeni listeyi cache olarak kaydet
                        using (var wrCache = new StreamWriter(jsonFormat.GetJsonFileByFormat("countriesForApiProj")))
                        {
                            wrCache.Write(JsonConvert.SerializeObject(countries));
                        }
                    }
                }

                // Sadece ülke isimlerini döndür
                return countries.Select(x => x.Country).ToList();
            }
        }
    }
}

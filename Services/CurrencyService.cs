using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace EkzamenADO.Services
{
    public class CurrencyService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<decimal> GetUsdToUahRateAsync()
        {
            string url = "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?valcode=USD&json";
            string json = await _httpClient.GetStringAsync(url);
            var doc = JsonDocument.Parse(json);
            var element = doc.RootElement[0];
            return element.GetProperty("rate").GetDecimal();
        }
    }
}

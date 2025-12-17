using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherApp.Models; 

namespace WeatherApp.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;

        // Constructor receives HttpClient from dependency injection
        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GetWeatherAsync method goes here
        public async Task<Weather> GetWeatherAsync(string city)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid=c2b45bd83132818cab2744dd6fcd7916&units=metric";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            return new Weather
            {
                City = root.GetProperty("name").GetString(),
                Description = root.GetProperty("weather")[0].GetProperty("description").GetString(),
                Temperature = root.GetProperty("main").GetProperty("temp").GetDouble(),
                FeelsLike = root.GetProperty("main").GetProperty("feels_like").GetDouble(),
                Humidity = root.GetProperty("main").GetProperty("humidity").GetInt32()
            };
        }
    }
}

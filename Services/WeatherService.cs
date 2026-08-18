<<<<<<< HEAD
﻿using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherApp.Models; 
=======
﻿using System.Text.Json;
using WeatherApp.Models;
>>>>>>> af434ee (Updated Project)

namespace WeatherApp.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public WeatherService(
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

<<<<<<< HEAD
        // GetWeatherAsync method goes here
        public async Task<Weather> GetWeatherAsync(string city)
=======
        public async Task<Weather?> GetWeatherAsync(string city)
>>>>>>> af434ee (Updated Project)
        {
            var apiKey = _configuration["WeatherApiKey"];

            if (string.IsNullOrEmpty(apiKey))
                return null;

            var url =
                $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            return new Weather
            {
                City = root.GetProperty("name").GetString(),
                Description = root.GetProperty("weather")[0]
                    .GetProperty("description")
                    .GetString(),

                Temperature = root.GetProperty("main")
                    .GetProperty("temp")
                    .GetDouble(),

                FeelsLike = root.GetProperty("main")
                    .GetProperty("feels_like")
                    .GetDouble(),

                Humidity = root.GetProperty("main")
                    .GetProperty("humidity")
                    .GetInt32()
            };
        }
    }
}
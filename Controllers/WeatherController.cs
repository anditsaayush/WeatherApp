using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models;
using WeatherApp.Services;
using System.Threading.Tasks;

namespace WeatherApp.Controllers
{
    public class WeatherController : Controller
    {
        private readonly WeatherService _weatherService;

        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(); // Shows empty form initially
        }

        [HttpPost]
        public async Task<IActionResult> Index(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                ViewBag.Error = "Please enter a city name";
                return View();
            }

            var weather = await _weatherService.GetWeatherAsync(city);

            if (weather == null)
            {
                ViewBag.Error = "Weather not found or invalid city";
                return View();
            }

            return View(weather); // Passes the weather model to the view
        }
    }
}

namespace WeatherApp.Models
{
    public class Weather
    {
        public string City { get; set; }
        public string Description { get; set; }
        public double Temperature { get; set; }
        public double FeelsLike { get; set; }
        public int Humidity { get; set; }
    }
}

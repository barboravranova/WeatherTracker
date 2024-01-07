using SharedLibrary.JsonObjects.FileContent;
using SharedLibrary.JsonObjects.WeatherInfo;
using System.Text;
using System.Text.Json;

namespace WeatherTrackerLogging
{
    internal class OpenWeatherAPI
    {
        private const string apiKey = "d69bc0ec890b337da5c489a93f21884d";
        private const string units = "metric";

        public WeatherInfo GetWheatherInfo(string lat, string lon)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.openweathermap.org/");

            var json = JsonSerializer.Serialize(client.DefaultRequestHeaders);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = client.PostAsync($"data/2.5/weather?lat={lat}&lon={lon}&units={units}&appid={apiKey}", content).Result;

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var responseContent = response.Content.ReadAsStringAsync().Result;
                var weatherObject = JsonSerializer.Deserialize<WeatherInfo>(responseContent, options);

                return weatherObject;

            } else
            {
                throw new Exception("GetWheatherInfo FAILED.");
            }
        }

        public MyWeatherInfo ParseWeatherInfo(WeatherInfo weatherInfo) 
        {
            return new MyWeatherInfo
            {
                Temp = weatherInfo.Main.Temp,
                Humidity = weatherInfo.Main.Humidity,
                Pressure = weatherInfo.Main.Pressure,
                WindSpeed = weatherInfo.Wind.Speed,
                Icon = weatherInfo.Weather.First().Icon
            };
        }
    }
}

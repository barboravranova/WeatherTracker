using SharedLibrary;
using SharedLibrary.JsonObjects.FileContent;
using SharedLibrary.JsonObjects.WeatherInfo;

namespace WeatherTrackerLogging
{
    internal class Program
    {
        static List<Tuple<string, string, string>> citiesList = new List<Tuple<string, string, string>>
        {
            new Tuple<string, string, string>("50.0755", "14.4378", "Praha"),
            new Tuple<string, string, string>("49.1951", "16.6068", "Brno"),
            new Tuple<string, string, string>("49.8209", "18.2625", "Ostrava"),
            new Tuple<string, string, string>("49.4718", "17.1128", "Prostějov")
        };

        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    var currentDateTime = DateTime.Now;

                    foreach (var city in citiesList)
                    {
                        LogDataFromAPI(city.Item1, city.Item2, city.Item3);
                    }

                    Thread.Sleep(TimeSpan.FromMinutes(60 - currentDateTime.Minute));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Program bude dále pokračovat.");
                }
            }
        }

        static void LogDataFromAPI(string lat, string lon, string city)
        {
            string currentTime = DateTime.Now.ToString("HH");
            var api = new OpenWeatherAPI();

            WeatherInfo weatherInfo = api.GetWheatherInfo(lat, lon);
            MyWeatherInfo myWeatherInfo = api.ParseWeatherInfo(weatherInfo);

            WeatherFileManager weatherFileManager = new WeatherFileManager();
            weatherFileManager.UpdateMyWeatherInfo(currentTime, city, myWeatherInfo);
        }

    }
}
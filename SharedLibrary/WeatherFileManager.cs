using System.Linq;
using System.Text.Json;
using SharedLibrary.JsonObjects.FileContent;

namespace SharedLibrary
{
    public class WeatherFileManager
    {
        private readonly string filePath = AppDomain.CurrentDomain.BaseDirectory + "database.json";

        private readonly Dictionary<string, Dictionary<string, MyWeatherInfo>> logObject;

        public WeatherFileManager()
        {
            if (File.Exists(filePath))
            {
                string fileContent = File.ReadAllText(filePath);
                logObject = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, MyWeatherInfo>>>(fileContent);
            }
            else
            {
                logObject = new Dictionary<string, Dictionary<string, MyWeatherInfo>>();
            }
        }

        public void UpdateMyWeatherInfo(string time, string city, MyWeatherInfo myWeatherInfo)
        {
            Dictionary<string, MyWeatherInfo> cityDictionary;

            if (!logObject.TryGetValue(time, out cityDictionary))
            {
                cityDictionary = new Dictionary<string, MyWeatherInfo>();
            }

            cityDictionary[city] = myWeatherInfo;
            logObject[time] = cityDictionary;

            string jsonContent = JsonSerializer.Serialize(logObject, new JsonSerializerOptions{WriteIndented = true});
            File.WriteAllText(filePath, jsonContent);
        }

        public MyWeatherInfo GetMyWeatherInfo(string time, string city)
        {

            Dictionary<string, MyWeatherInfo> cityDictionary;

            if (!logObject.TryGetValue(time, out cityDictionary))
            {
                time = (int.Parse(time) - 1).ToString();
                logObject.TryGetValue(time, out cityDictionary);
            }
            
            MyWeatherInfo myWeatherObject;
            cityDictionary.TryGetValue(city, out myWeatherObject);

            return myWeatherObject;
        }

        public double GetMaxTemp(string city)
        {
            return GetCityTemps(city).Max();
        }

        public double GetMinTemp(string city)
        {
            return GetCityTemps(city).Min();
        }

        private List<double> GetCityTemps(string city)
        {
            List<double> cityTemps = new List<double>();

            foreach (var data in logObject)
            {
                if (data.Value.TryGetValue(city, out var cityTemp))
                {
                    cityTemps.Add(cityTemp.Temp);
                }
            }
            return cityTemps;
        }
    }
}

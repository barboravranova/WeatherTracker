using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedLibrary;

namespace WeatherTrackerWeb.Pages
{
    public class IndexModel : PageModel
    {
        public double CurrentTemp { get; set; }
        public double MinTemp { get; set; }
        public double MaxTemp { get; set; }
        public int Humidity { get; set; }
        public int Pressure { get; set; }
        public double WindSpeed { get; set; }
        public string IconPath { get; set; }
        public string CurrentCity { get; set; }
        public string CurrentDate { get; set; }
        public string CurrentDay { get; set; }
        public void OnGet(string citySelect = "Brno")
        {
            var now = DateTime.Now;

            string currentTimeHour = now.ToString("HH");

            var weatherFileManager = new WeatherFileManager();
            var myWeatherInfo = weatherFileManager.GetMyWeatherInfo(currentTimeHour, citySelect);

            this.CurrentTemp = myWeatherInfo.Temp;
            this.Humidity = myWeatherInfo.Humidity;
            this.Pressure = myWeatherInfo.Pressure;
            this.WindSpeed = myWeatherInfo.WindSpeed;
            this.IconPath = $"https://openweathermap.org/img/w/{myWeatherInfo.Icon}.png";

            this.MaxTemp = weatherFileManager.GetMaxTemp(citySelect);
            this.MinTemp = weatherFileManager.GetMinTemp(citySelect);

            this.CurrentCity = citySelect;
            this.CurrentDay = GetCzechDay(now.DayOfWeek.ToString());
            this.CurrentDate = now.ToString("dd.MM.yyy");
        }

        public string GetCzechDay(string englishDay)
        {
            Dictionary<string, string> dayTranslations = new Dictionary<string, string>
            {
                { "Monday", "Pondìlí" },
                { "Tuesday", "Úterý" },
                { "Wednesday", "Støeda" },
                { "Thursday", "Ètvrtek" },
                { "Friday", "Pátek" },
                { "Saturday", "Sobota" },
                { "Sunday", "Nedìle" }
            };

            dayTranslations.TryGetValue(englishDay, out var czechDay);

            return czechDay;
        }
    }
}

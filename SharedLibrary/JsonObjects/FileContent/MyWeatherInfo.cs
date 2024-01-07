using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.JsonObjects.FileContent
{
    public class MyWeatherInfo
    {
        public double Temp { get; set; }
        public int Humidity { get; set; }
        public int Pressure { get; set; }
        public double WindSpeed { get; set; }
        public string Icon { get; set; }
    }
}

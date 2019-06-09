using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWeather.Models
{
    public class OpenWeatherMap
    {
        public string apiResponse { get; set; }

        public Dictionary<string, string> Cities{ get; set; }
        public int IdCities { get; set; }
    }
}
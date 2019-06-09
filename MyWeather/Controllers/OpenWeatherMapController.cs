using MyWeather.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace MyWeather.Controllers
{
    [Authorize]
    public class OpenWeatherMapController : Controller
    {
        public OpenWeatherMap FillCity()
        {
            OpenWeatherMap openWeatherMap = new OpenWeatherMap();
            openWeatherMap.Cities = new Dictionary<string, string>();
            //openWeatherMap.Cities.Add("Melbourne", "7839805");
            //openWeatherMap.Cities.Add("Auckland", "2193734");
            openWeatherMap.Cities.Add("New Delhi", "1261481");
            openWeatherMap.Cities.Add("Abu Dhabi", "292968");
            openWeatherMap.Cities.Add("Lahore", "1172451");
            return openWeatherMap;
        }

        //[HttpGet]
        public ActionResult Index()
        {
            OpenWeatherMap openWeatherMap = FillCity();
            return View(openWeatherMap);
        }

        [HttpPost]
        public ActionResult Index(OpenWeatherMap openWeatherMap, string cities)
        {
            openWeatherMap = FillCity();

            if (cities != null)
            {
                string apiKey = "b3c828810d3a5a76bc521cf9479b61a4";
                HttpWebRequest apiRequest = WebRequest.Create("http://api.openweathermap.org/data/2.5/weather?id=" +
                cities + "&appid=" + apiKey + "&units=metric") as HttpWebRequest;

                string apiResponse = "";
                HttpWebResponse response = (HttpWebResponse)apiRequest.GetResponse();

                var reader = new StreamReader(response.GetResponseStream());
                apiResponse = reader.ReadToEnd();

                ResponseWeather rootObject = JsonConvert.DeserializeObject<ResponseWeather>(apiResponse);

                StringBuilder sb = new StringBuilder();
                sb.Append("<table><tr><th>Weather Description</th></tr>");
                sb.Append("<tr><td>City:</td><td>" + rootObject.name + "</td></tr>");
                sb.Append("<tr><td>Country:</td><td>" + rootObject.sys.country + "</td></tr>");
                sb.Append("<tr><td>Wind:</td><td>" + rootObject.wind.speed + " Km/h</td></tr>");
                sb.Append("<tr><td>Current Temperature:</td><td>" + rootObject.main.temp + " °C</td></tr>");
                sb.Append("<tr><td>Min Temperature:</td><td>" + rootObject.main.temp_min + " °C</td></tr>");
                sb.Append("<tr><td>Max Temperature:</td><td>" + rootObject.main.temp_max + " °C</td></tr>");
                sb.Append("<tr><td>Humidity:</td><td>" + rootObject.main.humidity + "</td></tr>");
                sb.Append("<tr><td>Weather:</td><td>" + rootObject.weather[0].description + "</td></tr>");
                sb.Append("</table>");
                openWeatherMap.apiResponse = sb.ToString();
            }
            else
            {
                if (Request.Form["submit"] != null)
                {
                    openWeatherMap.apiResponse = "Select City";
                }
            }
            return View(openWeatherMap);
        }
    }
}
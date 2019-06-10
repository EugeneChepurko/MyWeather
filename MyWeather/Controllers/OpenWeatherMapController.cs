using MyWeather.Models;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyWeather.Controllers
{
    [Authorize]
    public class OpenWeatherMapController : Controller
    {
        public static async Task<ResponseWeather> GetWeather(string city)
        {
            var http = new HttpClient();
            var response = await http.GetAsync($"http://api.openweathermap.org/data/2.5/weather?q={city}&appid=b3c828810d3a5a76bc521cf9479b61a4&units=metric");
            var result = await response.Content.ReadAsStringAsync();

            var serializer = new DataContractJsonSerializer(typeof(ResponseWeather));
            var memory_stream = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (ResponseWeather)serializer.ReadObject(memory_stream);
            return data;          
        }
        [HttpGet]
        public async Task<ActionResult> Click(string city)
        {         
            ResponseWeather weather = await GetWeather(city);
            return View(weather);
        }

        public ActionResult Index()
        {
            return View();
        }                 
    }
}
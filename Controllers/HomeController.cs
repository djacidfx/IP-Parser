using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IPParser.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index(string IpAddress)
        {
            // Create the Http client
            HttpClient client = new HttpClient();

            // Build the Http request string
            string apiUrl = "https://freegeoip.net/json/" + IpAddress;

            // Send the GET request
            var response = client.GetAsync(apiUrl).Result;

            if (response != null && ((response.ReasonPhrase != "Unauthorized") && (response.ReasonPhrase != "Not Found")))
            {
                // Get the response string
                string strResponse = await response.Content.ReadAsStringAsync();

                // Deserialize the string as an GeoIPRespContent object
                var geoIP = Newtonsoft.Json.JsonConvert.DeserializeObject<GeoIPRespContent>(strResponse);

                // Store it in the dictionnary for disply
                ViewData["ip"] = geoIP.ip;
                ViewData["country_code"] = geoIP.country_code;
                ViewData["country_name"] = geoIP.country_name;
                ViewData["region_code"] = geoIP.region_code;
                ViewData["region_name"] = geoIP.region_name;
                ViewData["city"] = geoIP.city;
                ViewData["zip_code"] = geoIP.zip_code;
                ViewData["time_zone"] = geoIP.time_zone;
                ViewData["latitude"] = geoIP.latitude;
                ViewData["longitude"] = geoIP.longitude;
                ViewData["metro_code"] = geoIP.metro_code;
            }

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }

    // Class coresponding to the GeoIp response string structure
    public class GeoIPRespContent
    {
        public string ip { get; set; }
        public string country_code { get; set; }
        public string country_name { get; set; }
        public string region_code { get; set; }
        public string region_name { get; set; }
        public string city { get; set; }
        public string zip_code { get; set; }
        public string time_zone { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public int metro_code { get; set; }
    }
}

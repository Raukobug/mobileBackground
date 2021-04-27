using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace WorldApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MobileController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(string lat, string lng)
        {
            try{
            List<string> bDays = new List<string>();

            bDays.Add("19-06");
            bDays.Add("08-12");
            bDays.Add("05-09");
            bDays.Add("12-01");
            bDays.Add("05-04");

            var client = new HttpClient();
            var timeSpanOffset = GetTime(lat, lng);
            var date = DateTime.UtcNow.Add(timeSpanOffset);
            string offset = timeSpanOffset > TimeSpan.FromSeconds(0) ? timeSpanOffset.ToString("hh':'mm") : "-"+timeSpanOffset.ToString("hh':'mm");
            var productValue = new ProductInfoHeaderValue("Schalls", "1.0");
            client.DefaultRequestHeaders.UserAgent.Add(productValue);
            var jsonResult = client.GetAsync("https://api.met.no/weatherapi/locationforecast/2.0/compact.json?lat="+lat+"&lon="+lng).Result.Content.ReadAsStringAsync().Result;
            var weather = JsonConvert.DeserializeObject<WeatherForecast>(jsonResult);
            var sunriseResult = client.GetAsync("https://api.met.no/weatherapi/sunrise/2.0/.json?date="+date.ToString("yyyy-MM-dd")+"&lat="+lat+"&lon="+lng+"&offset="+offset).Result.Content.ReadAsStringAsync().Result;
            var sunrise = JsonConvert.DeserializeObject<Sunrise>(sunriseResult);

            var img = 0;
            var sunriseTime = sunrise.Location.Time.First().Sunrise.Time;
            var sunsetTime = sunrise.Location.Time.First().Sunset.Time;
            var diff = sunsetTime - sunriseTime;

            if(date < sunriseTime){
                img = 8;
            }else if(date > sunsetTime){
                img = 7;
            }else if(sunriseTime.AddHours(1) > date){
                img = 1;
            }else{
                for(var i = 2; i < 7; i++){
                    if(sunriseTime.AddHours(1).Add((i*diff/5)) > date && img == 0){
                        img = i;
                    }
                }
            }
            var urlString = img+"";
            if(img < 7 && bDays.Contains(date.ToString("dd-MM"))){
                urlString += "-flag";
            }else{
                var weatherData = weather.Properties.Timeseries.Where(x => x.Time > date.AddHours(-1) && x.Time < date).Single();
                if(weatherData.Data.Next1_Hours.Summary.SymbolCode.Contains("rain") || weatherData.Data.Next1_Hours.Summary.SymbolCode.Contains("sleet"))
                urlString += "-rain";
            }

            Byte[] b = System.IO.File.ReadAllBytes(@"assets/images/"+urlString+".png");
            return File(b, "image/jpeg");
            }
            catch(Exception e){
                return Ok(e.ToString());
            }
        }

        [HttpGet]
        [Route("time")]
        public IActionResult Time(string lat, string lng)
        {
            return Ok(DateTime.UtcNow.Add(GetTime(lat, lng)));
        }

        public TimeSpan GetTime(string lat, string lng)
        {
            var date = DateTime.UtcNow;
            HttpClient client = new HttpClient();
	        var request = "https://maps.googleapis.com/maps/api/timezone/json?location="+lat+","+lng+"&timestamp="+ToTimestamp(date)+"&sensor=false&key=AIzaSyAk1Pi8fiffqT44iguCP4raVpSGGYsODRw";
	        var resultJson = client.GetAsync(request).Result.Content.ReadAsStringAsync().Result;
            Console.Write(resultJson);
            var result = JsonConvert.DeserializeObject<GoogleTimeZone>(resultJson);
            long val = result.DstOffset+result.RawOffset;
	        TimeSpan timeResult = TimeSpan.FromSeconds(val);
	        //string fromTimeString = //timeResult > TimeSpan.FromSeconds(0) ? timeResult.ToString("hh':'mm") : "-"+timeResult.ToString("hh':'mm");
            return timeResult;
        }

        public static double ToTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }
    }
}
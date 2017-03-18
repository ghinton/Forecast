using System.Collections.Generic;
using Newtonsoft.Json;

namespace Forecast.Models
{
    public class OpenWeatherForecast
    {
        [JsonProperty(PropertyName = "city")]
        public City City { get; set; }
        //public string cod { get; set; }
        //public double message { get; set; }
        public int cnt { get; set; } // number of rows of forecast data (should be 40)
        [JsonProperty(PropertyName = "list")] // map list to type of timeslice lists
        public List<ForecastTimeslice> Timeslices { get; set; }
    }

    //[JsonObject("city")]
    public class City
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; } // city name
        [JsonProperty("country")]
        public string Country { get; set; } // country code - e.g. GB
        [JsonProperty("coord")]
        public Coord Coord { get; set; } // longtitude, latitude, altitude
        //public int population { get; set; }
        //public Sys sys { get; set; }
    }

    public class Coord
    {
        [JsonProperty("lon")]
        public double Longtitude { get; set; }
        [JsonProperty("lat")]
        public double Latitude { get; set; }
    }

    /*public class Sys
    {
        public int population { get; set; }
    }*/


    /// <summary>
    /// WeatherInfo
    /// </summary>
    [JsonObject("main")]
    public class WeatherInfo
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; }
        [JsonProperty("temp_min")]
        public double MinTemperature { get; set; }
        [JsonProperty("temp_max")]
        public double MaxTemperature { get; set; }
        [JsonProperty("pressure")]
        public double Pressure { get; set; }
        //public double sea_level { get; set; }
        //public double grnd_level { get; set; }7
        [JsonProperty("humidity")]
        public int Humidity { get; set; }
        //public double temp_kf { get; set; }
    }

    [JsonObject("weather")]
    public class WeatherConditionInfo
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("main")]
        public string Main { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("icon")]
        public string Icon { get; set; }
    }

    [JsonObject("clouds")]
    public class Clouds
    {
        [JsonProperty("all")]
        public int PercentageCloudCover { get; set; }
    }

    [JsonObject("wind")]
    public class Wind
    {
        [JsonProperty("speed")]
        public double Speed { get; set; } // will be meters per sec
        [JsonProperty("deg")]
        public double Degrees { get; set; } // Wind direction (degrees)
    }

    /// <summary>
    /// Rain volume in mm for last 3 hours
    /// </summary>
    [JsonObject("rain")]
    public class Rain
    {
        [JsonProperty("3h")]
        public double RainVolumeInMm { get; set; }
    }

    /*public class Sys2
    {
        public string pod { get; set; }
    }*/

    [JsonObject("list")]
    public class ForecastTimeslice
    {
        [JsonProperty("dt")]
        public int ForecastPeriod { get; set; }
        [JsonProperty("main")]
        public WeatherInfo Info { get; set; }
        [JsonProperty("weather")]
        public List<WeatherConditionInfo> Weather { get; set; }
        [JsonProperty("clouds")]
        public Clouds Clouds { get; set; }
        [JsonProperty("wind")]
        public Wind Wind { get; set; }
        [JsonProperty("rain")]
        public Rain Rain { get; set; }
        //public Sys2 sys { get; set; }
        [JsonProperty("dt_txt")]
        public string ForecastDateTime { get; set; }
    }

}
using System.Collections.Generic;

namespace Forecast.ViewModels
{
    public class WeatherForecast
    {
        public Location City { get; set; }
        public Dictionary<string, ForecastDay> Days { get; set; }
    }

    public class Location
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string Longtitude { get; set; }
        public string Latitude { get; set; }
    }

    public class ForecastDay
    {
        public string Date { get; set; } // maps to dt_txt
        public Dictionary<string, ForecastDay3HourSlice> Timeslices { get; set; }
    }

    public class ForecastDay3HourSlice
    {
        /// <summary>
        /// The 3 hour slice of time of day
        /// </summary>
        public string Period { get; set; }
        public string MinTemperature { get; set; }
        public string MaxTemperature { get; set; }
        public string Pressure { get; set; }
        public string Humidity { get; set; }
        public string WindSpeed { get; set; }
        public string WindDirectionInDegrees { get; set; }
        public string WindSummary { get; set; }
        public string RainVolumeInMm { get; set; }
        public string CloudCoverPercentage { get; set; }

        /// <summary>
        /// Maps to WeatherConditionInfo - description
        /// </summary>
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;

using Forecast.Helper;
using Forecast.Models;
using Forecast.Models.ViewModels;

namespace Forecast.Manager
{
    public class ForecastManager : BaseManager
    {
        private static readonly string weatherIconPrefixUrl = ConfigurationManager.AppSettings["weatherIconUrlPrefix"];
        private const string CONST_degreesFarenheit = "&#8451;";

        /// <summary>
        /// Takes an incoming open weather forecast response and transforms it to a WeatherForecast class instance
        /// </summary>
        /// <param name="forecastJson"></param>
        /// <param name="cityName"></param>
        /// <returns></returns>
        public WeatherForecast GetForecastByCity(string forecastJson, string cityName)
        {
            string errMsg = string.Empty;
            var wf = new WeatherForecast();
            try
            {
                // Connect to the weather API web service
                /* Params
                 * For temperature in Fahrenheit use units=imperial
                    For temperature in Celsius use units=metric
                    Temperature in Kelvin is used by default, no need to use units parameter in API call

                    Imperial will return wind in mph, metric in metres per second
                 */
                // TO DO - REMOVE TO SEPARATE MANAGER LAYER
                if (!string.IsNullOrEmpty(forecastJson))
                {
                    var f = JsonHelper<OpenWeatherForecast>.JsonDeserialize(forecastJson); // attempt to map the received data into our forecast model object

                    // Now map the incoming object into a view model before dispatch
                    wf.City = new Location
                    {
                        Country = f.City.Country,
                        Name = f.City.Name,
                        Latitude = f.City.Coord.Latitude.ToString(),
                        Longtitude = f.City.Coord.Longtitude.ToString()
                    };
                    wf.Days = new Dictionary<string, ForecastDay>();

                    // Loop through the 3 hour slices (should be 40) and assign to correct dates; they're returned in date asc order so no need to check...yet
                    foreach (var ts in f.Timeslices)
                    {
                        // incoming time - UTC: "2017-03-13 06:00:00"
                        DateTime dt;
                        if (!DateTime.TryParseExact(ts.ForecastDateTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out dt))
                        {
                            errMsg = "Invalid date format specified in received forecast openWeather forecast date - attempt to parse " + ts.ForecastDateTime + " failed";
                            throw new ApplicationException(errMsg);
                        }
                        var sliceDate = dt.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);

                        // Now build the 3 hour slice
                        WeatherConditionInfo info = (ts.Weather.Count > 0) ? ts.Weather[0] : new WeatherConditionInfo { Description = "No Description Available", Icon = "", Id = -1, Main = "" };
                        string windArrowCode = "";
                        var windArrows = new Dictionary<Func<double, bool>, Action>
                        {
                            { x => ((x > 337.5  && x <= 360) || (x >=0 && x < 22.5)), () => windArrowCode = "&#8593;" }, // north
                            { x => (x >= 22.5 && x < 67.5), () => windArrowCode = "&#8599;"}, // north east
                            { x => (x >= 67.5 && x < 112.5), () => windArrowCode = "&#8594;"}, // east
                            { x => (x >= 112.5 && x < 157.5), () => windArrowCode = "&#8600;"} , // south east
                            { x => (x >= 157.5 && x < 202.5),  () => windArrowCode = "&#8595;"}, // south
                            { x => (x >= 202.5 && x < 247.5), () => windArrowCode = "&#8601;"}, // south west
                            { x => (x >= 247.5 && x < 292.5), () => windArrowCode = "&#8592;"}, // west
                            { x => (x >= 292.5 && x <= 337.5), () => windArrowCode = "&#8598;"}, // north west
                        };
                        windArrows.First(sw => sw.Key(ts.Wind.Degrees)).Value(); // trigger a set

                        var slice = new ForecastDay3HourSlice()
                        {
                            CloudCoverPercentage = ts.Clouds.PercentageCloudCover.ToString() + "%",
                            Description = (ts.Weather.Count > 0) ? ts.Weather[0].Description : "No Description Available",
                            Humidity = ts.Info.Humidity.ToString() + "%",
                            Icon = "http://openweathermap.org/img/w/" + info.Icon + ".png",
                            MaxTemperature = ts.Info.MaxTemperature.ToString("0") + CONST_degreesFarenheit,
                            MinTemperature = ts.Info.MinTemperature.ToString("0") + CONST_degreesFarenheit,
                            Period = dt.ToString("HH:mm"),
                            Pressure = ts.Info.Pressure.ToString(),
                            RainVolumeInMm = ((ts.Rain != null) ? ts.Rain.RainVolumeInMm.ToString() : "0") + "mm",
                            WindDirectionInDegrees = ts.Wind.Degrees.ToString(), // http://htmlarrows.com could be used here.
                            WindSpeed = ts.Wind.Speed.ToString(), // metres per second
                            WindSummary = windArrowCode + " " + (ts.Wind.Speed * 2.23694).ToString("0") + "mph" // convert the wind speed from metres per second to mph and round
                        };

                        // If this date already exists, add the new slice to it, otherwise add the date, then the slice
                        ForecastDay fd;
                        if (!wf.Days.ContainsKey(sliceDate))
                        {
                            fd = new ForecastDay
                            {
                                Date = sliceDate,
                                Timeslices = new Dictionary<string, ForecastDay3HourSlice>()
                            };
                            wf.Days.Add(sliceDate, fd);
                        }
                        fd = wf.Days[sliceDate];
                        // Add the slice period to the date entry
                        fd.Timeslices.Add(slice.Period, slice);
                    } // end timeslice loop
                } // end strResponse if
            }
            catch (Exception ex)
            {
                if (string.IsNullOrEmpty(errMsg))
                {
                    errMsg = "An unrecoverable system error occurred";
                }
                errorCollection.Add(new Tuple<string, Exception>(errMsg, ex));
                wf.ErrorMessages.Add(errMsg);
                // TO DO - ADD INTERNAL LOGGING
            }
            return wf;
        }
    }
}
using System.Configuration;
using System.IO;
using System.Net;

using Forecast.Data.Interfaces;

namespace Forecast.Data
{
    public class OpenWeather : IOpenWeather
    {
        private static readonly string weatherWebServiceEndPointUrl = ConfigurationManager.AppSettings["weatherApiUrl"];
        private static readonly string weatherWebServiceResourceKey = ConfigurationManager.AppSettings["weatherApiResourceKey"];

        public string GetForecastJson(string cityName, out string errors)
        {
            // Connect to the weather API web service
            /* Params
             * For temperature in Fahrenheit use units=imperial
                For temperature in Celsius use units=metric
                Temperature in Kelvin is used by default, no need to use units parameter in API call

                Imperial will return wind in mph, metric in metres per second

                We could potentially move this out to a separate class
             */
            errors = string.Empty;
            var strResponse = string.Empty;
            var wr = WebRequest.Create(string.Format("{0}&appid={1}", weatherWebServiceEndPointUrl.Replace("{cityid}", cityName), weatherWebServiceResourceKey));
            using (var response = wr.GetResponse())
            {
                var responseCode = ((HttpWebResponse)response).StatusCode;
                if (responseCode != HttpStatusCode.OK)
                {
                    errors = "Unable to retrieve data from Weather Service at " + weatherWebServiceEndPointUrl;
                    return null;
                }
                using (var dataStream = response.GetResponseStream())
                {
                    if (dataStream != null)
                    {
                        using (var reader = new StreamReader(dataStream))
                        {
                            strResponse = reader.ReadToEnd();
                        }
                    }
                }
            }
            return strResponse;
        }
    }
}

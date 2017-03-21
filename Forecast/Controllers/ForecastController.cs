using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Forecast.Manager;
using Forecast.Models.ViewModels;

namespace Forecast.Controllers
{
    [RoutePrefix("api/forecast")]
    public class ForecastController : BaseController
    {
        private static readonly string weatherWebServiceEndPointUrl = ConfigurationManager.AppSettings["weatherApiUrl"];
        private static readonly string weatherWebServiceResourceKey = ConfigurationManager.AppSettings["weatherApiResourceKey"];

        [Route("{cityName}")]
        public HttpResponseMessage GetForecast(string cityName)
        {
            string errMsg = string.Empty;
            var wf = new WeatherForecast();

            try
            {
                if (string.IsNullOrEmpty(cityName))
                {
                    wf.ErrorMessages.Add("City name was not specified");
                    throw new ApplicationException(errMsg);
                }
                var strResponse = string.Empty;

                // Connect to the weather API web service
                /* Params
                 * For temperature in Fahrenheit use units=imperial
                    For temperature in Celsius use units=metric
                    Temperature in Kelvin is used by default, no need to use units parameter in API call

                    Imperial will return wind in mph, metric in metres per second
                 */
                var wr = WebRequest.Create(string.Format("{0}&appid={1}", weatherWebServiceEndPointUrl.Replace("{cityid}", cityName), weatherWebServiceResourceKey));
                using (var response = wr.GetResponse())
                {
                    var responseCode = ((HttpWebResponse)response).StatusCode;
                    if (responseCode != HttpStatusCode.OK)
                    {
                        wf.ErrorMessages.Add("Unable to retrieve data from Weather Service at " + weatherWebServiceEndPointUrl);
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

                ForecastManager fm = new ForecastManager();
                wf = fm.GetForecastByCity(strResponse, cityName);
                return Request.CreateResponse(HttpStatusCode.OK, wf);
            }
            catch (Exception ex)
            {
                if (wf.ErrorMessages.Count == 0)
                {
                    wf.ErrorMessages.Add("An unrecoverable system error occurred");
                }

                // TO DO - ADD INTERNAL LOGGING TO FILE
                return Request.CreateResponse(HttpStatusCode.InternalServerError, wf);
            }
        }
    }
}
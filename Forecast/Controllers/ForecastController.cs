using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Forecast.Manager.Interfaces;
using Forecast.Models.ViewModels;

namespace Forecast.Controllers
{
    [RoutePrefix("api/forecast")]
    public class ForecastController : BaseController
    {
        private readonly IForecastManager _forecastManager;

        public ForecastController() : this(new Manager.ForecastManager())
        {
            
        }

        public ForecastController(IForecastManager forecastManager)
        {
            if(forecastManager == null)
            {
                throw new ArgumentNullException("forecastManager not supplied");
            }
            _forecastManager = forecastManager;
        }

        [Route("{cityName}")]
        public HttpResponseMessage GetForecast(string cityName)
        {
            string errMsg = string.Empty;
            var wf = new WeatherForecast();

            try
            {
                // Connect to the weather API web service
                var strResponse = _forecastManager.GetForecastByCityJson(cityName, out errMsg);
                if (!string.IsNullOrEmpty(errMsg))
                {
                    wf.ErrorMessages.Add(errMsg);
                }
                else
                {
                    wf = _forecastManager.GetForecastByCity(strResponse, cityName);
                }

                if (Request != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, wf);
                }
                else
                {
                    HttpRequestMessage hrm = new HttpRequestMessage();
                    return hrm.CreateResponse(HttpStatusCode.OK, wf, GlobalConfiguration.Configuration);
                }
            }
            catch (Exception ex)
            {
                if (wf.ErrorMessages.Count == 0)
                {
                    wf.ErrorMessages.Add("An unrecoverable system error occurred");
                }

                // TO DO - ADD INTERNAL LOGGING TO FILE
                if(Request != null)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, wf);
                }
                // Unit Testing alternative
                HttpRequestMessage hrm = new HttpRequestMessage();
                return hrm.CreateResponse(HttpStatusCode.InternalServerError, wf, GlobalConfiguration.Configuration);
            }
        }
    }
}
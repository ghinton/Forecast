using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Forecast.Controllers
{
    [RoutePrefix("api/location")]
    public class LocationController : BaseController
    {
        public IHttpActionResult GetLocation(string cityName)
        {
            // Validation
            if(String.IsNullOrEmpty(cityName))
            {
                return NotFound();
            }

            // Attempt to fetch the city code from a web service
            return null;
        }
    }
}

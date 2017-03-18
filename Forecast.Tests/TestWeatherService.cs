using System;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Forecast.Tests
{
    [TestClass]
    public class TestWeatherService
    {
        [TestMethod]
        public void GetWeatherWithNoCity()
        {
            var fc = new Controllers.ForecastController();
            IHttpActionResult r = fc.GetForecast(string.Empty);
        }

        [TestMethod]
        public void GetWeatherWithInvalidCity()
        {

        }

        [TestMethod]
        public void GetWeatherWithValidCity()
        {

        }



        // Fetch web service call with pre-supplied city
        // Transform
    }
}

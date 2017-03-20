using System;
using System.Configuration;
using System.Web.Http;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

using NUnit.Framework;

namespace Forecast.Tests
{
    [TestFixture]
    public class TestWeatherService
    {
        [OneTimeSetUp]
        public void Init()
        {
            Console.WriteLine("Setting up Test Fixtures");
        }

        [Test]
        public void GetWeatherWithNoCity()
        {
            var fc = new Controllers.ForecastController();
            IHttpActionResult r = fc.GetForecast(string.Empty);
            //Assert.Throws
        }

        [Test]
        public void GetWeatherWithInvalidCity()
        {
            var fc = new Controllers.ForecastController();
        }

        [Test]
        public void GetWeatherWithValidCity()
        {

        }

        [Test]
        public void CheckConfigValuesArePresent()
        {
            if (String.IsNullOrEmpty(ConfigurationManager.AppSettings["weatherApiUrl"]) || String.IsNullOrEmpty(ConfigurationManager.AppSettings["weatherApiResourceKey"]) || String.IsNullOrEmpty(ConfigurationManager.AppSettings["weatherIconUrlPrefix"]))
            {

            }
        }

        [OneTimeTearDown]
        public void TearDown()
        {

        }

        // Fetch web service call with pre-supplied city
        // Transform
    }
}

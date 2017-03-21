using System;
using System.Configuration;
using System.Text;
using Newtonsoft.Json.Converters;

using NUnit.Framework;

using Forecast.Models.ViewModels;
using Forecast.Controllers;

namespace Forecast.Tests
{
    [TestFixture]
    public class TestWeatherService
    {
        ForecastController fc;

        [OneTimeSetUp]
        public void Init()
        {
            Console.WriteLine("Setting up Test Fixtures");
            fc = new ForecastController();
        }

        /// <summary>
        /// Run on build to ensure that JSON schema has not changed from origin
        /// </summary>
        [Test]
        public void ensureJSONSchemaIsValid()
        {
            
        }

        [Test]
        public void GetWeatherWithNoCity()
        {
            var r = fc.GetForecast(string.Empty);

            // now we can test the exception itself
            Assert.That(r.StatusCode == System.Net.HttpStatusCode.InternalServerError);

            // Test that the error collection is populated
            System.Threading.Tasks.Task<string> s = r.Content.ReadAsStringAsync();
            s.Wait();
            var wf = Helper.JsonHelper<WeatherForecast>.JsonDeserialize(s.Result);
            Assert.That(wf.ErrorMessages.Contains("City name was not specified"), "The controller returned an incorrect error message");
        }

        [Test]
        public void GetWeatherWithInvalidCity()
        {
            var r = fc.GetForecast("InvalidCity,InvalidCountry");

            // now we can test the exception itself
            Assert.That(r.StatusCode == System.Net.HttpStatusCode.InternalServerError);
        }

        [Test]
        public void GetWeatherWithValidCity()
        {
            var r = fc.GetForecast("London,GB");
            Assert.That(r.StatusCode == System.Net.HttpStatusCode.OK, "Failed to retrieve Forecast for London GB");
        }

        [Test]
        public void CheckConfigValuesArePresent()
        {
            var configErrors = new StringBuilder();
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["weatherApiUrl"])) {
                configErrors.AppendFormat("weatherApiUrl not specified in config");
            }
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["weatherApiResourceKey"]))
            {
                configErrors.AppendFormat("weatherApiResourceKey not specified in config");
            }
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["weatherIconUrlPrefix"]))
            {
                configErrors.AppendFormat("weatherIconUrlPrefix not specified in config");
            }
            if (configErrors.Length > 0)
            {
                Assert.Fail(configErrors.ToString());
            }
            else
            {
                Assert.Pass("Required Config Keys Present");
            }
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            fc = null;
            Console.WriteLine("Tests completed");
        }

        // Fetch web service call with pre-supplied city
        // Transform
    }
}

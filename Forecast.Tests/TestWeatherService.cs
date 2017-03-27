using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;

using Moq;
using NUnit.Framework;

using Forecast.Models.ViewModels;
using Forecast.Controllers;
using Forecast.Manager;
using Forecast.Manager.Interfaces;

namespace Forecast.Tests
{
    [TestFixture]
    public class TestWeatherService
    {
        ForecastController fc;
        IForecastManager fm;

        [OneTimeSetUp]
        public void Init()
        {
            Console.WriteLine("Setting up Test Fixtures");
            fm = new ForecastManager();
            fc = new ForecastController(fm);
        }

        /// <summary>
        /// Run on build to ensure that JSON schema has not changed from origin
        /// </summary>
        [Test]
        public void ensureJSONSchemaIsValid()
        {
            
        }

        [Test]
        public void TestForecastManager()
        {
            string testJson = "testResult";
            string str = string.Empty;
            var city = "London,GB";
            var testLocation = new Location()
            {
                Country = "GB",
                Latitude = "1",
                Longtitude = "2",
                Name = city
            };
            var fm2 = new Mock<IForecastManager>();
            fm2.Setup(mgr => mgr.GetForecastByCity(It.IsAny<string>(), It.IsAny<string>())).Returns(new WeatherForecast() { City = testLocation, ErrorMessages = new List<string>() { "Test Error" }, Days = new Dictionary<string, ForecastDay>() });
            fm2.Setup(mgr => mgr.GetForecastByCityJson(city, out str)).Returns(testJson);

            // Verify that manager calls return correctly
            var strJson = fm2.Object.GetForecastByCityJson(city, out str);
            Assert.AreEqual(strJson, testJson, "The expected JSON string was not returned");
        }

        [Test]
        public void TestForecastController()
        {
            var city = "London,GB";
            var errorMsg = string.Empty;

            // Given a forecast controller
            // When a forecast manager is not passed into the constructor
            // Then throw an argument null exception
            Assert.Throws<ArgumentNullException>(() => { new ForecastController(null); }, "Forecast Controller should not permit instantiation without a valid ForecastManager");

            var fMgr = new Mock<IForecastManager>();
            var fakeForecast = getFakeForecastObject();
            fMgr.Setup(fcst => fcst.GetForecastByCity(It.IsAny<string>(), It.IsAny<string>())).Returns(fakeForecast);
            var fc = new ForecastController(fMgr.Object);
            var responseObj = fc.GetForecast(city);

            fMgr.Verify(mgr => mgr.GetForecastByCityJson(city, out errorMsg), Times.Once, "GetForecastByCityJson method not called from ForecastManager class");
            fMgr.Verify(mgr => mgr.GetForecastByCity(It.IsAny<string>(), city), Times.Once, "GetForecastByCity method not called from ForecastManager class");

            Assert.That(responseObj.StatusCode == HttpStatusCode.OK, "The response returned indicated an error");

            // Test that the error collection is populated
            System.Threading.Tasks.Task<string> s = responseObj.Content.ReadAsStringAsync();
            s.Wait();
            var wf = Helper.JsonHelper<WeatherForecast>.JsonDeserialize(s.Result);
            var wf2 = getFakeForecastObject();

            // Compare both objects to ensure they're identical in terms of value rather than reference
            string wf1Json = JsonConvert.SerializeObject(wf);
            string wf2Json = JsonConvert.SerializeObject(wf2);

            Assert.AreEqual(wf1Json, wf2Json, "The result received from the controller was incorrect");
            Assert.That(wf.ErrorMessages.Count == 1, "An error should be present in the returned forecast object");
        }


        #region Implementation Tests

        [Test]
        public void CheckConfigValuesArePresent()
        {
            var configErrors = new StringBuilder();
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["weatherApiUrl"]))
            {
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
        }

        [Test]
        public void GetWeatherWithNoCity()
        {
            // Given that city is not supplied
            // When calling GetForecast
            // Then Expect a response of OK with an empty forecast object and an error collection of 1
            var r = fc.GetForecast(string.Empty);
            Assert.That(r.StatusCode == HttpStatusCode.OK, "Even when the city is not supplied the API should return an empty object");

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
            Assert.That(r.StatusCode == HttpStatusCode.InternalServerError);
        }

        [Test]
        public void GetWeatherWithValidCity()
        {
            var r = fc.GetForecast("London,GB");
            Assert.That(r.StatusCode == HttpStatusCode.OK, "Failed to retrieve Forecast for London GB");
        }

        #endregion Implementation Tests

        private WeatherForecast getFakeForecastObject()
        {
            return new WeatherForecast()
            {
                City = new Location()
                {
                    Country = "GB",
                    Latitude = "1",
                    Longtitude = "2",
                    Name = "London"
                },
                ErrorMessages = new List<string>() { "Test Error" },
                Days = new Dictionary<string, ForecastDay>()
            };
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            fc = null;
            fm = null;
            Console.WriteLine("Tests completed");
        }

    }
}

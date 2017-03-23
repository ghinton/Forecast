using Forecast.Models.ViewModels;

namespace Forecast.Manager.Interfaces
{
    public interface IForecastManager
    {
        WeatherForecast GetForecastByCity(string forecastJson, string cityName);
        string GetForecastByCityJson(string cityName, out string errors);
    }
}
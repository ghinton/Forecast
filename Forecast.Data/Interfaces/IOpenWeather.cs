namespace Forecast.Data.Interfaces
{
    public interface IOpenWeather
    {
        string GetForecastJson(string cityName, out string errors);
    }
}

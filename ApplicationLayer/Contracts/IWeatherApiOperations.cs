using ApplicationLayer.Data.ApiObjects.WeatherAPI;

namespace ApplicationLayer.Contracts
{
    /// <summary>
    /// Interface for the weather operations service.
    /// </summary>
    public interface IWeatherApiOperations
    {
        Task<WeatherResponseObject> GetWeatherData(int appUserId, string locationName);
        string GetWeatherIconPath(string originalPath);
        Task UpdateUserRequests(int appUserId, DateTime currentTimestamp);
        Task UpdateWeatherRequestTable(string locationName, double temperature, DateTime currentTimestamp);
    }
}
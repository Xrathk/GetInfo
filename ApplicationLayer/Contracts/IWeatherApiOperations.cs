using ApplicationLayer.Data.ApiObjects.WeatherAPI;

namespace ApplicationLayer.Contracts
{
    /// <summary>
    /// Interface for the weather operations service.
    /// </summary>
    public interface IWeatherApiOperations
    {
        /// <summary>
        /// Gets weather DATA via the open WeatherApi.
        /// </summary>
        /// <param name="appUserId">The user's ID</param>
        /// <param name="locationName">The searched location</param>
        /// <returns>Expanded weather data object, including text and icon path</returns>
        Task<WeatherResponseObject> GetWeatherData(int appUserId, string locationName);

        /// <summary>
        /// Retrieves the path for the correct weather icon, based on the weather report.
        /// </summary>
        /// <param name="originalPath">The path in the original response object</param>
        /// <returns>The weather icon path (GetInfo app)</returns>
        string GetWeatherIconPath(string originalPath);

        /// <summary>
        /// Adds the weather request to the user's overall request summary.
        /// </summary>
        /// <param name="appUserId">The app user ID</param>
        /// <param name="currentTimestamp">The timestamp of the last weather request</param>
        Task UpdateUserRequests(int appUserId, DateTime currentTimestamp);

        /// <summary>
        /// Updates weather request table (per location).
        /// </summary>
        /// <param name="locationName">Name of location requested</param>
        /// <param name="temperature">Current temperature (time of request)</param>
        /// <param name="currentTimestamp">Timestamp of requests</param>
        Task UpdateWeatherRequestTable(string locationName, double temperature, DateTime currentTimestamp);
    }
}
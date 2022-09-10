using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DataAccessLayer.Contacts;
using DomainLayer.Entities;
using LogicLayer.Data.ApiObjects.WeatherAPI;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace LogicLayer.Services
{
    /// <summary>
    /// Class that handles weather requests and all database operations pertaining to them.
    /// </summary>
    public class WeatherApiOperations
    {
        // Fields
        private readonly IServiceScopeFactory scopeFactory;
        private readonly ILogger<AccountOperations> Logger;
        private readonly IConfiguration configuration;
        // DAL repos
        public readonly IRepositoryAppUserRequests _userReqRepo;
        public readonly IRepositoryWeatherRequest _weatherReqRepo;

        // API Key
        public string WeatherAPIKey;

        /// <summary>
        /// Service constuctor.
        /// </summary>
        /// <param name="_scopeFactory">Score factory</param>
        /// <param name="logger">Logger component</param>
        /// <param name="userReqRepo">User requests model repository</param>
        /// <param name="weatherReqRepo">Weather reqests model repository</param>
        public WeatherApiOperations(
            IServiceScopeFactory _scopeFactory,
            ILogger<AccountOperations> logger,
            IConfiguration _configuration,
            IRepositoryAppUserRequests userReqRepo,
            IRepositoryWeatherRequest weatherReqRepo
        )
        {
            scopeFactory = _scopeFactory;
            Logger = logger;
            configuration = _configuration;
            _userReqRepo = userReqRepo;
            _weatherReqRepo = weatherReqRepo;

            // Fetch weather API key from configuration
            WeatherAPIKey = configuration.GetValue<string>("WeatherApiKey"); // From configuration

        }

        /// <summary>
        /// Gets weather DATA via the open WeatherApi.
        /// </summary>
        /// <param name="appUserId">The user's ID</param>
        /// <param name="locationName">The searched location</param>
        /// <returns>Expanded weather data object, including text and icon path</returns>
        public async Task<WeatherResponseObject> GetWeatherData(int appUserId, string locationName)
        {
            // Formulating request and getting output
            var requestId = UtilityMethods.BackendUtilityMethods.GetRandomAlphanumericString(16);
            Logger.LogInformation("A user has requested weather data.\nRequest ID:{reqId}, User ID: {userId}, Location: {location}", requestId, appUserId, locationName);

            // Perform request
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "http://api.weatherapi.com/v1/current.json?key=" + WeatherAPIKey + "&q=" + locationName + "&aqi=no"))
                {
                    var response = await httpClient.SendAsync(request);

                    // Get status
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        // Successful request - log output and store
                        ValidResponseObject weatherData = new ValidResponseObject();
                        weatherData = JsonConvert.DeserializeObject<ValidResponseObject>(response.Content.ReadAsStringAsync().Result); // Deserialize
                        var currentTemp = weatherData.current.temp_c; // Current temperature
                        var currentWeather = weatherData.current.condition.text; // Current weather
                        DateTime currentTime = DateTime.Now; // Current timestamp
                        Logger.LogInformation("Request with ID \"{requestID}\" has returned the status code \"{statusCode}\".\nLocation: {location}, Weather: {currentWeather}, Temperature: {currentTemp}", requestId, response.StatusCode, locationName, currentWeather, currentTemp); // Logging 

                        // Update weather and user requests table
                        try
                        {
                            await UpdateWeatherRequestTable(locationName, currentTemp, currentTime);
                        }
                        catch (Exception Ex)
                        {
                            Logger.LogError("Could not store weather request data for location -- Location: {locationName}, Exception: {currentTemp}", locationName, Ex);
                        }
                        try
                        {
                            await UpdateUserRequests(appUserId, currentTime);
                        }
                        catch (Exception Ex)
                        {
                            Logger.LogError("Could not store weather request data for user -- UserId: {appUserId}, Exception: {currentTemp}", appUserId, Ex);
                        }

                        // Return valid response object
                        return new WeatherResponseObject
                        {
                            responseStatus = System.Net.HttpStatusCode.OK,
                            data = weatherData.current
                        };
                    }
                    else // If request unsuccessful, print appropriate error message.
                    {
                        InvalidResponseObject apiResponse = new InvalidResponseObject();
                        apiResponse = JsonConvert.DeserializeObject<InvalidResponseObject>(response.Content.ReadAsStringAsync().Result); // Deserialize error message
                        var errorCode = apiResponse.error.code;
                        var errorMessage = apiResponse.error.message;
                        var statusCode = response.StatusCode;
                        Logger.LogInformation("Could not fetch data about location -- {locationName} ==== Error code {errorCode} -- {errorMessage} (HTTP response status code -- {statusCode})", locationName, errorCode, errorMessage, statusCode); // Logging 

                        // Return error response object
                        return new WeatherResponseObject
                        {
                            responseStatus = response.StatusCode
                        };
                    }

                }
            }

        }


        /// <summary>
        /// Updates weather request table (per location).
        /// </summary>
        /// <param name="locationName">Name of location requested</param>
        /// <param name="temperature">Current temperature (time of request)</param>
        /// <param name="currentTimestamp">Timestamp of requests</param>
        public async Task UpdateWeatherRequestTable(string locationName, double temperature, DateTime currentTimestamp)
        {
            // Fetch locations
            var locations = _weatherReqRepo.GetAll();

            // Check if location exists
            var currentLocationData = locations.Where(x => x.LocationName.ToLower().Equals(locationName.ToLower())).FirstOrDefault();

            // If yes, update object accordingly
            if (currentLocationData != null)
            {
                currentLocationData.TimesRequested++;
                currentLocationData.LastRequested = currentTimestamp;
                currentLocationData.LastRecordedTemperature = temperature;
                if (temperature < currentLocationData.MinRecordedTemperature)
                {
                    currentLocationData.MinRecordedTemperature = temperature;
                    Logger.LogInformation("New maximum temperature recorded for \"{location}\"! Temperature: {temp}", currentLocationData.LocationName, temperature);
                }
                if (temperature > currentLocationData.MaxRecordedTemperature)
                {
                    currentLocationData.MaxRecordedTemperature = temperature;
                    Logger.LogInformation("New minimum temperature recorded for \"{location}\"! Temperature: {temp}", currentLocationData.LocationName, temperature);
                }

                // Update via repo
                _weatherReqRepo.Update(currentLocationData);
                Logger.LogInformation("Weather request data for \"{location}\" updated. Times location requested: {timesRequested}", currentLocationData.LocationName, currentLocationData.TimesRequested);

            }
            else // If no, create new object
            {
                var newLocationData = new WeatherRequest
                {
                    LocationName = locationName,
                    LastRecordedTemperature = temperature,
                    MinRecordedTemperature = temperature,
                    MaxRecordedTemperature = temperature,
                    TimesRequested = 1,
                    LastRequested = currentTimestamp
                };

                // Create via repo
                await _weatherReqRepo.Create(newLocationData);
                Logger.LogInformation("Weather request data for \"{location}\" created. First request recorded at: {timestamp}", newLocationData.LocationName, newLocationData.LastRequested);

            }

        }

        /// <summary>
        /// Adds the weather request to the user's overall request summary.
        /// </summary>
        /// <param name="appUserId">The app user ID</param>
        /// <param name="currentTimestamp">The timestamp of the last weather request</param>
        /// <returns></returns>
        public async Task UpdateUserRequests(int appUserId, DateTime currentTimestamp)
        {
            // Fetch requests
            var requests = _userReqRepo.GetAll();

            // Check if user
            var currentUserData = requests.Where(x => x.AppUserId == appUserId).FirstOrDefault();

            // If yes, update object accordingly
            if (currentUserData != null)
            {
                currentUserData.WeatherRequests++;
                currentUserData.LastWeatherRequest = currentTimestamp;

                // Update via repo
                _userReqRepo.Update(currentUserData);
                Logger.LogInformation("User request data for user with ID \"{userId}\" updated. Total weather requests: {totalRequests}", currentUserData.AppUserId, currentUserData.WeatherRequests);

            }
            else // If no, create new object
            {
                var newUserData = new AppUserRequests
                {
                    WeatherRequests = 1,
                    LastWeatherRequest = currentTimestamp,
                    AppUserId = appUserId
                };

                // Create via repo
                await _userReqRepo.Create(newUserData);
                Logger.LogInformation("User request data for user with ID \"{userId}\" created. First weather request recorded at: {timestamp}", newUserData.AppUserId, newUserData.LastWeatherRequest);

            }

        }


        /// <summary>
        /// Retrieves the path for the correct weather icon, based on the weather report.
        /// </summary>
        /// <param name="originalPath">The path in the original response object</param>
        /// <returns>The weather icon path (GetInfo app)</returns>
        public string GetWeatherIconPath(string originalPath)
        {
            // Remove first two slashes from string
            string usefulPath = originalPath.Substring(2, originalPath.Length - 2);
            // Separate string, keep only 2 last elements (only last 2 elements useful)
            var substrings = usefulPath.Split('/');

            // Create and return new path for GetInfo app
            string newPath = "/Pictures/WeatherPics/BaseIcons/" + substrings[substrings.Length - 2] + "/" + substrings[substrings.Length - 1];
            return newPath;
        }


    }

}

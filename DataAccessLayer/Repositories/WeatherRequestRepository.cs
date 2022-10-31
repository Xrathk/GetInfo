using DataAccessLayer.Contacts;
using DataAccessLayer.Data;
using DomainLayer.Entities;
using Microsoft.Extensions.Logging;

namespace DataAccessLayer.Repositories
{
    /// <summary>
    /// Weather request repository class.
    /// </summary>
    public class RepositoryWeatherRequest : IRepositoryWeatherRequest
    {
        private readonly GetInfoDbContext dbContext;
        private readonly ILogger _logger;

        /// <summary>
        /// Weather request repository constructor.
        /// </summary>
        /// <param name="_dbContext">Database context</param>
        /// <param name="logger">Logger component</param>
        public RepositoryWeatherRequest(GetInfoDbContext _dbContext, ILogger<WeatherRequest> logger)
        {
            dbContext = _dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Add weather request to database.
        /// </summary>
        /// <param name="weatherrequest">New weather request to be added</param>
        /// <returns></returns>
        public async Task<WeatherRequest> Create(WeatherRequest weatherrequest)
        {
            try
            {
                if (weatherrequest != null)
                {
                    var obj = dbContext.Add(weatherrequest);
                    await dbContext.SaveChangesAsync();
                    _logger.LogInformation("Weather request data object initiated successfully:\n" +
                        "Location: {location}, Last recorded temperature: {lastTemp}", 
                        obj.Entity.LocationName, obj.Entity.LastRecordedTemperature);
                    return obj.Entity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception Ex) // Exception handling
            {
                _logger.LogError("Could not store new weather request to database.\n" +
                    "Exception: {Ex}", Ex);
                return null;
            }
        }

        /// <summary>
        /// Delete a weather request from the database.
        /// </summary>
        /// <param name="weatherrequest">Weather request to be deleted</param>
        public void Delete(WeatherRequest weatherrequest)
        {
            try
            {
                if (weatherrequest != null)
                {
                    var obj = dbContext.Remove(weatherrequest);
                    if (obj != null)
                    {
                        dbContext.SaveChangesAsync();
                        _logger.LogInformation("Weather request object deleted successfully from database:\n" +
                            "Request ID:{id}, Location: {location}",weatherrequest.Id, weatherrequest.LocationName);
                    }
                }
            }
            catch (Exception Ex) // Exception handling
            {
                _logger.LogError("Could not delete weather request data with from database.\n" +
                    "Weather Request ID: {objId}, Exception: {Ex}", weatherrequest.Id, Ex);
            }
        }

        /// <summary>
        /// Retrieves all weather requests from database.
        /// </summary>
        /// <returns>All weather requests.</returns>
        public IEnumerable<WeatherRequest> GetAll()
        {
            try
            {
                var obj = dbContext.WeatherRequests.ToList();
                if (obj != null) return obj;
                else return null;
            }
            catch (Exception Ex) // Exception handling
            {
                _logger.LogError("Could not retrieve weather requests from database.\n" +
                    "Exception: {Ex}", Ex);
                return null;
            }
        }

        /// <summary>
        /// Retrieves a weather request by its Id.
        /// </summary>
        /// <param name="Id">The weather request ID</param>
        /// <returns>The weather request whom the ID belongs to.</returns>
        public WeatherRequest GetById(int Id)
        {
            try
            {
                var Obj = dbContext.WeatherRequests.FirstOrDefault(x => x.Id == Id);
                if (Obj != null) return Obj;
                else return null;
            }
            catch (Exception Ex) // Exception handling
            {
                _logger.LogError("Could not retrieve weather request from database.\n" +
                    "Weather Request ID: {reqId}, Exception: {Ex}", Id, Ex);
                return null;
            }
        }

        /// <summary>
        /// Update weather request info.
        /// </summary>
        /// <param name="weatherrequest">Weather request to be updated</param>
        public void Update(WeatherRequest weatherrequest)
        {
            try
            {
                if (weatherrequest != null)
                {
                    var obj = dbContext.Update(weatherrequest);
                    if (obj != null)
                    {
                        dbContext.SaveChanges();
                        _logger.LogInformation("Weather request data object updated successfully:\n" +
                            "Location: {location}, Times requested: {timesRequested}, Last recorded temperature: {lastTemp}", 
                            obj.Entity.LocationName, obj.Entity.TimesRequested, obj.Entity.LastRecordedTemperature);
                    }
                }
            }
            catch (Exception Ex) // Exception handling
            {
                _logger.LogError("Could not update weather request info in database.\n" +
                    "Weather Request ID: {reqId}, Exception: {Ex}", weatherrequest.Id, Ex);
            }
        }
    }
}

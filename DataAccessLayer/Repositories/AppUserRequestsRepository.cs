using DataAccessLayer.Contacts;
using DataAccessLayer.Data;
using DomainLayer.Entities;
using Microsoft.Extensions.Logging;

namespace DataAccessLayer.Repositories
{
    /// <summary>
    /// App user requests repository class.
    /// </summary>
    public class RepositoryAppUserRequests : IRepositoryAppUserRequests
    {
        private readonly GetInfoDbContext dbContext;
        private readonly ILogger _logger;

        /// <summary>
        /// App user requests repository constructor.
        /// </summary>
        /// <param name="_dbContext">Database context</param>
        /// <param name="logger">Logger component</param>
        public RepositoryAppUserRequests(GetInfoDbContext _dbContext, ILogger<AppUserRequests> logger)
        {
            dbContext = _dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Add user requests object to database.
        /// </summary>
        /// <param name="appuserrequests">New user requests object to be added</param>
        /// <returns></returns>
        public async Task<AppUserRequests> Create(AppUserRequests appuserrequests)
        {
            try
            {
                if (appuserrequests != null)
                {
                    var obj = dbContext.Add(appuserrequests);
                    await dbContext.SaveChangesAsync();
                    _logger.LogInformation("Added new user requests object for user.\n" +
                        "UserId: {userId}", obj.Entity.AppUserId);
                    return obj.Entity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception Ex) // Exception handling
            {
                _logger.LogError("Could not store new user requests object to database.\n" +
                    "User ID: {userId}, Exception: {Ex}",appuserrequests.AppUserId, Ex);
                return null;
            }
        }

        /// <summary>
        /// Delete a user requests object from the database.
        /// </summary>
        /// <param name="appuserrequests">User requests object to be deleted</param>
        public void Delete(AppUserRequests appuserrequests)
        {
            try
            {
                if (appuserrequests != null)
                {
                    var obj = dbContext.Remove(appuserrequests);
                    if (obj != null)
                    {
                        dbContext.SaveChangesAsync();
                        _logger.LogInformation("Deleted user requests object from database.\n" +
                            "Requests object ID: {objId}", obj.Entity.AppUserId);
                    }
                }
            }
            catch (Exception Ex) // Exception handling
            {
                _logger.LogError("Could not delete user requests object from database.\n" +
                    "Requests object ID: {objId} Exception: {Ex}", appuserrequests.Id, Ex);
            }
        }

        /// <summary>
        /// Retrieves all user requests object from database.
        /// </summary>
        /// <returns>All users.</returns>
        public IEnumerable<AppUserRequests> GetAll()
        {
            try
            {
                var obj = dbContext.AppUserRequestsOverview.ToList();
                if (obj != null) return obj;
                else return null;
            }
            catch (Exception Ex) // Exception handling
            {
                _logger.LogError("Could not retrieve users requests overview from database.\n" +
                    "Exception: {Ex}", Ex);
                return null;
            }
        }

        /// <summary>
        /// Retrieves a user's requests by his Id.
        /// </summary>
        /// <param name="Id">The user ID</param>
        /// <returns>The user requests object whom the ID belongs to.</returns>
        public AppUserRequests GetById(int Id)
        {
            try
            {
                var Obj = dbContext.AppUserRequestsOverview.FirstOrDefault(x => x.Id == Id);
                if (Obj != null) return Obj;
                else return null;
            }
            catch (Exception Ex) // Exception handling
            {
                _logger.LogError("Could not retrieve user requests object from database.\n" +
                    "User Requests object ID: {objId}, Exception: {Ex}", Id, Ex);
                return null;
            }
        }

        /// <summary>
        /// Update user requests object info.
        /// </summary>
        /// <param name="appuserrequests">User requests object to be updated</param>
        public void Update(AppUserRequests appuserrequests)
        {
            try
            {
                if (appuserrequests != null)
                {
                    var obj = dbContext.Update(appuserrequests);
                    if (obj != null)
                    {
                        dbContext.SaveChanges();
                        _logger.LogInformation("Updated user requests object .\n" +
                            "User Requests object ID: {objId}", appuserrequests.AppUserId);
                    }
                }
            }
            catch (Exception Ex) // Exception handling
            {
                _logger.LogError("Could not update info for user requests object in database.\n" +
                    "User Requests object ID: {objId}, Exception: {Ex}", appuserrequests.Id, Ex);
            }
        }
    }
}

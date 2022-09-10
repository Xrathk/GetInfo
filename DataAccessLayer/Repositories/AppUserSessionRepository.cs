using DataAccessLayer.Contacts;
using DataAccessLayer.Data;
using DomainLayer.Entities;
using Microsoft.Extensions.Logging;

namespace DataAccessLayer.Repositories
{
    /// <summary>
    /// User sessions repository class.
    /// </summary>
    public class RepositoryAppUserSession : IRepositoryAppUserSession
    {
        private readonly GetInfoDbContext dbContext;
        private readonly ILogger _logger;

        /// <summary>
        /// Session repository constructor.
        /// </summary>
        /// <param name="_dbContext">Database context</param>
        /// <param name="logger">Logger component</param>
        public RepositoryAppUserSession(GetInfoDbContext _dbContext, ILogger<AppUserSession> logger)
        {
            dbContext = _dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Add session to database.
        /// </summary>
        /// <param name="AppUserSession">New session to be added</param>
        /// <returns></returns>
        public async Task<AppUserSession> Create(AppUserSession AppUserSession)
        {
            try
            {
                if (AppUserSession != null)
                {
                    var obj = dbContext.Add(AppUserSession);
                    await dbContext.SaveChangesAsync();
                    _logger.LogInformation("New session initialized.\n User ID: {}, Session ID: {sessId}", obj.Entity.AppUserID, obj.Entity.SessionId);
                    return obj.Entity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception Ex) // Exception handling
            {
                _logger.LogInformation("Could not store new session for user with ID \"{userId}\" to database. Exception: {Ex}", AppUserSession.AppUserID, Ex);
                return null;
            }
        }

        /// <summary>
        /// Delete a session from the database.
        /// </summary>
        /// <param name="AppUserSession">Session to be deleted</param>
        public void Delete(AppUserSession AppUserSession)
        {
            try
            {
                if (AppUserSession != null)
                {
                    var obj = dbContext.Remove(AppUserSession);
                    if (obj != null)
                    {
                        dbContext.SaveChangesAsync();
                        _logger.LogInformation("Session info deleted.\n User ID: {}, Session ID: {sessId}", obj.Entity.AppUserID, obj.Entity.SessionId);
                    }
                }
            }
            catch (Exception Ex) // Exception handling
            {
                _logger.LogInformation("Could not delete session info for user with ID \"{userId}\" to database. Exception: {Ex}", AppUserSession.AppUserID, Ex);
            }
        }

        /// <summary>
        /// Retrieves all sessions from database.
        /// </summary>
        /// <returns>All sessions.</returns>
        public IEnumerable<AppUserSession> GetAll()
        {
            try
            {
                var obj = dbContext.AppUserSessions.ToList();
                if (obj != null) return obj;
                else return null;
            }
            catch (Exception Ex) // Exception handling
            {
                _logger.LogInformation("Could not retrieve sessions from database. Exception: {Ex}", Ex);
                return null;
            }
        }

        /// <summary>
        /// Retrieves a session by its Id.
        /// </summary>
        /// <param name="Id">The session ID</param>
        /// <returns>Session info.</returns>
        public AppUserSession GetById(int Id)
        {
            try
            {
                var Obj = dbContext.AppUserSessions.FirstOrDefault(x => x.Id == Id);
                if (Obj != null) return Obj;
                else return null;
            }
            catch (Exception Ex) // Exception handling
            {
                _logger.LogInformation("Could not retrieve info for session with ID \"{sessId}\" from database. Exception: {Ex}", Id, Ex);
                return null;
            }
        }

        /// <summary>
        /// Retrieves a session info by a user's username.
        /// </summary>
        /// <param name="username">The user's username</param>
        /// <returns>The session info to whom the username belongs to.</returns>
        public AppUserSession GetByUsername(string username)
        {
            try
            {
                var Obj = dbContext.AppUserSessions.FirstOrDefault(x => x.UserName.Equals(username));
                if (Obj != null) return Obj;
                else return null;
            }
            catch (Exception Ex) // Exception handling
            {
                _logger.LogInformation("Could not retrieve session info for user with username \"{userName}\" from database. Exception: {Ex}", username, Ex);
                return null;
            }
        }

        /// <summary>
        /// Retrieves a session info by a session ID.
        /// </summary>
        /// <param name="sessionId">The session Id.</param>
        /// <returns>The session info to whom the session ID belongs to.</returns>
        public AppUserSession GetBySessionId(string sessionId)
        {
            try
            {
                var Obj = dbContext.AppUserSessions.FirstOrDefault(x => x.SessionId.Equals(sessionId) && x.SessionActive == true);
                if (Obj != null) return Obj;
                else return null;
            }
            catch (Exception Ex) // Exception handling
            {
                _logger.LogInformation("Could not retrieve session info for session ID \"{sessId}\" from database. Exception: {Ex}", sessionId, Ex);
                return null;
            }
        }

        /// <summary>
        /// Update session info.
        /// </summary>
        /// <param name="AppUserSession">Session to be updated</param>
        public void Update(AppUserSession AppUserSession)
        {
            try
            {
                if (AppUserSession != null)
                {
                    var obj = dbContext.Update(AppUserSession);
                    if (obj != null)
                    {
                        dbContext.SaveChanges();
                        _logger.LogInformation("Session info updated.\n User ID: {userId}, Session ID: {sessId}, Session active: {sessStatus}", obj.Entity.AppUserID, obj.Entity.SessionId, obj.Entity.SessionActive);
                    }
                }
            }
            catch (Exception Ex) // Exception handling
            {
                _logger.LogInformation("Could not update session info for user with ID \"{userId}\". Exception: {Ex}", AppUserSession.AppUserID, Ex);
            }
        }
    }
}

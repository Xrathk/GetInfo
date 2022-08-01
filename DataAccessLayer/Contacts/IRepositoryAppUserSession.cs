using DataAccessLayer.Models.Entities;

namespace DataAccessLayer.Contacts
{
    /// <summary>
    /// Interface with methods for database interaction for the app user sessions model.
    /// </summary>
    public interface IRepositoryAppUserSession : IRepository<AppUserSession>
    {
        /// <summary>
        /// Retrieves a session by the user's username.
        /// </summary>
        /// <param name="username">Username parameter</param>
        /// <returns></returns>
        public AppUserSession GetByUsername(string username); 

        /// <summary>
        /// Retrieves a session by its session Id.
        /// </summary>
        /// <param name="sessionId">Session Id parameter</param>
        /// <returns></returns>
        public AppUserSession GetBySessionId(string sessionId); 
    }
}

using DataAccessLayer.Models.Entities;

namespace DataAccessLayer.Contacts
{
    /// <summary>
    /// Interface with methods for database interaction for the app user sessions model.
    /// </summary>
    public interface IRepositoryAppUserSession : IRepository<AppUserSession>
    {
        public AppUserSession GetByUsername(string username); // Retrieve user session by username

        public AppUserSession GetBySessionId(string sessionId); // Retrieve user session by session ID
    }
}

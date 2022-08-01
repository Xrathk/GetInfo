using DataAccessLayer.Models.Entities;

namespace DataAccessLayer.Contacts
{
    /// <summary>
    /// Interface with methods for database interaction for the app user model.
    /// </summary>
    public interface IRepositoryAppUser : IRepository<AppUser>
    {
        /// <summary>
        /// Retrieves a user by his username.
        /// </summary>
        /// <param name="username">Username parameter</param>
        /// <returns></returns>
        public AppUser GetByUsername(string username); 
    }
}

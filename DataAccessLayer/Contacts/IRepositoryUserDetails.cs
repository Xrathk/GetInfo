using DataAccessLayer.Models.Entities;

namespace DataAccessLayer.Contacts
{
    /// <summary>
    /// Interface with methods for database interaction for the user details model.
    /// </summary>
    public interface IRepositoryUserDetails : IRepository<UserDetails>
    {
        /// <summary>
        /// Retrieves user details by the user's Id.
        /// </summary>
        /// <param name="appUserId">User Id parameter</param>
        /// <returns></returns>
        public UserDetails GetByUserId(int appUserId); 
    }
}

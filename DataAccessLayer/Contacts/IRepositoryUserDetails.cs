using DataAccessLayer.Models.Entities;

namespace DataAccessLayer.Contacts
{
    /// <summary>
    /// Interface with methods for database interaction for the user details model.
    /// </summary>
    public interface IRepositoryUserDetails : IRepository<UserDetails>
    {
        public UserDetails GetByUserId(int appUserId); // Retrieve details by user Id
    }
}

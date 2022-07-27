using DataAccessLayer.Models.Entities;

namespace DataAccessLayer.Contacts
{
    /// <summary>
    /// Interface with methods for database interaction for the app user model.
    /// </summary>
    public interface IRepositoryAppUser : IRepository<AppUser>
    {
        public AppUser GetByUsername(string username); // Retrieve user by username
    }
}

using DataAccessLayer.Contacts;
using DataAccessLayer.Data;
using DomainLayer.Entities;
using Microsoft.Extensions.Logging;

namespace DataAccessLayer.Repositories
{
    /// <summary>
    /// App user repository class.
    /// </summary>
    public class RepositoryAppUser : IRepositoryAppUser
    {
        private readonly GetInfoDbContext dbContext;
        private readonly ILogger _logger;

        /// <summary>
        /// User repository constructor.
        /// </summary>
        /// <param name="_dbContext">Database context</param>
        /// <param name="logger">Logger component</param>
        public RepositoryAppUser(GetInfoDbContext _dbContext, ILogger<AppUser> logger)
        {
            dbContext = _dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Add user to database.
        /// </summary>
        /// <param name="appuser">New user to be added</param>
        /// <returns></returns>
        public async Task<AppUser> Create(AppUser appuser)
        {
            try
            {
                if (appuser != null) 
                {
                    var obj = dbContext.Add(appuser);
                    await dbContext.SaveChangesAsync();
                    _logger.LogInformation("User added to database:\n" +
                        "User Id: {id}, Username: {userName}, Email: {eMail}", 
                        obj.Entity.Id, obj.Entity.UserName, obj.Entity.EMail);
                    return obj.Entity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception Ex) // Exception handling
            {
                _logger.LogError("Could not store new user to database.\n" +
                    "Exception: {Ex}", Ex);
                return null;
            }
        }

        /// <summary>
        /// Delete a user from the database.
        /// </summary>
        /// <param name="appuser">User to be deleted</param>
        public void Delete(AppUser appuser)
        {
            try
            {
                if (appuser != null)
                {
                    var obj = dbContext.Remove(appuser);
                    if (obj != null)
                    {
                        dbContext.SaveChangesAsync();
                        _logger.LogInformation("User deleted from database successfully.\n" +
                            "User Id: {id}, Username: {userName}, Email: {eMail}", 
                            obj.Entity.Id, obj.Entity.UserName, obj.Entity.EMail);
                    }
                }
            }
            catch (Exception Ex) // Exception handling
            {
                _logger.LogError("Could not delete user from database.\n" +
                    "User ID: {userId}, Exception: {Ex}", appuser.Id, Ex);
            }
        }

        /// <summary>
        /// Retrieves all users from database.
        /// </summary>
        /// <returns>All users.</returns>
        public IEnumerable<AppUser> GetAll()
        {
            try
            {
                var obj = dbContext.AppUsers.ToList();
                if (obj != null) return obj;
                else return null;
            }
            catch (Exception Ex) // Exception handling
            {
                _logger.LogError("Could not retrieve users from the database.\n" +
                    "Exception: {Ex}", Ex);
                return null;
            }
        }

        /// <summary>
        /// Retrieves a user by his Id.
        /// </summary>
        /// <param name="Id">The user ID</param>
        /// <returns>The user whom the ID belongs to.</returns>
        public AppUser GetById(int Id)
        {
            try
            {
                var Obj = dbContext.AppUsers.FirstOrDefault(x => x.Id == Id);
                if (Obj != null) return Obj;
                else return null;
            }
            catch (Exception Ex) // Exception handling
            {
                _logger.LogError("Could not retrieve user from database.\n" +
                    "User ID: {userId}, Exception: {Ex}", Id, Ex);
                return null;
            }
        }

        /// <summary>
        /// Retrieves a user by his username.
        /// </summary>
        /// <param name="username">The user's username</param>
        /// <returns>The user whom the username belongs to.</returns>
        public AppUser GetByUsername(string username)
        {
            try
            {
                var Obj = dbContext.AppUsers.FirstOrDefault(x => x.UserName.Equals(username));
                if (Obj != null) return Obj;
                else return null;
            }
            catch (Exception Ex) // Exception handling
            {
                _logger.LogError("Could not retrieve user from database.\n" +
                    "Username: {username}, Exception: {Ex}", username, Ex);
                return null;
            }
        }

        /// <summary>
        /// Update user info.
        /// </summary>
        /// <param name="appuser">User to be updated</param>
        public void Update(AppUser appuser)
        {
            try
            {
                if (appuser != null)
                {
                    var obj = dbContext.Update(appuser);
                    if (obj != null)
                    {
                        dbContext.SaveChanges();
                        _logger.LogInformation("User info updated successfully.\n" +
                            "User Id: {id}, Username: {userName}, Email: {eMail}", obj.Entity.Id, obj.Entity.UserName, obj.Entity.EMail);
                    }
                }
            }
            catch (Exception Ex) // Exception handling
            {
                _logger.LogError("Could not update info for user in the database.\n" +
                    "User ID: {userId}, Exception: {Ex}", appuser.Id, Ex);
            }
        }
    }
}

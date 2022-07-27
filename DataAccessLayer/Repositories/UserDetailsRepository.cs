using DataAccessLayer.Contacts;
using DataAccessLayer.Data;
using DataAccessLayer.Models.Entities;
using Microsoft.Extensions.Logging;

namespace DataAccessLayer.Repositories
{
    /// <summary>
    /// User details repository method.
    /// </summary>
    public class RepositoryUserDetails : IRepositoryUserDetails
    {
        private readonly GetInfoDbContext dbContext;
        private readonly ILogger _logger;

        // Constructor
        public RepositoryUserDetails(GetInfoDbContext _dbContext, ILogger<UserDetails> logger)
        {
            dbContext = _dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Add user details to database.
        /// </summary>
        /// <param name="UserDetails">New user details to be added</param>
        /// <returns></returns>
        public async Task<UserDetails> Create(UserDetails UserDetails)
        {
            try
            {
                if (UserDetails != null)
                {
                    var obj = dbContext.Add<UserDetails>(UserDetails);
                    await dbContext.SaveChangesAsync();
                    _logger.LogInformation("User details with ID \"{userId}\" successfully stored to database.", obj.Entity.Id);
                    return obj.Entity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception Ex) // Exception handling
            {
                _logger.LogInformation("Could not store new user details with ID \"{detId}\" to database. Exception: {Ex}", UserDetails.Id, Ex);
                return null;
            }
        }

        /// <summary>
        /// Delete user details from the database.
        /// </summary>
        /// <param name="UserDetails">User details to be deleted</param>
        public void Delete(UserDetails UserDetails)
        {
            try
            {
                if (UserDetails != null)
                {
                    var obj = dbContext.Remove(UserDetails);
                    if (obj != null)
                    {
                        dbContext.SaveChangesAsync();
                        _logger.LogInformation("User details with ID \"{userId}\" has been deleted from the database succesfully.", obj.Entity.Id);
                    }
                }
            }
            catch (Exception Ex) // Exception handling
            {
                _logger.LogInformation("Could not delete user details with ID \"{userId}\" from database. Exception: {Ex}", UserDetails.Id, Ex);
            }
        }

        /// <summary>
        /// Retrieves all users details from database.
        /// </summary>
        /// <returns>All users details.</returns>
        public IEnumerable<UserDetails> GetAll()
        {
            try
            {
                var obj = dbContext.UserDetails.ToList();
                if (obj != null) return obj;
                else return null;
            }
            catch (Exception Ex) // Exception handling
            {
                _logger.LogInformation("Could not retrieve all user details from database. Exception: {Ex}", Ex);
                return null;
            }
        }

        /// <summary>
        /// Retrieves a user's details be user detail ID.
        /// </summary>
        /// <param name="Id">The user details ID</param>
        /// <returns>The details for the user with this ID.</returns>
        public UserDetails GetById(int Id)
        {
            try
            {
                var Obj = dbContext.UserDetails.FirstOrDefault(x => x.Id == Id);
                if (Obj != null) return Obj;
                else return null;
            }
            catch (Exception Ex) // Exception handling
            {
                _logger.LogInformation("Could not retrieve details with ID \"{detId}\" from database. Exception: {Ex}", Id, Ex);
                return null;
            }
        }

        /// <summary>
        /// Retrieves a user by his user Id.
        /// </summary>
        /// <param name="appUserId">The user's id</param>
        /// <returns>The user details to whom the user ID belongs to.</returns>
        public UserDetails GetByUserId(int appUserId)
        {
            try
            {
                var Obj = dbContext.UserDetails.FirstOrDefault(x => x.AppUserId == appUserId);
                if (Obj != null) return Obj;
                else return null;
            }
            catch (Exception Ex) // Exception handling
            {
                _logger.LogInformation("Could not retrieve user details for user with ID \"{appUserId}\" from database. Exception: {Ex}", appUserId, Ex);
                return null;
            }
        }

        /// <summary>
        /// Update user details.
        /// </summary>
        /// <param name="UserDetails">User details to be updated</param>
        public void Update(UserDetails UserDetails)
        {
            try
            {
                if (UserDetails != null)
                {
                    var obj = dbContext.Update(UserDetails);
                    if (obj != null)
                    {
                        dbContext.SaveChanges();
                        _logger.LogInformation("User details with ID \"{userId}\" has been updated succesfully.", obj.Entity.Id);
                    }
                }
            }
            catch (Exception Ex) // Exception handling
            {
                _logger.LogInformation("Could not update info for user details with ID \"{detId}\". Exception: {Ex}", UserDetails.Id, Ex);
            }
        }
    }
}

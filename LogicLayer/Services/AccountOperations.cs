using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DomainLayer.Entities;
using LogicLayer.Data.Forms;
using DataAccessLayer.Contacts;

namespace LogicLayer.Services
{
    
    /// <summary>
    /// Class that handles account operations logic in GetInfo.
    /// </summary>
    public class AccountOperations
    {
        // Fields
        private readonly IServiceScopeFactory scopeFactory;
        private readonly ILogger<AccountOperations> Logger;
        // DAL repos
        public readonly IRepositoryAppUser _userRepo;
        public readonly IRepositoryAppUserSession _sessionRepo;
        public readonly IRepositoryUserDetails _userDetailsRepo;

        /// <summary>
        /// Service constuctor.
        /// </summary>
        /// <param name="_scopeFactory">Score factory</param>
        /// <param name="logger">Logger component</param>
        /// <param name="userRepo">User model repository</param>
        /// <param name="sessionRepo">Session model repository</param>
        /// <param name="userDetailsRepo">User details model repository</param>
        public AccountOperations(
            IServiceScopeFactory _scopeFactory, 
            ILogger<AccountOperations> logger, 
            IRepositoryAppUser userRepo,
            IRepositoryAppUserSession sessionRepo,
            IRepositoryUserDetails userDetailsRepo
        )
        {
            scopeFactory = _scopeFactory;
            Logger = logger;
            _userRepo = userRepo;
            _sessionRepo = sessionRepo;
            _userDetailsRepo = userDetailsRepo;

        }


        /// <summary>
        /// This method handles the registration of new accounts.
        /// </summary>
        /// <param name="userForm">The user form data from the registration page.</param>
        /// <returns> A return code for the status of the registration form: </returns>
        /// 0: Successful account registration
        /// 1: Error - Username already exists
        /// 2: Error - Email already registered
        public int RegisterAccount(RegisterForm userForm)
        {
            // Import all accounts
            var accounts = _userRepo.GetAll();

            // Check if email and username already exist - return error codes if yes
            if (accounts.Where(x => x.EMail.Equals(userForm.Email)).Count() > 0)
            {
                return 2;
            }
            if (accounts.Where(x => x.UserName.Equals(userForm.Username)).Count() > 0)
            {
                return 1;
            }

            // Hash password using BCrypt algorithm before storing to database.
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userForm.Password);
            // All checks passed - create user object
            var newUser = new AppUser{
                UserName = userForm.Username,
                Password = hashedPassword,   
                EMail = userForm.Email
            };

            // Store user to database
            var createdUser = _userRepo.Create(newUser); // Synchronously so ID can be fetched for logging
            Logger.LogInformation("A new user has registered. Username: {userName}, Email: {eMail}, User ID: {appUserId}", newUser.UserName, newUser.EMail, createdUser.Id);

            return 0; 
        }

        /// <summary>
        /// This method handles logon operations of users to the app.
        /// </summary>
        /// <param name="userForm">The user form data from the login page.</param>
        /// <returns> A return code for the status of the login form: </returns>
        /// 0: Successful account login
        /// 3: Error - Wrong credentials
        /// 6: Error - Exception
        public int LoginAccount(LoginForm userForm)
        {
            // Fetch user
            var user = _userRepo.GetByUsername(userForm.Username);

            // Check if user exists
            if (user != null)
            {
                try
                {
                    if (BCrypt.Net.BCrypt.Verify(userForm.Password, user.Password)) // Verify password hash, login if verification successful
                    {
                        Logger.LogInformation("User with ID {appUserId} has successfully logged in.", user.Id);
                        return 0;
                    }
                    else // If password incorrect, return appropriate error code
                    {
                        return 3;
                    }
                }
                catch (Exception Ex) // Log exception and generic error message for user
                {
                    Logger.LogWarning("Error during login operation for user with ID {appUserId} -- Exception: {Ex}", user.Id, Ex);
                    return 6;
                }

            }
            else // If username incorrect, return appropriate error code
            {
                return 3;
            }

        }

        /// <summary>
        /// Initializes a new user session by granting user client an authentication cookie after a successful login.
        /// Valid cookies are required to login to GetWeather.
        /// </summary>
        /// <param name="username">The client's username.</param>
        /// <returns>A return code for the status of the session initialization.</returns>
        /// 0: SessionId stored successfully.
        /// 4: Error - Cannot initiate session - user is banned. (WILL ADD FUNCTIONALITY LATER)
        public int InitializeNewSession(string username)
        {
            // Create new authentication token for user -- SHA256 hash of username concatenated with a random number from 10 to 20 million.
            Random rnd = new Random();
            var toBeHashedString = username + rnd.Next(10000000,20000000).ToString();
            string userCookie = "";
            using (var sha256 = SHA256.Create())
            {
                userCookie = BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(toBeHashedString))).Replace("-", "");
            }

            // Fetch userId from username
            var appUserId = _userRepo.GetByUsername(username).Id;
            // Fetch user row from session table
            var userSessionData = _sessionRepo.GetByUsername(username);

            // If row doesn't exist, create new row for user. 
            if (userSessionData == null)
            {
                var newSession = new AppUserSession
                {
                    UserName = username,
                    SessionId = userCookie,
                    SessionActive = true,
                    AppUserID = appUserId
                };
                _sessionRepo.Create(newSession);
            }
            else // Else, update cookie
            {
                userSessionData.SessionId = userCookie;
                userSessionData.SessionActive = true;
                _sessionRepo.Update(userSessionData);
            }

            // Return
            return 0;

        }

        /// <summary>
        /// Retrieves the user's authentication cookie (called after session initialization)
        /// </summary>
        /// <param name="username">The client's username.</param>
        /// <returns>The authentication cookie.</returns>
        public string RetrieveAuthCookie(string username)
        {
            return _sessionRepo.GetByUsername(username).SessionId;
        }

        /// <summary>
        /// Verifies a session Id of a user. 
        /// This check is done in all pages that aren't the login or register pages.
        /// Valid cookies are required to login to GetWeather.
        /// </summary>
        /// <param name="ProtectedSessionStore">The client's session storage.</param>
        /// <returns>A return code for the validity of the cookie.</returns>
        /// 0: Cookie valid.
        /// 5: Error - Cookie not valid (wrong or inactive).
        public async Task<int> VerifyAuthenticationCookie(ProtectedSessionStorage ProtectedSessionStore)
        {
            if ((await ProtectedSessionStore.GetAsync<string>("SessionId")).Success) // Check if there is a cookie
            {
                // Initial cookie check
                var authenticationCookie = (await ProtectedSessionStore.GetAsync<string>("SessionId")).Value;
                var correspondingSession = _sessionRepo.GetBySessionId(authenticationCookie);

                // If active session was found, allow access to app and fetch corresponding user data (WILL BE IMPLEMENTED LATER)
                if (correspondingSession != null)
                {
                    return 0;
                }
                else // Else, forbid access - user must login his credentials again
                {
                    return 5;
                }
            }
            else // No cookie found, back to login page
            {
                return 5;
            }

        }

        /// <summary>
        /// Retrieves a user's account details (if they exist), after a request.
        /// </summary>
        /// <param name="AppUserId">The user's ID</param>
        /// <returns>The user's details.</returns>
        public async Task<UserDetails> RetrieveUserDetails(int AppUserId)
        {
            // Fetch user details
            var userDetails = _userDetailsRepo.GetByUserId(AppUserId);
            if (userDetails != null)
            {
                return userDetails;
            }
            else
            {
                return new UserDetails();
            }
        }

        /// <summary>
        /// Finds a user by his session ID, after a request.
        /// </summary>
        /// <param name="sessionId">The user's session ID</param>
        /// <returns>The user ID.</returns>
        public async Task<int> FetchUserBySessionId(string sessionId)
        {
            var userSessionData = _sessionRepo.GetBySessionId(sessionId);
            if (userSessionData != null)
            {
                return userSessionData.AppUserID;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Updates a user's account details.
        /// </summary>
        /// <param name="accDetails">Form account details</param>
        /// <param name="AppUserId">User's ID</param>
        /// <returns>True if storing was successful, false if not.</returns>
        public async Task<bool> StoreUserDetails(EditDetailsForm accDetails, int AppUserId)
        {
            // Fetch current details
            var currentDetails = _userDetailsRepo.GetByUserId(AppUserId);
            
            // If current details don't exist, create new object
            if (currentDetails == null)
            {
                var newDetails = new UserDetails
                {
                    FirstName = accDetails.FirstName,
                    LastName = accDetails.LastName,
                    Age = accDetails.Age,
                    CurrentLocation = accDetails.CurrentLocation,
                    CountryOfOrigin = accDetails.CountryOfOrigin,
                    AppUserId = AppUserId
                };
                await _userDetailsRepo.Create(newDetails);
            }
            else
            {
                // If current details exist, update
                currentDetails.FirstName = accDetails.FirstName; 
                currentDetails.LastName = accDetails.LastName;
                currentDetails.Age = accDetails.Age;
                currentDetails.CurrentLocation = accDetails.CurrentLocation;
                currentDetails.CountryOfOrigin = accDetails.CountryOfOrigin;
                _userDetailsRepo.Update(currentDetails);
            }

            Logger.LogInformation("User with ID {appUserId} has modified their account details.", AppUserId); ;
            return true; // All information is accepted, for the moment
        }


    }
    
}

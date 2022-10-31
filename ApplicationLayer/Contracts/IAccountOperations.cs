using DomainLayer.Entities;
using ApplicationLayer.Data.Forms;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace ApplicationLayer.Contracts
{
    /// <summary>
    /// Interface for the account operations service.
    /// </summary>
    public interface IAccountOperations
    {
        /// <summary>
        /// Finds a user by his session ID, after a request.
        /// </summary>
        /// <param name="sessionId">The user's session ID</param>
        /// <returns>The user ID.</returns>
        int FetchUserBySessionId(string sessionId);

        /// <summary>
        /// Initializes a new user session by granting user client an authentication cookie after a successful login.
        /// </summary>
        /// <param name="username">The client's username.</param>
        /// <returns>A return code for the status of the session initialization.</returns>
        int InitializeNewSession(string username);

        /// <summary>
        /// This method handles logon operations of users to the app.
        /// </summary>
        /// <param name="userForm">The user form data from the login page.</param>
        /// <returns> A return code for the status of the login form. </returns>
        int LoginAccount(LoginForm userForm);

        /// <summary>
        /// This method handles the registration of new accounts.
        /// </summary>
        /// <param name="userForm">The user form data from the registration page.</param>
        /// <returns> A return code for the status of the registration form. </returns>
        int RegisterAccount(RegisterForm userForm);

        /// <summary>
        /// Retrieves the user's authentication cookie (called after session initialization)
        /// </summary>
        /// <param name="username">The client's username.</param>
        /// <returns>The authentication cookie.</returns>
        string RetrieveAuthCookie(string username);

        /// <summary>
        /// Retrieves a user's account details (if they exist), after a request.
        /// </summary>
        /// <param name="AppUserId">The user's ID</param>
        /// <returns>The user's details.</returns>
        UserDetails RetrieveUserDetails(int AppUserId);

        /// <summary>
        /// Updates a user's account details.
        /// </summary>
        /// <param name="accDetails">Form account details</param>
        /// <param name="AppUserId">User's ID</param>
        /// <returns>True if storing was successful, false if not.</returns>
        Task<bool> StoreUserDetails(EditDetailsForm accDetails, int AppUserId);

        /// <summary>
        /// Verifies a session Id of a user. 
        /// This check is done in all pages that aren't the login or register pages.
        /// </summary>
        /// <param name="ProtectedSessionStore">The client's session storage.</param>
        /// <returns>A return code for the validity of the cookie.</returns>
        Task<int> VerifyAuthenticationCookie(ProtectedSessionStorage ProtectedSessionStore);
    }
}
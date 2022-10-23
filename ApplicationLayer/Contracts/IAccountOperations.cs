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
        Task<int> FetchUserBySessionId(string sessionId);
        int InitializeNewSession(string username);
        int LoginAccount(LoginForm userForm);
        int RegisterAccount(RegisterForm userForm);
        string RetrieveAuthCookie(string username);
        Task<UserDetails> RetrieveUserDetails(int AppUserId);
        Task<bool> StoreUserDetails(EditDetailsForm accDetails, int AppUserId);
        Task<int> VerifyAuthenticationCookie(ProtectedSessionStorage ProtectedSessionStore);
    }
}
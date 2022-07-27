namespace DataAccessLayer.Models.Entities
{
    /// <summary>
    /// GetInfo app user session data. Includes sessionId for verification purposes and other session metadata.
    /// </summary>
    public class AppUserSession
    {
        public int Id { get; set; } // DB Id

        public string UserName { get; set; } // Username

        public string SessionId { get; set; } // Session ID for this user

        public bool SessionActive { get; set; } // Keeps data on whether a user is still signed in or logged out

        public int AppUserID{ get; set; } // UserID-ForeignKey

        // Relationships to other tables
        public AppUser AppUser { get; set; } // User 


    }
}

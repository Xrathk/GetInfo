using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Entities
{
    /// <summary>
    /// GetInfo app user credentials. Used for authentication process.
    /// </summary>
    public class AppUser
    {
        public int Id { get; set; } // DB Id

        [MinLength(3)]
        public string UserName { get; set; } // Username

        [MinLength(6)]
        public string Password { get; set; } // Password

        public string EMail { get; set; } // E-mail

        // Relationships to other tables
        //public AppUserSession UserSession { get; set; } // Session info
        public UserDetails UserDetails { get; set; } // User details

    }
}

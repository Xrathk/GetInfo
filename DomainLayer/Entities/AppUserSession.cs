using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Entities
{
    /// <summary>
    /// GetInfo app user session data. Includes sessionId for verification purposes and other session metadata.
    /// </summary>
    public class AppUserSession
    {
        public int Id { get; set; } // DB Id

        [Required]
        [MinLength(3)]
        [MaxLength(32)]
        public string UserName { get; set; } // Username

        [MaxLength(128)]
        [Column(TypeName = "varchar(128)")]
        public string SessionId { get; set; } // Session ID for this user

        public bool SessionActive { get; set; } // Keeps data on whether a user is still signed in or logged out

        // Foreign Keys - Nav properties
        [Required]
        public int AppUserID{ get; set; } // UserID-ForeignKey
        public AppUser AppUser { get; set; } // User 


    }
}

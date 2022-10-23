using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Entities
{
    /// <summary>
    /// GetInfo app user credentials. Used for authentication process.
    /// </summary>
    public class AppUser
    {
        public int Id { get; set; } // DB Id

        [Required]
        [MinLength(3)]
        [MaxLength(32)]
        public string UserName { get; set; } // Username

        [Required]
        [MinLength(6)]
        [MaxLength(256)]
        public string Password { get; set; } // Password (salted and hashed)

        [Required]
        [MaxLength(128)]
        [Column(TypeName = "varchar(128)")]
        public string EMail { get; set; } // E-mail

        // Navigation properties
        public AppUserSession AppUserSession { get; set; }
        public AppUserRequests AppUserRequests { get; set; }
        public UserDetails UserDetails { get; set; }

    }
}

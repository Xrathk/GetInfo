using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Entities
{
    /// <summary>
    /// Individual user account details.
    /// </summary>
    public class UserDetails
    {
        public int Id { get; set; } // DB Id

        [MinLength(1)]
        [MaxLength(64)]
        public string FirstName { get; set; } // User's first name

        [MinLength(1)]
        [MaxLength(128)]
        public string LastName { get; set; } // User's last name

        [Range(12, 160)]
        public int Age { get; set; } // User age

        [MaxLength(64)]
        public string CountryOfOrigin { get; set; } // User's country

        [MaxLength(256)]
        public string CurrentLocation { get; set; } // User's current location


        // Foreign Keys - Nav properties
        [Required]
        public int AppUserId { get; set; } // UserID-ForeignKey
        public AppUser AppUser { get; set; } // User 

    }
}

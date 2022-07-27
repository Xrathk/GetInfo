using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.Entities
{
    /// <summary>
    /// Individual user account details.
    /// </summary>
    public class UserDetails
    {
        public int Id { get; set; } // DB Id

        [MinLength(1)]
        public string FirstName { get; set; } // User's first name

        [MinLength(1)]
        public string LastName { get; set; } // User's last name

        [Range(12, 160)]
        public int Age { get; set; } // User age

        public string CountryOfOrigin { get; set; } // User's country

        public string CurrentLocation { get; set; } // User's current location

        public int AppUserId { get; set; } // UserID-ForeignKey

        // Relationships to other tables
        public AppUser AppUser { get; set; } // User 

    }
}

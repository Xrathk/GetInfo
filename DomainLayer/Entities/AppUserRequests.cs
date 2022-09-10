using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Entities
{
    /// <summary>
    /// API requests per category, per user.
    /// </summary>
    public class AppUserRequests
    {
        public int Id; // DB Id

        [Display(Name = "Total Weather Requests")]
        public int WeatherRequests { get; set; } // Weather API requests

        [Display(Name = "Last Weather Request")]
        public DateTime LastWeatherRequest { get; set; } // Last recorded weather request

        // work in progress


        public int AppUserId { get; set; } // UserID-ForeignKey

        // Relationships to other tables
        public AppUser AppUser { get; set; } // User 
    }
}

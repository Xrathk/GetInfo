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

        [Display(Name = "Total News Requests")]
        public int NewsRequests { get; set; } // News API requests

        [Display(Name = "Last News Request")]
        public DateTime LastNewsRequest { get; set; } // Last recorded news request

        // -- WORK IN PROGRESS --


        // Foreign Keys - Nav properties
        [Required]
        public int AppUserId { get; set; } // UserID-ForeignKey
        public AppUser AppUser { get; set; } // User 
    }
}

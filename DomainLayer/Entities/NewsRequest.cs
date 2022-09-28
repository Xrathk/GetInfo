using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Entities
{
    /// <summary>
    /// News request per keyword. Used for data analytics.
    /// </summary>
    public class NewsRequest
    {
        public int Id; // DB Id

        [Display(Name = "Keyword")]
        public string Keyword { get; set; } // Keyword

        [Display(Name = "Times keyword requested")]
        public int TimesRequested { get; set; } // Times keyword requested

        [Display(Name = "Average results per request")]
        public int AverageResults { get; set; } // Average results per request

        public DateTime LastRequested { get; set; } // Timestamp of last request

    }
}

using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Entities
{
    /// <summary>
    /// Weather requests per location. Used for data analytics.
    /// </summary>
    public class WeatherRequest
    {
        public int Id; // DB Id

        [Display(Name = "Location")]
        public string LocationName { get; set; } // Location

        public double LastRecordedTemperature { get; set; } // Last recorded temperature

        public double MaxRecordedTemperature { get; set; } // Max recorded temp
        
        public double MinRecordedTemperature { get; set; } // Min recorded temp

        [Display(Name = "Times location requested")]
        public int TimesRequested { get; set; } // Times location requested

        public DateTime LastRequested { get; set; } // Timestamp of last request

    }
}

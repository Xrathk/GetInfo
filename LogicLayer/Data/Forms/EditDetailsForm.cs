using System.ComponentModel.DataAnnotations;

namespace LogicLayer.Data.Forms
{
    /// <summary>
    /// Model for detail editing form.
    /// </summary>
    public class EditDetailsForm
    {
        [MinLength(1, ErrorMessage = "First name must include characters.")]
        public string FirstName { get; set; } // User's first name

        [MinLength(1, ErrorMessage = "Last name must include characters.")]
        public string LastName { get; set; } // User's last name

        [Range(12, 160, ErrorMessage = "Age must be above 12 and less than 160.")]
        public int Age { get; set; } // User age

        public string CountryOfOrigin { get; set; } // User's country

        public string CurrentLocation { get; set; } // User's current location
    }
}

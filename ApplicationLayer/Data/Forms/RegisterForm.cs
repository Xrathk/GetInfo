using System.ComponentModel.DataAnnotations;

namespace ApplicationLayer.Data.Forms
{
    /// <summary>
    /// Model for sign-up form.
    /// </summary>
    public class RegisterForm
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$", ErrorMessage = "E-Mail address is not valid.")]
        public string Email { get; set; }

        [Required]
        [StringLength(24, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 25 characters long.")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(24, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 25 characters long.")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "The fields \"Password\" and \"Confirm Password\" must be equal.")]
        public string PasswordRepeat { get; set; }
    }
}

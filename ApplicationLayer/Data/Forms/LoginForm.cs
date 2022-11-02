using System.ComponentModel.DataAnnotations;

namespace ApplicationLayer.Data.Forms
{
    /// <summary>
    /// Model for login form.
    /// </summary>
    public class LoginForm
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

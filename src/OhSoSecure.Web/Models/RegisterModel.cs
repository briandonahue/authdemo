using System.ComponentModel.DataAnnotations;

namespace OhSoSecure.Web.Models
{
    public class RegisterModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required(ErrorMessage = "The Confirm Password field is required.")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
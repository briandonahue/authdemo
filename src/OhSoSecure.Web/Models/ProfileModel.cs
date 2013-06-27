using System.ComponentModel.DataAnnotations;

namespace OhSoSecure.Web.Models
{
    public class ProfileModel
    {
        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        [Display(Description = "Use something creative!")]
        public string UserName { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace AutoParts.Web.Client.Public.User.Models
{
    public class UserSignInFormModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

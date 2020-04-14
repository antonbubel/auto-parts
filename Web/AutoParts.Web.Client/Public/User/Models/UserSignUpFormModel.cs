using AutoParts.Web.Client.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace AutoParts.Web.Client.Public.User.Models
{
    public class UserSignUpFormModel
    {
        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(ValidationConstants.DefaultMaxLength, ErrorMessage = "First name must be less than 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [MaxLength(ValidationConstants.DefaultMaxLength, ErrorMessage = "Last name must be less than 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email address is not valid.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string PasswordConfirmation { get; set; }
    }
}

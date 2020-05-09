using AutoParts.Web.Client.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace AutoParts.Web.Client.Public.User.Models
{
    public class SupplierSignUpFormModel
    {
        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(ValidationConstants.DefaultMaxLength, ErrorMessage = "First name must be less than 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(ValidationConstants.DefaultMaxLength, ErrorMessage = "First name must be less than 50 characters.")]
        public string LastName { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string PasswordConfirmation { get; set; }

        [Required(ErrorMessage = "Organization name is required.")]
        [MaxLength(ValidationConstants.DefaultMaxLength, ErrorMessage = "Organization name must be less than 50 characters.")]
        public string OrganizationName { get; set; }

        [Required(ErrorMessage = "Organization address is required.")]
        [MaxLength(ValidationConstants.OrganizationAddressMaxLength, ErrorMessage = "Organization address must be less than 200 characters.")]
        public string OrganizationAddress { get; set; }

        public string Website { get; set; }

        public string InvitationToken { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^[+][0-9]*$", ErrorMessage = "Phone number is in the invalid format.")]
        public string PhoneNumber { get; set; }
    }
}

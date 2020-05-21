using AutoParts.Web.Client.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace AutoParts.Web.Client.Public.Cart.Models
{
    public class CreateOrderFormModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email address is not valid.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(ValidationConstants.DefaultMaxLength, ErrorMessage = "First name must be less than 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [MaxLength(ValidationConstants.DefaultMaxLength, ErrorMessage = "Last name must be less than 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Street address is required.")]
        [MaxLength(ValidationConstants.StreetAddressMaxLingth, ErrorMessage = "Street address must be less than 200 characters.")]
        public string StreetAddress { get; set; }

        [MaxLength(ValidationConstants.StreetAddressMaxLingth, ErrorMessage = "Street address line 2 must be less than 200 characters.")]
        public string StreetAddressSecondLine { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [MaxLength(ValidationConstants.DefaultMaxLength, ErrorMessage = "City must be less than 50 characters.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Postal / Zip Code is required.")]
        [MaxLength(ValidationConstants.DefaultMaxLength, ErrorMessage = "Postal / Zip Code must be less than 50 characters.")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Region is required.")]
        [MaxLength(ValidationConstants.DefaultMaxLength, ErrorMessage = "Region must be less than 50 characters.")]
        public string Region { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Please select a country.")]
        public long CountryId { get; set; }
    }
}

namespace AutoParts.Web.Client.Private.Supplier.Models
{
    using Blazor.FileReader;

    using System.ComponentModel.DataAnnotations;

    using Shared.Constants;

    public class SupplierProfileFormModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Organization name is required.")]
        [MaxLength(ValidationConstants.DefaultMaxLength, ErrorMessage = "Organization name must be less than 50 characters.")]
        public string OrganizationName { get; set; }

        [Required(ErrorMessage = "Organization address is required.")]
        [MaxLength(ValidationConstants.OrganizationAddressMaxLength, ErrorMessage = "Organization address must be less than 200 characters.")]
        public string OrganizationAddress { get; set; }

        [MaxLength(ValidationConstants.OrganizationAddressMaxLength, ErrorMessage = "Organization description must be less than 500 characters.")]
        public string OrganizationDescription { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email address is not valid.")]
        public string SalesEmail { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^[+][0-9]*$", ErrorMessage = "Phone number is in the invalid format.")]
        public string SalesPhoneNumber { get; set; }

        public string Website { get; set; }

        public IFileInfo LogoFileInfo { get; set; }

        public byte[] LogoBuffer { get; set; }

    }
}

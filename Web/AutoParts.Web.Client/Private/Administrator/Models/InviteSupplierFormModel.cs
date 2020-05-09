namespace AutoParts.Web.Client.Private.Administrator.Models
{
    using System.ComponentModel.DataAnnotations;

    using Shared.Constants;

    public class InviteSupplierFormModel
    {
        [Required(ErrorMessage = "Supplier email is required.")]
        [EmailAddress(ErrorMessage = "Value in the Email field is not a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Supplier name is required.")]
        [MaxLength(ValidationConstants.DefaultMaxLength, ErrorMessage = "Supplier name is required name must be less than 50 characters.")]
        public string Name { get; set; }
    }
}

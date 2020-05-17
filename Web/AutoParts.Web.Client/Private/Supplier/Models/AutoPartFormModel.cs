namespace AutoParts.Web.Client.Private.Supplier.Models
{
    using Blazor.FileReader;

    using System.ComponentModel.DataAnnotations;

    using Shared.Constants;

    public class AutoPartFormModel
    {
        [Required(ErrorMessage = "Auto part name is required.")]
        [MaxLength(ValidationConstants.DefaultMaxLength, ErrorMessage = "Auto part name must be less than 50 characters.")]
        public string Name { get; set; }

        [MaxLength(ValidationConstants.DefaultMaxLength, ErrorMessage = "Auto part description must be less than 200 characters.")]
        public string Description { get; set; }

        public IFileInfo ImageFileInfo { get; set; }

        public byte[] ImageFileBuffer { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price should be valid float number")]
        public double Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity should be a valid integer number.")]
        public int Quantity { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Please select a manufacturer.")]
        public long ManufacturerId { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Please select a country.")]
        public long CountryId { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Please select a car modification.")]
        public long CarModificationId { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Please select a sub catalog.")]
        public long SubCatalogId { get; set; }
    }
}

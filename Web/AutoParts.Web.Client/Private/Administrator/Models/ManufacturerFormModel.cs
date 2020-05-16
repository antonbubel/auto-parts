using AutoParts.Web.Client.Shared.Constants;
using Blazor.FileReader;
using System.ComponentModel.DataAnnotations;

namespace AutoParts.Web.Client.Private.Administrator.Models
{
    public class ManufacturerFormModel
    {
        [Required(ErrorMessage = "Manufacturer name is required.")]
        [MaxLength(ValidationConstants.DefaultMaxLength, ErrorMessage = "Manufacturer name must be less than 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Manufacturer description is required.")]
        [MaxLength(ValidationConstants.DefaultMaxLength, ErrorMessage = "Manufacturer description must be less than 200 characters.")]
        public string Description { get; set; }

        public long CountryId { get; set; }

        public IFileInfo ImageFileInfo { get; set; }

        public byte[] ImageBuffer { get; set; }
    }
}

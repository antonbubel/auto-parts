using AutoParts.Web.Client.Shared.Constants;
using Blazor.FileReader;
using System.ComponentModel.DataAnnotations;

namespace AutoParts.Web.Client.Private.Administrator.Models
{
    public class CarModelFormModel
    {
        [Required(ErrorMessage = "Car model name is required.")]
        [MaxLength(ValidationConstants.DefaultMaxLength, ErrorMessage = "Car model name must be less than 50 characters.")]
        public string Name { get; set; }

        public IFileInfo ImageFileInfo { get; set; }

        public byte[] ImageBuffer { get; set; }

        public long CarBrandId { get; set; }
    }
}

using AutoParts.Web.Client.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace AutoParts.Web.Client.Private.Administrator.Models
{
    public class CarModificationFormModel
    {
        [Required(ErrorMessage = "Car modification name is required.")]
        [MaxLength(ValidationConstants.DefaultMaxLength, ErrorMessage = "Car modification name must be less than 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Car modification description is required.")]
        [MaxLength(ValidationConstants.DefaultMaxLength, ErrorMessage = "Car modification description must be less than 200 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Car modification year is required.")]
        public string Year { get; set; }

        public long CarModelId { get; set; }
    }
}

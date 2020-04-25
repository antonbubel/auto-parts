namespace AutoParts.Core.Constants.Options
{
    using System.ComponentModel.DataAnnotations;

    public class ClientOptions
    {
        public string BaseUrl { get; set; }

        [Required]
        public string SupplierSignUpUrl { get; set; }
    }
}

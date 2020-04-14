using System.ComponentModel.DataAnnotations;

namespace AutoParts.Infrastructure.Web.Options
{
    public class IdentityOptions
    {
        [Required]
        public string ClientId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string IdentityServer { get; set; }
    }
}

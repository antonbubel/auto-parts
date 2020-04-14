using System.ComponentModel.DataAnnotations;

namespace AutoParts.Infrastructure.Web.Options
{
    public class JwtOptions
    {
        [Required]
        public string Authority { get; set; }

        [Required]
        public string Audience { get; set; }

        [Required]
        public string Issuer { get; set; }

        [Required]
        public bool RequireHttpsMetadata { get; set; }
    }
}

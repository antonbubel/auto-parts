namespace AutoParts.Core.Constants.Options
{
    using System.ComponentModel.DataAnnotations;

    public class SendGridOptions
    {
        [Required]
        public string ApiKey { get; set; }

        [Required]
        [EmailAddress]
        public string FromEmail { get; set; }

        [Required]
        public string FromName { get; set; }

        [Required]
        public string SupplierInvitationTemplateId { get; set; }

        [Required]
        public string OrderCreatedUserTemplateId { get; set; }

        [Required]
        public string OrderCreatedSupplierTemplateId { get; set; }
    }
}

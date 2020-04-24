namespace AutoParts.Core.Contracts.Emails.Notifications
{
    using MediatR;

    public class SendEmailNotification : INotification
    {
        public string ToEmail { get; set; }

        public string ToName { get; set; }

        public string TemplateId { get; set; }

        public object TemplateData { get; set; }
    }
}

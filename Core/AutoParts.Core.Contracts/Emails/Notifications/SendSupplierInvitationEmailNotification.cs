namespace AutoParts.Core.Contracts.Emails.Notifications
{
    using MediatR;

    public class SendSupplierInvitationEmailNotification : INotification
    {
        public string ToEmail { get; set; }

        public string ToName { get; set; }

        public string InvitationToken { get; set; }
    }
}

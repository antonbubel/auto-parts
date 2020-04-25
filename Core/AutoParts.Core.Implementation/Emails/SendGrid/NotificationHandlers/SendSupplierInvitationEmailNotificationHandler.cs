namespace AutoParts.Core.Implementation.Emails.SendGrid.NotificationHandlers
{
    using MediatR;

    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Options;

    using Constants.Options;

    using Contracts.Emails.Models;
    using Contracts.Emails.Notifications;

    public class SendSupplierInvitationEmailNotificationHandler : INotificationHandler<SendSupplierInvitationEmailNotification>
    {
        private readonly IMediator mediator;
        private readonly SendGridOptions sendGridOptions;
        private readonly ClientOptions clientOptions;

        public SendSupplierInvitationEmailNotificationHandler(
            IMediator mediator,
            IOptions<SendGridOptions> sendGridOptions,
            IOptions<ClientOptions> clientOptions)
        {
            this.mediator = mediator;
            this.sendGridOptions = sendGridOptions.Value;
            this.clientOptions = clientOptions.Value;
        }

        public Task Handle(SendSupplierInvitationEmailNotification notification, CancellationToken cancellationToken)
        {
            var supplierSignUpUrl = string.Format(clientOptions.SupplierSignUpUrl, clientOptions.BaseUrl, notification.InvitationToken);

            var templateData = new SupplierInvitationEmailTemplateData
            {
                SignUpUrl = supplierSignUpUrl
            };

            var sendEmailNotification = new SendEmailNotification
            {
                ToEmail = notification.ToEmail,
                ToName = notification.ToName,
                TemplateId = sendGridOptions.SupplierInvitationTemplateId,
                TemplateData = templateData
            };

            return mediator.Publish(sendEmailNotification);
        }
    }
}

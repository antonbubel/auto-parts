namespace AutoParts.Core.Implementation.Emails.SendGrid.NotificationHandlers
{
    using MediatR;

    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Options;

    using Constants.Options;

    using Contracts.Emails.Models;
    using Contracts.Emails.Notifications;

    public class SendSupplierOrderCreatedEmailNotificationHandler : INotificationHandler<SendSupplierOrderCreatedEmailNotification>
    {
        private readonly IMediator mediator;
        private readonly SendGridOptions sendGridOptions;
        private readonly ClientOptions clientOptions;

        public SendSupplierOrderCreatedEmailNotificationHandler(
            IMediator mediator,
            IOptions<SendGridOptions> sendGridOptions,
            IOptions<ClientOptions> clientOptions)
        {
            this.mediator = mediator;
            this.sendGridOptions = sendGridOptions.Value;
            this.clientOptions = clientOptions.Value;
        }

        public Task Handle(SendSupplierOrderCreatedEmailNotification notification, CancellationToken cancellationToken)
        {
            var supplierOrdersUrl = string.Format(clientOptions.SupplierOrdersUrl, clientOptions.BaseUrl);

            var templateData = new SupplierOrderCreatedEmailTemplateData
            {
                SupplierOrdersUrl = supplierOrdersUrl
            };

            var sendEmailNotification = new SendEmailNotification
            {
                ToEmail = notification.ToEmail,
                ToName = notification.ToName,
                TemplateId = sendGridOptions.OrderCreatedSupplierTemplateId,
                TemplateData = templateData
            };

            return mediator.Publish(sendEmailNotification);
        }
    }
}

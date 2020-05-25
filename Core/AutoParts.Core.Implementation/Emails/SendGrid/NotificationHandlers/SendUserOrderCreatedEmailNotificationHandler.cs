namespace AutoParts.Core.Implementation.Emails.SendGrid.NotificationHandlers
{
    using MediatR;

    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Options;

    using Constants.Options;

    using Contracts.Emails.Notifications;

    public class SendUserOrderCreatedEmailNotificationHandler : INotificationHandler<SendUserOrderCreatedEmailNotification>
    {
        private readonly IMediator mediator;
        private readonly SendGridOptions sendGridOptions;

        public SendUserOrderCreatedEmailNotificationHandler(
            IMediator mediator,
            IOptions<SendGridOptions> sendGridOptions)
        {
            this.mediator = mediator;
            this.sendGridOptions = sendGridOptions.Value;
        }

        public Task Handle(SendUserOrderCreatedEmailNotification notification, CancellationToken cancellationToken)
        {
            var sendEmailNotification = new SendEmailNotification
            {
                ToEmail = notification.ToEmail,
                ToName = notification.ToName,
                TemplateId = sendGridOptions.OrderCreatedUserTemplateId
            };

            return mediator.Publish(sendEmailNotification);
        }
    }
}

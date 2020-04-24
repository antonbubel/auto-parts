namespace AutoParts.Core.Implementation.Emails.SendGrid.NotificationHandlers
{
    using MediatR;

    using global::SendGrid;
    using global::SendGrid.Helpers.Mail;

    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using Constants.Options;

    using Contracts.Emails.Exceptions;
    using Contracts.Emails.Notifications;

    public class SendEmailNotificationHandler : INotificationHandler<SendEmailNotification>
    {
        private readonly ISendGridClient sendGridClient;
        private readonly SendGridOptions sendGridOptions;
        private readonly ILogger<SendEmailNotificationHandler> logger;

        public SendEmailNotificationHandler(
            ISendGridClient sendGridClient,
            IOptions<SendGridOptions> sendGridOptions,
            ILogger<SendEmailNotificationHandler> logger)
        {
            this.sendGridClient = sendGridClient;
            this.sendGridOptions = sendGridOptions.Value;
            this.logger = logger;
        }

        public async Task Handle(SendEmailNotification notification, CancellationToken cancellationToken)
        {
            var message = PrepareSendGridMessage(notification);

            var response = await sendGridClient.SendEmailAsync(message);

            if (response.StatusCode >= System.Net.HttpStatusCode.OK && response.StatusCode <= System.Net.HttpStatusCode.IMUsed)
            {
                return;
            }

            logger.LogError(
                $"SendGrid returned a negative status. Response: {response} for {notification.ToEmail} address. TemplateId: {notification.TemplateId}");

            throw new SendEmailException();
        }

        private SendGridMessage PrepareSendGridMessage(SendEmailNotification notification)
        {
            var message = new SendGridMessage();

            message.SetFrom(new EmailAddress(sendGridOptions.FromEmail, sendGridOptions.FromName));
            message.AddTo(new EmailAddress(notification.ToEmail, notification.ToName));
            message.SetTemplateId(notification.TemplateId);
            message.SetTemplateData(notification.TemplateData);

            return message;
        }
    }
}

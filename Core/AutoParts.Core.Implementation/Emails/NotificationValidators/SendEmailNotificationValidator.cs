namespace AutoParts.Core.Implementation.Emails.NotificationValidators
{
    using FluentValidation;

    using Contracts.Emails.Notifications;

    public class SendEmailNotificationValidator : AbstractValidator<SendEmailNotification>
    {
        public SendEmailNotificationValidator()
        {
            RuleFor(notification => notification.ToEmail)
                .NotEmpty()
                .EmailAddress();

            RuleFor(notification => notification.TemplateId)
                .NotNull()
                .NotEmpty();
        }
    }
}

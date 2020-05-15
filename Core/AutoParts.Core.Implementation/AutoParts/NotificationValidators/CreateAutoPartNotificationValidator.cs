namespace AutoParts.Core.Implementation.AutoParts.NotificationValidators
{
    using FluentValidation;

    using Contracts.AutoParts.Notifications;

    using Constants.ValidationConstants;

    public class CreateAutoPartNotificationValidator : AbstractValidator<CreateAutoPartNotification>
    {
        public CreateAutoPartNotificationValidator()
        {
            RuleFor(notification => notification.Name)
                .NotEmpty()
                .MaximumLength(ValidationConstants.DefaultMaxLength);

            RuleFor(notification => notification.Description)
                .MaximumLength(ValidationConstants.AutoPartDescriptionMaxLength);
        }
    }
}

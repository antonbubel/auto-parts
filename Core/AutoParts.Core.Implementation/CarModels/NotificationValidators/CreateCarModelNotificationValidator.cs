namespace AutoParts.Core.Implementation.CarModels.NotificationValidators
{
    using FluentValidation;

    using Constants.ValidationConstants;

    using Contracts.CarModels.Notifications;

    public class CreateCarModelNotificationValidator : AbstractValidator<CreateCarModelNotification>
    {
        public CreateCarModelNotificationValidator()
        {
            RuleFor(notification => notification.Name)
                .NotEmpty()
                .MaximumLength(ValidationConstants.DefaultMaxLength);

        }
    }
}

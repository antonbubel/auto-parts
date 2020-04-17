namespace AutoParts.Core.Implementation.CarModels.NotificationValidators
{
    using FluentValidation;

    using Constants.ValidationConstants;

    using Contracts.CarModels.Notifications;

    public class UpdateCarModelNotificationValidator : AbstractValidator<UpdateCarModelNotification>
    {
        public UpdateCarModelNotificationValidator()
        {
            RuleFor(notification => notification.Name)
                .NotEmpty()
                .MaximumLength(ValidationConstants.DefaultMaxLength);

        }
    }
}

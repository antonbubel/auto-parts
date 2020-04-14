namespace AutoParts.Core.Implementation.Users.NotificationValidators
{
    using FluentValidation;

    using Constants.ValidationConstants;
    using Contracts.Users.Notifications;

    public class UserSignUpNotificationValidator : AbstractValidator<UserSignUpNotification>
    {
        public UserSignUpNotificationValidator()
        {
            RuleFor(notification => notification.FirstName)
                .NotEmpty()
                .MaximumLength(UserValidationConstants.DefaultMaxLength);

            RuleFor(notification => notification.LastName)
                .NotEmpty()
                .MaximumLength(UserValidationConstants.DefaultMaxLength);

            RuleFor(notification => notification.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(notification => notification.Password)
                .NotEmpty()
                .MinimumLength(AuthenticationValidationConstants.PasswordMinLength);

            RuleFor(notification => notification.PasswordConfirmation)
                .Equal(notification => notification.Password);
        }
    }
}

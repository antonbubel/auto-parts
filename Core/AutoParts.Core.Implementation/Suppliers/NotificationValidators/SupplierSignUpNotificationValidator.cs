namespace AutoParts.Core.Implementation.Suppliers.NotificationValidators
{
    using FluentValidation;

    using Contracts.Suppliers.Notifications;

    using Constants.ValidationConstants;

    public class SupplierSignUpNotificationValidator : AbstractValidator<SupplierSignUpNotification>
    {
        public SupplierSignUpNotificationValidator()
        {
            RuleFor(notification => notification.FirstName)
                .NotEmpty()
                .MaximumLength(UserValidationConstants.DefaultMaxLength);

            RuleFor(notification => notification.LastName)
                .NotEmpty()
                .MaximumLength(UserValidationConstants.DefaultMaxLength);
            
            RuleFor(notification => notification.PhoneNumber)
                .MinimumLength(UserValidationConstants.PhoneNumberMinLength)
                .MaximumLength(UserValidationConstants.PhoneNumberMaxLength)
                .Matches(UserValidationConstants.PhoneNumberFormat)
                .NotEmpty();

            RuleFor(notification => notification.OrganizationName)
                .NotEmpty()
                .MaximumLength(ValidationConstants.DefaultMaxLength);

            RuleFor(notification => notification.OrganizationAddress)
                .NotEmpty()
                .MaximumLength(ValidationConstants.SupplierOrganizationAddressMaxLength);

            RuleFor(notification => notification.Website)
                .MaximumLength(UserValidationConstants.DefaultMaxLength);

            RuleFor(notification => notification.Password)
                .NotEmpty()
                .MinimumLength(AuthenticationValidationConstants.PasswordMinLength);

            RuleFor(notification => notification.PasswordConfirmation)
                .Equal(notification => notification.Password);

            RuleFor(notification => notification.InvitationToken)
                .NotEmpty();
        }
    }
}

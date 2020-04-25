namespace AutoParts.Core.Implementation.Suppliers.NotificationValidators
{
    using FluentValidation;

    using Contracts.Suppliers.Notifications;

    using Constants.ValidationConstants;

    public class UpdateSupplierProfileNotificationValidator : AbstractValidator<UpdateSupplierProfileNotification>
    {
        public UpdateSupplierProfileNotificationValidator()
        {
            RuleFor(notification => notification.SalesEmail)
                .NotEmpty()
                .EmailAddress();

            RuleFor(notification => notification.SalesPhoneNumber)
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

            RuleFor(notification => notification.OrganizationDescription)
                .MaximumLength(ValidationConstants.SupplierOrganizationDescriptionMaxLength);

            RuleFor(notification => notification.Website)
                .MaximumLength(UserValidationConstants.DefaultMaxLength);
        }
    }
}

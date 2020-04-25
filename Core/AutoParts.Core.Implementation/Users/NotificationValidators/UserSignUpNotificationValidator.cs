namespace AutoParts.Core.Implementation.Users.NotificationValidators
{
    using MediatR;

    using FluentValidation;

    using Constants.ValidationConstants;

    using Contracts.Users.Notifications;

    using Contracts.Suppliers.Requests;

    public class UserSignUpNotificationValidator : AbstractValidator<UserSignUpNotification>
    {
        public UserSignUpNotificationValidator(IMediator mediator)
        {
            RuleFor(notification => notification.FirstName)
                .NotEmpty()
                .MaximumLength(UserValidationConstants.DefaultMaxLength);

            RuleFor(notification => notification.LastName)
                .NotEmpty()
                .MaximumLength(UserValidationConstants.DefaultMaxLength);

            RuleFor(notification => notification.Email)
                .NotEmpty()
                .EmailAddress()
                .MustAsync(async (email, cancelationToken) =>
                {
                    var request = new SupplierInvitationExistsByEmailRequest
                    {
                        Email = email
                    };

                    var supplierInvitationExists = await mediator.Send(request);

                    return !supplierInvitationExists;
                });

            RuleFor(notification => notification.Password)
                .NotEmpty()
                .MinimumLength(AuthenticationValidationConstants.PasswordMinLength);

            RuleFor(notification => notification.PasswordConfirmation)
                .Equal(notification => notification.Password);
        }
    }
}

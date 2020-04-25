namespace AutoParts.Core.Implementation.Suppliers.NotificationValidators
{
    using MediatR;

    using FluentValidation;

    using Constants.ValidationConstants;

    using Contracts.Suppliers.Requests;
    using Contracts.Suppliers.Notifications;

    using Contracts.Users.Requests;

    public class InviteSupplierNotificationValidator : AbstractValidator<InviteSupplierNotification>
    {
        public InviteSupplierNotificationValidator(IMediator mediator)
        {
            RuleFor(notification => notification.Name)
                .NotEmpty()
                .MaximumLength(ValidationConstants.DefaultMaxLength);

            RuleFor(notification => notification.Email)
                .NotEmpty()
                .EmailAddress()
                .MustAsync(async (email, cancelationToken) =>
                {
                    var request = new UserExistsByEmailRequest
                    {
                        Email = email
                    };

                    var userExists = await mediator.Send(request);

                    return !userExists;
                })
                .WithMessage(email => $"User with email {email} alredy exists.")
                .MustAsync(async (email, cancelationToken) =>
                {
                    var request = new SupplierInvitationExistsByEmailRequest
                    {
                        Email = email
                    };

                    var supplierInvitationExists = await mediator.Send(request);

                    return !supplierInvitationExists;
                })
                .WithMessage(email => $"Supplier invitation for email {email} alredy exists.");
        }
    }
}

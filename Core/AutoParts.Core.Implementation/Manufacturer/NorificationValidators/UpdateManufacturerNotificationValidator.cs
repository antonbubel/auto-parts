namespace AutoParts.Core.Implementation.Manufacturer.NorificationValidators
{
    using FluentValidation;

    using MediatR;

    using Contracts.Manufacturer.Requests;
    using Contracts.Manufacturer.Notifications;

    using Constants.ValidationConstants;

    public class UpdateManufacturerNotificationValidator : AbstractValidator<UpdateManufacturerNotification>
    {
        public UpdateManufacturerNotificationValidator(IMediator mediator)
        {
            RuleFor(notification => notification.Name)
                .NotEmpty()
                .MaximumLength(ValidationConstants.DefaultMaxLength)
                .MustAsync(async (name, cancelationToken) =>
                {
                    var request = new ManufacturerExistsByNameRequest
                    {
                        Name = name
                    };

                    var manufacturerExists = await mediator.Send(request);

                    return !manufacturerExists;
                })
                .WithMessage(name => $"Manufacturer with name {name} already exists.");

            RuleFor(notification => notification.Description)
                .NotEmpty()
                .MaximumLength(ValidationConstants.ManufacturerDescriptionMaxLength);
        }
    }
}

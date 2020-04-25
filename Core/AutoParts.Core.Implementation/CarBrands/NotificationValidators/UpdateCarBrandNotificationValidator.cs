namespace AutoParts.Core.Implementation.CarBrands.NotificationValidators
{
    using MediatR;

    using FluentValidation;

    using Constants.ValidationConstants;

    using Contracts.CarBrands.Requests;
    using Contracts.CarBrands.Notifications;

    public class UpdateCarBrandNotificationValidator : AbstractValidator<UpdateCarBrandNotification>
    {
        public UpdateCarBrandNotificationValidator(IMediator mediator)
        {
            RuleFor(notification => notification.Name)
                .NotEmpty()
                .MaximumLength(ValidationConstants.DefaultMaxLength)
                .MustAsync(async (name, cancelationToken) =>
                {
                    var request = new CarBrandExistsByNameRequest
                    {
                        Name = name
                    };

                    var carBrandExists = await mediator.Send(request);

                    return !carBrandExists;
                })
                .WithMessage(name => $"Car brand with name {name} is already exists.");

        }
    }
}

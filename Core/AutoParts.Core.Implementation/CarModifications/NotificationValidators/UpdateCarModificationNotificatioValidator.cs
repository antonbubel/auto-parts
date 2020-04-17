namespace AutoParts.Core.Implementation.CarModifications.NotificationValidators
{
    using FluentValidation;

    using System;

    using Contracts.CarModifications.Notifications;

    using Constants.ValidationConstants;

    public class UpdateCarModificationNotificatioValidator : AbstractValidator<UpdateCarModificationNotification>
    {
        public UpdateCarModificationNotificatioValidator()
        {
            RuleFor(notification => notification.Name)
                .NotEmpty()
                .MaximumLength(ValidationConstants.DefaultMaxLength);

            RuleFor(notification => notification.Description)
                .MaximumLength(ValidationConstants.CarModificationDescriptionMaxLength);

            RuleFor(notification => notification.Year)
                .Must(year => year > ValidationConstants.CarModificationYearMinValue)
                .Must(year => year <= DateTime.UtcNow.Year);
        }
    }
}

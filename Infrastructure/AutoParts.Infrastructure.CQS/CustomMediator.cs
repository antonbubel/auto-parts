namespace AutoParts.Infrastructure.CQS
{
    using MediatR;

    using FluentValidation;

    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class CustomMediator : IMediator
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IMediator mediator;

        public CustomMediator(ServiceFactory serviceFactory, IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            mediator = new Mediator(serviceFactory);
        }

        public async Task Publish(object notification, CancellationToken cancellationToken = default)
        {
            await ValidateNotification(notification);

            await mediator.Publish(notification, cancellationToken);
        }

        public async Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
        {
            await ValidateNotification(notification);

            await mediator.Publish(notification, cancellationToken);
        }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            return mediator.Send(request);
        }

        private async Task ValidateNotification(object notification)
        {
            var validatorType = typeof(IValidator<>).MakeGenericType(notification.GetType());

            var validator = this.serviceProvider.GetService(validatorType) as IValidator;

            if (validator != null)
            {
                var validationContext = new ValidationContext(notification);

                var validationResult = await validator.ValidateAsync(validationContext);

                if (validationResult != null && !validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }
            }
        }
    }
}

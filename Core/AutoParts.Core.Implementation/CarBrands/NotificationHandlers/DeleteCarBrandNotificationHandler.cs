namespace AutoParts.Core.Implementation.CarBrands.NotificationHandlers
{
    using MediatR;

    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.CarBrands.Exceptions;
    using Contracts.CarBrands.Notifications;

    using Data.Model.Results;
    using Data.Model.Repositories;

    public class DeleteCarBrandNotificationHandler : INotificationHandler<DeleteCarBrandNotification>
    {
        private readonly ICarBrandRepository carBrandRepository;

        public DeleteCarBrandNotificationHandler(ICarBrandRepository carBrandRepository)
        {
            this.carBrandRepository = carBrandRepository;
        }

        public async Task Handle(DeleteCarBrandNotification notification, CancellationToken cancellationToken)
        {
            var operationResult = await carBrandRepository.RemoveAsync(notification.CarBrandId)
                .ConfigureAwait(false);

            if (operationResult.Status != OperationStatus.Successful)
            {
                throw new DeleteCarBrandException(operationResult);
            }
        }
    }
}

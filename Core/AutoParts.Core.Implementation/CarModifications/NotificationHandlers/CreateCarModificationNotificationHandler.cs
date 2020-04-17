namespace AutoParts.Core.Implementation.CarModifications.NotificationHandlers
{
    using MediatR;

    using AutoMapper;

    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.CarModifications.Exceptions;
    using Contracts.CarModifications.Notifications;

    using Data.Model.Results;
    using Data.Model.Entities;
    using Data.Model.Repositories;

    public class CreateCarModificationNotificationHandler : INotificationHandler<CreateCarModificationNotification>
    {
        private readonly IMapper mapper;
        private readonly ICarModificationRepository carModificationRepository;

        public CreateCarModificationNotificationHandler(IMapper mapper, ICarModificationRepository carModificationRepository)
        {
            this.mapper = mapper;
            this.carModificationRepository = carModificationRepository;
        }

        public async Task Handle(CreateCarModificationNotification notification, CancellationToken cancellationToken)
        {
            var carModification = mapper.Map<CarModification>(notification);

            var operationResult = await carModificationRepository.CreateAsync(carModification)
                .ConfigureAwait(false);

            if (operationResult.Status != OperationStatus.Successful)
            {
                throw new CreateCarModificationException(operationResult);
            }
        }
    }
}

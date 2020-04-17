namespace AutoParts.Core.Implementation.CarModels.NotificationHandlers
{
    using MediatR;

    using AutoMapper;

    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.CarModels.Exceptions;
    using Contracts.CarModels.Notifications;

    using Contracts.Files.Requests;

    using Data.Model.Results;
    using Data.Model.Entities;
    using Data.Model.Repositories;

    public class CreateCarModelNotificationHandler : INotificationHandler<CreateCarModelNotification>
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly ICarModelRepository carModelRepository;

        public CreateCarModelNotificationHandler(IMapper mapper, IMediator mediator, ICarModelRepository carModelRepository)
        {
            this.mapper = mapper;
            this.mediator = mediator;
            this.carModelRepository = carModelRepository;
        }

        public async Task Handle(CreateCarModelNotification notification, CancellationToken cancellationToken)
        {
            var carModel = mapper.Map<CarModel>(notification);

            if (!string.IsNullOrEmpty(notification.ImageFileName) && !notification.ImageFileBuffer.IsEmpty)
            {
                carModel.Image = await mediator.Send(new SaveFileRequest { FileName = notification.ImageFileName, Buffer = notification.ImageFileBuffer });
            }

            var operationResult = await carModelRepository.CreateAsync(carModel)
                .ConfigureAwait(false);

            if (operationResult.Status != OperationStatus.Successful)
            {
                throw new CreateCarModelException(operationResult);
            }
        }
    }
}

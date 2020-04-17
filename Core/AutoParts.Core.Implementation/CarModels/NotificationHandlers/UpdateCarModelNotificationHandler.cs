namespace AutoParts.Core.Implementation.CarModels.NotificationHandlers
{
    using MediatR;

    using AutoMapper;

    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.CarModels.Exceptions;
    using Contracts.CarModels.Notifications;

    using Contracts.Files.Requests;
    using Contracts.Files.Notifications;

    using Data.Model.Results;
    using Data.Model.Repositories;

    public class UpdateCarModelNotificationHandler : INotificationHandler<UpdateCarModelNotification>
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly ICarModelRepository carModelRepository;

        public UpdateCarModelNotificationHandler(IMapper mapper, IMediator mediator, ICarModelRepository carModelRepository)
        {
            this.mapper = mapper;
            this.mediator = mediator;
            this.carModelRepository = carModelRepository;
        }

        public async Task Handle(UpdateCarModelNotification notification, CancellationToken cancellationToken)
        {
            var carModel = await carModelRepository.FindAsync(notification.CarModelId);

            if (carModel == null)
            {
                // TODO: throw more concrete exception instead of System.ArgumentException
                throw new ArgumentException();
            }

            var carModelImage = carModel.Image;

            if (!string.IsNullOrEmpty(notification.ImageFileName) && !notification.ImageFileBuffer.IsEmpty)
            {
                if (!string.IsNullOrEmpty(carModel.Image))
                {
                    await mediator.Publish(new DeleteFileNotification { FileName = carModel.Image });
                }

                carModelImage = await mediator.Send(new SaveFileRequest { FileName = notification.ImageFileName, Buffer = notification.ImageFileBuffer });
            }

            mapper.Map(notification, carModel);

            carModel.Image = carModelImage;

            var operationResult = await carModelRepository.UpdateAsync(carModel)
                .ConfigureAwait(false);

            if (operationResult.Status != OperationStatus.Successful)
            {
                throw new UpdateCarModelException(operationResult);
            }
        }
    }
}

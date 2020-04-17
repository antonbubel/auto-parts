namespace AutoParts.Core.Implementation.CarBrands.NotificationHandlers
{
    using MediatR;

    using AutoMapper;

    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.CarBrands.Exceptions;
    using Contracts.CarBrands.Notifications;

    using Contracts.Files.Requests;
    using Contracts.Files.Notifications;

    using Data.Model.Results;
    using Data.Model.Repositories;

    public class UpdateCarBrandNotificationHandler : INotificationHandler<UpdateCarBrandNotification>
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly ICarBrandRepository carBrandRepository;

        public UpdateCarBrandNotificationHandler(IMapper mapper, IMediator mediator, ICarBrandRepository carBrandRepository)
        {
            this.mapper = mapper;
            this.mediator = mediator;
            this.carBrandRepository = carBrandRepository;
        }

        public async Task Handle(UpdateCarBrandNotification notification, CancellationToken cancellationToken)
        {
            var carBrand = await carBrandRepository.FindAsync(notification.CarBrandId);

            if (carBrand == null)
            {
                // TODO: throw more concrete exception instead of System.ArgumentException
                throw new ArgumentException();
            }

            var carBrandImage = carBrand.Image;

            if (!string.IsNullOrEmpty(notification.ImageFileName) && !notification.ImageFileBuffer.IsEmpty)
            {
                if (!string.IsNullOrEmpty(carBrand.Image))
                {
                    await mediator.Publish(new DeleteFileNotification { FileName = carBrand.Image });
                }

                carBrandImage = await mediator.Send(new SaveFileRequest { FileName = notification.ImageFileName, Buffer = notification.ImageFileBuffer });
            }

            mapper.Map(notification, carBrand);

            carBrand.Image = carBrandImage;

            var operationResult = await carBrandRepository.UpdateAsync(carBrand)
                .ConfigureAwait(false);

            if (operationResult.Status != OperationStatus.Successful)
            {
                throw new UpdateCarBrandException(operationResult);
            }
        }
    }
}

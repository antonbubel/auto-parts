namespace AutoParts.Core.Implementation.CarBrands.NotificationHandlers
{
    using MediatR;

    using AutoMapper;

    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.CarBrands.Exceptions;
    using Contracts.CarBrands.Notifications;

    using Contracts.Files.Requests;

    using Data.Model.Results;
    using Data.Model.Entities;
    using Data.Model.Repositories;
    
    public class CreateCarBrandNotificationHandler : INotificationHandler<CreateCarBrandNotification>
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly ICarBrandRepository carBrandRepository;

        public CreateCarBrandNotificationHandler(IMapper mapper, IMediator mediator, ICarBrandRepository carBrandRepository)
        {
            this.mapper = mapper;
            this.mediator = mediator;
            this.carBrandRepository = carBrandRepository;
        }

        public async Task Handle(CreateCarBrandNotification notification, CancellationToken cancellationToken)
        {
            var carBrand = mapper.Map<CarBrand>(notification);

            if (!string.IsNullOrEmpty(notification.ImageFileName) && !notification.ImageFileBuffer.IsEmpty)
            {
                carBrand.Image = await mediator.Send(new SaveFileRequest { FileName = notification.ImageFileName, Buffer = notification.ImageFileBuffer });
            }

            var operationResult = await carBrandRepository.CreateAsync(carBrand)
                .ConfigureAwait(false);

            if (operationResult.Status != OperationStatus.Successful)
            {
                throw new CreateCarBrandException(operationResult);
            }
        }
    }
}

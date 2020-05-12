namespace AutoParts.Core.Implementation.Manufacturer.NotificationHandlers
{
    using MediatR;

    using AutoMapper;

    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.Manufacturer.Exceptions;
    using Contracts.Manufacturer.Notifications;

    using Contracts.Files.Requests;

    using Data.Model.Results;
    using Data.Model.Entities;
    using Data.Model.Repositories;

    public class CreateManufacturerNotificationHandler : INotificationHandler<CreateManufacturerNotification>
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly IManufacturerRepository manufacturerRepository;

        public CreateManufacturerNotificationHandler(
            IMediator mediator,
            IMapper mapper,
            IManufacturerRepository manufacturerRepository)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.manufacturerRepository = manufacturerRepository;
        }

        public async Task Handle(CreateManufacturerNotification notification, CancellationToken cancellationToken)
        {
            var manufacturer = mapper.Map<Manufacturer>(notification);

            if (!string.IsNullOrWhiteSpace(notification.ImageFileName) && !notification.ImageFileBuffer.IsEmpty)
            {
                var saveFileRequest = new SaveFileRequest
                {
                    FileName = notification.ImageFileName,
                    Buffer = notification.ImageFileBuffer
                };

                manufacturer.Image = await mediator.Send(saveFileRequest)
                    .ConfigureAwait(false);
            }

            var operationResult = await manufacturerRepository.CreateAsync(manufacturer)
                .ConfigureAwait(false);

            if (operationResult.Status != OperationStatus.Successful)
            {
                throw new CreateManufacturerException(operationResult);
            }
        }
    }
}

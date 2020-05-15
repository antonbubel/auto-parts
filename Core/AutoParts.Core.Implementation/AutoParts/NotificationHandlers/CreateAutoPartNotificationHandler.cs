namespace AutoParts.Core.Implementation.AutoParts.NotificationHandlers
{
    using MediatR;

    using AutoMapper;

    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.AutoParts.Exceptions;
    using Contracts.AutoParts.Notifications;

    using Contracts.Files.Requests;

    using Data.Model.Results;
    using Data.Model.Entities;
    using Data.Model.Repositories;

    public class CreateAutoPartNotificationHandler : INotificationHandler<CreateAutoPartNotification>
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly IAutoPartRepository autoPartRepository;

        public CreateAutoPartNotificationHandler(
            IMediator mediator,
            IMapper mapper,
            IAutoPartRepository autoPartRepository)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.autoPartRepository = autoPartRepository;
        }

        public async Task Handle(CreateAutoPartNotification notification, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<AutoPart>(notification);

            if (!string.IsNullOrEmpty(notification.ImageFileName) && !notification.ImageFileBuffer.IsEmpty)
            {
                var saveFileRequest = new SaveFileRequest
                {
                    FileName = notification.ImageFileName,
                    Buffer = notification.ImageFileBuffer
                };

                entity.Image = await mediator.Send(saveFileRequest)
                    .ConfigureAwait(false);
            }

            var operationResult = await autoPartRepository.CreateAsync(entity)
                .ConfigureAwait(false);

            if (operationResult.Status != OperationStatus.Successful)
            {
                throw new CreateAutoPartException(operationResult);
            }
        }
    }
}

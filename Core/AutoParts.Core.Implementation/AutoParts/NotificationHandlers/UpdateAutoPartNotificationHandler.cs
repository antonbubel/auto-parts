namespace AutoParts.Core.Implementation.AutoParts.NotificationHandlers
{
    using MediatR;

    using AutoMapper;

    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.AutoParts.Exceptions;
    using Contracts.AutoParts.Notifications;

    using Contracts.Files.Requests;
    using Contracts.Files.Notifications;

    using Data.Model.Results;
    using Data.Model.Entities;
    using Data.Model.Repositories;
    
    using Infrastructure.Exceptions;

    public class UpdateAutoPartNotificationHandler : INotificationHandler<UpdateAutoPartNotification>
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly IAutoPartRepository autoPartRepository;

        public UpdateAutoPartNotificationHandler(
            IMediator mediator,
            IMapper mapper,
            IAutoPartRepository autoPartRepository)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.autoPartRepository = autoPartRepository;
        }

        public async Task Handle(UpdateAutoPartNotification notification, CancellationToken cancellationToken)
        {
            var entity = await autoPartRepository.FindAsync(notification.AutoPartId)
                .ConfigureAwait(false);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            if (entity.SupplierId != notification.SupplierId)
            {
                throw new ForbiddenException();
            }

            mapper.Map(notification, entity);

            entity.Image = await SaveAutoPartImageFromNotificationAndDeletePreviousImageIfItExists(notification, entity);

            var operationResult = await autoPartRepository.UpdateAsync(entity)
                .ConfigureAwait(false);

            if (operationResult.Status != OperationStatus.Successful)
            {
                throw new UpdateAutoPartException(operationResult);
            }
        }

        private async Task<string> SaveAutoPartImageFromNotificationAndDeletePreviousImageIfItExists(
            UpdateAutoPartNotification notification,
            AutoPart entity)
        {
            if (!string.IsNullOrEmpty(notification.ImageFileName) && !notification.ImageFileBuffer.IsEmpty)
            {
                if (!string.IsNullOrEmpty(entity.Image))
                {
                    var deleteFileNotification = new DeleteFileNotification
                    {
                        FileName = entity.Image
                    };

                    await mediator.Publish(deleteFileNotification)
                        .ConfigureAwait(false);
                }

                var saveFileRequest = new SaveFileRequest
                {
                    FileName = notification.ImageFileName,
                    Buffer = notification.ImageFileBuffer
                };

                return await mediator.Send(saveFileRequest)
                    .ConfigureAwait(false);
            }

            return entity.Image;
        }
    }
}

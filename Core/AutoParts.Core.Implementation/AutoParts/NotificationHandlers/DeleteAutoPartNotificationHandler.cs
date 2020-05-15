namespace AutoParts.Core.Implementation.AutoParts.NotificationHandlers
{
    using MediatR;

    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.AutoParts.Exceptions;
    using Contracts.AutoParts.Notifications;

    using Contracts.Files.Notifications;

    using Data.Model.Results;
    using Data.Model.Entities;
    using Data.Model.Repositories;

    using Infrastructure.Exceptions;

    public class DeleteAutoPartNotificationHandler : INotificationHandler<DeleteAutoPartNotification>
    {
        private readonly IMediator mediator;
        private readonly IAutoPartRepository autoPartRepository;

        public DeleteAutoPartNotificationHandler(
            IMediator mediator,
            IAutoPartRepository autoPartRepository)
        {
            this.mediator = mediator;
            this.autoPartRepository = autoPartRepository;
        }

        public async Task Handle(DeleteAutoPartNotification notification, CancellationToken cancellationToken)
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

            await DeleteAutoPartImageIfItExists(entity);

            var operationResult = await autoPartRepository.RemoveAsync(entity.Id)
                .ConfigureAwait(false);

            if (operationResult.Status != OperationStatus.Successful)
            {
                throw new DeleteAutoPartException(operationResult);
            }
        }

        private async Task DeleteAutoPartImageIfItExists(AutoPart entity)
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
        }
    }
}

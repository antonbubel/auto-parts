using System;

namespace AutoParts.Core.Implementation.Files.LocalFolderFileStorage.NotificationHandlers
{
    using MediatR;

    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.Files.Notifications;

    public class DeleteFileNotificationHandler : INotificationHandler<DeleteFileNotification>
    {
        public Task Handle(DeleteFileNotification notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

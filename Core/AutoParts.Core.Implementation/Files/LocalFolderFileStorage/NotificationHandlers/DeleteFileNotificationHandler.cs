using System;

namespace AutoParts.Core.Implementation.Files.LocalFolderFileStorage.NotificationHandlers
{
    using MediatR;

    using System.Threading;
    using System.Threading.Tasks;

    using System.IO;

    using Contracts.Files.Notifications;

    using Constants;

    public class DeleteFileNotificationHandler : INotificationHandler<DeleteFileNotification>
    {
        public Task Handle(DeleteFileNotification notification, CancellationToken cancellationToken)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FileConstants.LocalFilesFolderName, notification.FileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            return Task.CompletedTask;
        }
    }
}

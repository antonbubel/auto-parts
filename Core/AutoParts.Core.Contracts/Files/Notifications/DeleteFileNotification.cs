namespace AutoParts.Core.Contracts.Files.Notifications
{
    using MediatR;

    public class DeleteFileNotification : INotification
    {
        public string FileName { get; set; }
    }
}

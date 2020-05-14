namespace AutoParts.Core.Contracts.AutoParts.Notifications
{
    using MediatR;

    public class DeleteAutoPartNotification : INotification
    {
        public long AutoPartId { get; set; }

        public long SupplierId { get; set; }
    }
}

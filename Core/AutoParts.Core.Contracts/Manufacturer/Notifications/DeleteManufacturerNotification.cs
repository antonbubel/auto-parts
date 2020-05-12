namespace AutoParts.Core.Contracts.Manufacturer.Notifications
{
    using MediatR;

    public class DeleteManufacturerNotification : INotification
    {
        public long ManufacturerId { get; set; }
    }
}

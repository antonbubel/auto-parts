namespace AutoParts.Core.Contracts.AutoParts.Notifications
{
    using MediatR;

    public class UpdateAutoPartAvailabilityNotification : INotification
    {
        public long AutoPartId { get; set; }

        public int RemovedQuantity { get; set; }
    }
}

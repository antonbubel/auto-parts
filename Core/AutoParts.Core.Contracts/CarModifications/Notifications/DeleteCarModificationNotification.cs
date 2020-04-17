namespace AutoParts.Core.Contracts.CarModifications.Notifications
{
    using MediatR;

    public class DeleteCarModificationNotification : INotification
    {
        public long CarModificationId { get; set; }
    }
}

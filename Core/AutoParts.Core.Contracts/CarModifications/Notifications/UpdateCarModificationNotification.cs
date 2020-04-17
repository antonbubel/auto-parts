namespace AutoParts.Core.Contracts.CarModifications.Notifications
{
    using MediatR;

    public class UpdateCarModificationNotification : INotification
    {
        public long CarModificationId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }
    }
}

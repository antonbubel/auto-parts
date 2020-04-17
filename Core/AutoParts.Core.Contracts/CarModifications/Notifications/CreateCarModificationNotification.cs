namespace AutoParts.Core.Contracts.CarModifications.Notifications
{
    using MediatR;

    public class CreateCarModificationNotification : INotification
    {
        public long CarModelId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }
    }
}

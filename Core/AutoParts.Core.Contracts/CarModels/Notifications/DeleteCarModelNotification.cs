namespace AutoParts.Core.Contracts.CarModels.Notifications
{
    using MediatR;

    public class DeleteCarModelNotification : INotification
    {
        public long CarModelId { get; set; }
    }
}

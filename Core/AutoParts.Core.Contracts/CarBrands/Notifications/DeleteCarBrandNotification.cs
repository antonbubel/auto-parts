namespace AutoParts.Core.Contracts.CarBrands.Notifications
{
    using MediatR;

    public class DeleteCarBrandNotification : INotification
    {
        public long CarBrandId { get; set; }
    }
}

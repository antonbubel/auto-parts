namespace AutoParts.Core.Contracts.CarBrands.Notifications
{
    using MediatR;

    public class UpdateCarBrandNotification : INotification
    {
        public long CarBrandId { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }
    }
}

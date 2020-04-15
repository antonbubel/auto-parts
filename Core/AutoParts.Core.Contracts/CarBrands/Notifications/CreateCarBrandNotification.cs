namespace AutoParts.Core.Contracts.CarBrands.Notifications
{
    using MediatR;

    public class CreateCarBrandNotification : INotification
    {
        public string Name { get; set; }

        public string Image { get; set; }
    }
}

namespace AutoParts.Core.Contracts.Orders.Notifications
{
    using MediatR;

    using Models;

    public class CreateOrderNotification : INotification
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string StreetAddress { get; set; }

        public string StreetAddressSecondLine { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string ZipCode { get; set; }

        public string Comment { get; set; }

        public long CountryId { get; set; }

        public long? UserId { get; set; }

        public bool SaveShippingInfo { get; set; }

        public OrderItemModel[] OrderItems { get; set; }
    }
}

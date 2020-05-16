namespace AutoParts.Data.Model.Entities
{
    using Base;

    using OrderStatusEnum = Enums.OrderStatus;

    public class Order : BaseEntity<long>
    {
        public string StreetAddress { get; set; }

        public string StreetAddressSecondLine { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string ZipCode { get; set; }

        public long CountryId { get; set; }

        public Country Country { get; set; }

        public long UserId { get; set; }

        public User User { get; set; }

        public OrderStatusEnum OrderStatusId { get; set; }

        public OrderStatus OrderStatus { get; set; }
    }
}

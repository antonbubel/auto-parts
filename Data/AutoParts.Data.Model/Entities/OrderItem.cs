namespace AutoParts.Data.Model.Entities
{
    using Base;

    public class OrderItem : BaseEntity<long>
    {
        public int Quantity { get; set; }

        public long AutoPartId { get; set; }

        public AutoPart AutoPart { get; set; }

        public long OrderId { get; set; }

        public Order Order { get; set; }
    }
}

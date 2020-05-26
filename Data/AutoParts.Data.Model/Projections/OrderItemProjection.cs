namespace AutoParts.Data.Model.Projections
{
    public class OrderItemProjection
    {
        public long AutoPartId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public long SupplierId { get; set; }

        public string SupplierName { get; set; }
    }
}

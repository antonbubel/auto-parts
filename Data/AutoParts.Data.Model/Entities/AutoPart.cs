namespace AutoParts.Data.Model.Entities
{
    using Base;

    public class AutoPart : BaseEntity<long>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public bool IsAvailable { get; set; }

        public long ManufacturerId { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public long CountryId { get; set; }

        public Country Country { get; set; }

        public long SupplierId { get; set; }

        public SupplierProfile Supplier { get; set; }
    }
}

namespace AutoParts.Data.Model.Projections
{
    public class AutoPartProjection
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public bool IsAvailable { get; set; }

        public long CountryId { get; set; }

        public string CountryName { get; set; }

        public long ManufacturerId { get; set; }

        public string ManufacturerName { get; set; }

        public long SupplierId { get; set; }

        public string SupplierName { get; set; }

        public long AutoPartsCatalogSubGroupId { get; set; }

        public string AutoPartsCatalogSubGroupName { get; set; }
    }
}

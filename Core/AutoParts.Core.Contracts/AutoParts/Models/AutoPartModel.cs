namespace AutoParts.Core.Contracts.AutoParts.Models
{
    public class AutoPartModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public bool IsAvailable { get; set; }

        public long CountryId { get; set; }

        public string CountryName { get; set; }

        public long ManufacturerId { get; set; }

        public string ManufacturerName { get; set; }

        public long SupplierId { get; set; }

        public string SupplierName { get; set; }

        public long SubCatalogId { get; set; }

        public string SubCatalogName { get; set; }
    }
}

namespace AutoParts.Data.Model.Entities
{
    using System.Collections.Generic;

    using Base;

    public class AutoPart : BaseEntity<long>
    {
        public string Name { get; set; }

        public string NormalizedName { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public bool IsAvailable { get; set; }

        public long ManufacturerId { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public long CountryId { get; set; }

        public Country Country { get; set; }

        public long SupplierId { get; set; }

        public SupplierProfile Supplier { get; set; }

        public long CarModificationId { get; set; }

        public CarModification CarModification { get; set; }

        public long AutoPartsCatalogSubGroupId { get; set; }

        public AutoPartsCatalogSubGroup AutoPartsCatalogSubGroup { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}

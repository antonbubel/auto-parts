namespace AutoParts.Data.Model.Filters
{
    public class AutoPartsFilter
    {
        public int ItemsToSkip { get; set; }

        public int ItemsToTake { get; set; }

        public string SearchText { get; set; }

        public long? CarModificationId { get; set; }

        public long? ManufacturerId { get; set; }

        public long? CountryId { get; set; }

        public long? SupplierId { get; set; }

        public long? AutoPartsCatalogSubGroupId { get; set; }

        public bool AvailableOnly { get; set; }
    }
}

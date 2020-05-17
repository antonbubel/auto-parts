namespace AutoParts.Web.Client.Public.AutoPart.Models
{
    using Protos;

    public class AutoPartsFilter
    {
        public int PageNumber { get; set; } = 1;

        public string SearchText { get; set; }

        public long CarModificationId { get; set; }

        public long ManufacturerId { get; set; }

        public long CountryId { get; set; }

        public long SupplierId { get; set; }

        public long SubCatalogId { get; set; }

        public bool AvailableOnly { get; set; }

        public AutoPartsSortingOption Sorting { get; set; } = AutoPartsSortingOption.NameAscending;
    }
}

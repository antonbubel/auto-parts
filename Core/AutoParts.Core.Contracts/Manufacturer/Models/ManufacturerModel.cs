namespace AutoParts.Core.Contracts.Manufacturer.Models
{
    public class ManufacturerModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public long CountryId { get; set; }

        public string CountryName { get; set; }
    }
}

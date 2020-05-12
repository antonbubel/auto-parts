namespace AutoParts.Data.Model.Projections
{
    public class ManufacturerProjection
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public long CountryId { get; set; }

        public string CountryName { get; set; }
    }
}

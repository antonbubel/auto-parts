namespace AutoParts.Data.Model.Entities
{
    using Base;

    public class Manufacturer : BaseEntity<long>
    {
        public string Name { get; set; }

        public string NormalizedName { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public long CountryId { get; set; }

        public Country Country { get; }
    }
}

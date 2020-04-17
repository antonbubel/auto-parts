namespace AutoParts.Data.Model.Entities
{
    using Base;

    public class AutoPartsCatalogSubGroup : BaseEntity<long>
    {
        public string Name { get; set; }

        public long AutoPartsCatalogGroupId { get; set; }

        public AutoPartsCatalogGroup AutoPartsCatalogGroup { get; set; }
    }
}

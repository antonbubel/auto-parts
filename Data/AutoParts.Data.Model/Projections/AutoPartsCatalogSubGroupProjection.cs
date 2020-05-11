namespace AutoParts.Data.Model.Projections
{
    public class AutoPartsCatalogSubGroupProjection
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long AutoPartsCatalogGroupId { get; set; }
    }
}

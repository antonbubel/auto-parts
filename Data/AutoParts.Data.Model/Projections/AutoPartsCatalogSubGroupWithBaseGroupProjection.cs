namespace AutoParts.Data.Model.Projections
{
    public class AutoPartsCatalogSubGroupWithBaseGroupProjection
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long AutoPartsCatalogGroupId { get; set; }

        public string AutoPartsCatalogGroupName { get; set; }
    }
}

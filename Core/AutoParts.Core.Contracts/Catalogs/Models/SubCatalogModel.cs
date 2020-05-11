namespace AutoParts.Core.Contracts.Catalogs.Models
{
    public class SubCatalogModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public CatalogModel BaseCatalog { get; set; }
    }
}

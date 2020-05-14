namespace AutoParts.Data.Model.Entities
{
    using System.Collections.Generic;

    using Base;

    public class AutoPartsCatalogSubGroup : BaseEntity<long>
    {
        public string Name { get; set; }

        public long AutoPartsCatalogGroupId { get; set; }

        public AutoPartsCatalogGroup AutoPartsCatalogGroup { get; set; }

        public virtual ICollection<AutoPart> AutoParts { get; set; }
    }
}

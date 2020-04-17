namespace AutoParts.Data.Model.Entities
{
    using Base;

    using System.Collections.Generic;

    public class AutoPartsCatalogGroup : BaseEntity<long>
    {
        public string Name { get; set; }

        public virtual ICollection<AutoPartsCatalogSubGroup> AutoPartsCatalogSubGroups { get; set; }
    }
}

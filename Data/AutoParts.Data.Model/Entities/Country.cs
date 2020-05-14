namespace AutoParts.Data.Model.Entities
{
    using System.Collections.Generic;

    using Base;

    public class Country : LookupEntity<long>
    {
        public virtual ICollection<Manufacturer> Manufacturers { get; set; }

        public virtual ICollection<AutoPart> AutoParts { get; set; }
    }
}

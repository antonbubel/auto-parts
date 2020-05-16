namespace AutoParts.Data.Model.Entities
{
    using System.Collections.Generic;

    using Base;

    public class Country : LookupEntity<long>
    {
        public bool IsShippingCountry { get; set; }

        public virtual ICollection<Manufacturer> Manufacturers { get; set; }

        public virtual ICollection<AutoPart> AutoParts { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<UserShippingInfo> UserShippingInfos { get; set; }
    }
}

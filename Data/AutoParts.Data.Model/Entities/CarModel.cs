namespace AutoParts.Data.Model.Entities
{
    using Base;

    using System.Collections.Generic;

    public class CarModel : BaseEntity<long>
    {
        public long CarBrandId { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public virtual CarBrand CarBrand { get; set; }

        public virtual ICollection<CarModification> CarModifications { get; set; } 
    }
}

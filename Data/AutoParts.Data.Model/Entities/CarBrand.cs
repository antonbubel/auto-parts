namespace AutoParts.Data.Model.Entities
{
    using Base;

    using System.Collections.Generic;

    public class CarBrand : BaseEntity<long>
    {
        public string Name { get; set; }

        public string Image { get; set; }

        public virtual ICollection<CarModel> CarModels { get; set; }
    }
}

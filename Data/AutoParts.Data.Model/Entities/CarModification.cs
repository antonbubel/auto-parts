namespace AutoParts.Data.Model.Entities
{
    using System.Collections.Generic;

    using Base;

    public class CarModification : BaseEntity<long>
    {
        public long CarModelId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public virtual CarModel CarModel { get; set; }

        public virtual ICollection<AutoPart> AutoParts { get; set; }

        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}

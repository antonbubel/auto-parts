namespace AutoParts.Data.Model.Entities
{
    using System.Collections.Generic;

    using Base;

    public class UserProfile : BaseEntity<long>
    {
        public User User { get; set; }

        public long? CarModificationId { get; set; }

        public CarModification CarModification { get; set; }

        public virtual ICollection<UserShippingInfo> UserShippingInfos { get; set; }
    }
}

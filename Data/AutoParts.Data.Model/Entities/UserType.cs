namespace AutoParts.Data.Model.Entities
{
    using System.Collections.Generic;

    using Base;

    using UserTypeEnum = Enums.UserType;

    public class UserType : LookupEntity<UserTypeEnum>
    {
        public virtual ICollection<User> Users { get; set; }
    }
}

namespace AutoParts.Data.Model.Entities
{
    using Microsoft.AspNetCore.Identity;
    
    using System.Collections.Generic;

    using UserTypeEnum = Enums.UserType;

    public class User : IdentityUser<long>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public UserTypeEnum UserTypeId { get; set; }

        public virtual UserType UserType { get; set; }

        public virtual SupplierProfile SupplierProfile { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}

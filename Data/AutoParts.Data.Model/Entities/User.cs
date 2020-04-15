namespace AutoParts.Data.Model.Entities
{
    using Microsoft.AspNetCore.Identity;

    using UserTypeEnum = Enums.UserType;

    public class User : IdentityUser<long>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public UserTypeEnum UserTypeId { get; set; }

        public virtual UserType UserType { get; set; }
    }
}

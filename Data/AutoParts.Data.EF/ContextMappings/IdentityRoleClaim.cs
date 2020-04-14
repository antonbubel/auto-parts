namespace AutoParts.Data.EF.ContextMappings
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Identity;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using UserTypeEnum = Model.Enums.UserType;

    public class IdentityRoleClaimMap : IEntityTypeConfiguration<IdentityRoleClaim<long>>
    {
        public void Configure(EntityTypeBuilder<IdentityRoleClaim<long>> builder)
        {
            builder.HasData(
                new IdentityRoleClaim<long>
                {
                    Id = 1,
                    ClaimType = ClaimTypes.Role,
                    ClaimValue = UserTypeEnum.User.ToString(),
                    RoleId = (long)UserTypeEnum.User
                }
            );
        }
    }
}

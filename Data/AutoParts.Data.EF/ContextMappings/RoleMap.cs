namespace AutoParts.Data.EF.ContextMappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Model.Entities;
    using UserTypeEnum = Model.Enums.UserType;

    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role
                {
                    Id = (long)UserTypeEnum.User,
                    Name = UserTypeEnum.User.ToString(),
                    NormalizedName = UserTypeEnum.User.ToString().ToUpperInvariant()
                }
            );
        }
    }
}

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
                },
                new Role
                {
                    Id = (long)UserTypeEnum.Supplier,
                    Name = UserTypeEnum.Supplier.ToString(),
                    NormalizedName = UserTypeEnum.Supplier.ToString().ToUpperInvariant()
                },
                new Role
                {
                    Id = (long)UserTypeEnum.Administrator,
                    Name = UserTypeEnum.Administrator.ToString(),
                    NormalizedName = UserTypeEnum.Administrator.ToString().ToUpperInvariant()
                }
            );
        }
    }
}

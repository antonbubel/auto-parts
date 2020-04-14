namespace AutoParts.Data.EF.ContextMappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Model.Entities;
    using UserTypeEnum = Model.Enums.UserType;

    public class UserTypeMap : IEntityTypeConfiguration<UserType>
    {
        public void Configure(EntityTypeBuilder<UserType> builder)
        {
            builder.Property(userType => userType.Id)
                .ValueGeneratedNever();

            builder.Property(userType => userType.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasData(
                new UserType { Id = UserTypeEnum.User, Name = UserTypeEnum.User.ToString() }
            );
        }
    }
}

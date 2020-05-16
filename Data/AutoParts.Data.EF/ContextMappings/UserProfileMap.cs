namespace AutoParts.Data.EF.ContextMappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Model.Entities;

    public class UserProfileMap : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.Property(userProfile => userProfile.Id)
                .ValueGeneratedNever();

            builder
                .HasOne(userProfile => userProfile.User)
                .WithOne(user => user.UserProfile)
                .HasForeignKey<UserProfile>(userProfile => userProfile.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(userProfile => userProfile.CarModification)
                .WithMany(carModification => carModification.UserProfiles)
                .HasForeignKey(userProfile => userProfile.CarModificationId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

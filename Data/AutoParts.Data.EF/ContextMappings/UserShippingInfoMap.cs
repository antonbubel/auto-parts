namespace AutoParts.Data.EF.ContextMappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Model.Entities;

    public class UserShippingInfoMap : IEntityTypeConfiguration<UserShippingInfo>
    {
        public void Configure(EntityTypeBuilder<UserShippingInfo> builder)
        {
            builder
                .Property(userShippingInfo => userShippingInfo.StreetAddress)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .Property(userShippingInfo => userShippingInfo.StreetAddressSecondLine)
                .HasMaxLength(200);

            builder
                .Property(userShippingInfo => userShippingInfo.City)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(userShippingInfo => userShippingInfo.Region)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(userShippingInfo => userShippingInfo.ZipCode)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .HasOne(userShippingInfo => userShippingInfo.Country)
                .WithMany(country => country.UserShippingInfos)
                .HasForeignKey(userShippingInfo => userShippingInfo.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(userShippingInfo => userShippingInfo.UserProfile)
                .WithMany(userProfile => userProfile.UserShippingInfos)
                .HasForeignKey(userShippingInfo => userShippingInfo.UserProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

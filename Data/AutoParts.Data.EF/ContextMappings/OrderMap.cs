namespace AutoParts.Data.EF.ContextMappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Model.Entities;

    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .Property(order => order.StreetAddress)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .Property(order => order.StreetAddressSecondLine)
                .HasMaxLength(200);

            builder
                .Property(order => order.City)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(order => order.Region)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(order => order.ZipCode)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .HasOne(order => order.Country)
                .WithMany(country => country.Orders)
                .HasForeignKey(order => order.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(order => order.User)
                .WithMany(user => user.Orders)
                .HasForeignKey(order => order.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(order => order.OrderStatus)
                .WithMany(orderStatus => orderStatus.Orders)
                .HasForeignKey(order => order.OrderStatusId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

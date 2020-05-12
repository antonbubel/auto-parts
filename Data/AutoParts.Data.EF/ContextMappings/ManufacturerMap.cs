namespace AutoParts.Data.EF.ContextMappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Model.Entities;

    public class ManufacturerMap : IEntityTypeConfiguration<Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            builder
                .Property(manufacturer => manufacturer.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(manufacturer => manufacturer.Description)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .HasOne(manufacturer => manufacturer.Country)
                .WithMany(country => country.Manufacturers)
                .HasForeignKey(manufacturer => manufacturer.CountryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

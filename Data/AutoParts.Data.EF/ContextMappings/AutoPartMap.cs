namespace AutoParts.Data.EF.ContextMappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Model.Entities;

    public class AutoPartMap : IEntityTypeConfiguration<AutoPart>
    {
        public void Configure(EntityTypeBuilder<AutoPart> builder)
        {
            builder
                .Property(autoPart => autoPart.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(autoPart => autoPart.Description)
                .HasMaxLength(200);

            builder
                .HasOne(autoPart => autoPart.Manufacturer)
                .WithMany(manufacturer => manufacturer.AutoParts)
                .HasForeignKey(autoPart => autoPart.ManufacturerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(autoPart => autoPart.Country)
                .WithMany(country => country.AutoParts)
                .HasForeignKey(autoPart => autoPart.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(autoPart => autoPart.Supplier)
                .WithMany(supplier => supplier.AutoParts)
                .HasForeignKey(autoPart => autoPart.SupplierId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(autoPart => autoPart.CarModification)
                .WithMany(carModification => carModification.AutoParts)
                .HasForeignKey(autoPart => autoPart.CarModificationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(autoPart => autoPart.AutoPartsCatalogSubGroup)
                .WithMany(autoPartsCatalogSubGroup => autoPartsCatalogSubGroup.AutoParts)
                .HasForeignKey(autoPart => autoPart.AutoPartsCatalogSubGroupId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

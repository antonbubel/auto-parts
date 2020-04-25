namespace AutoParts.Data.EF.ContextMappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Model.Entities;

    public class SupplierProfileMap : IEntityTypeConfiguration<SupplierProfile>
    {
        public void Configure(EntityTypeBuilder<SupplierProfile> builder)
        {
            builder.Property(supplierProfile => supplierProfile.Id)
                .ValueGeneratedNever();

            builder
                .HasOne(supplierProfile => supplierProfile.User)
                .WithOne(user => user.SupplierProfile)
                .HasForeignKey<SupplierProfile>(supplierProfile => supplierProfile.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(supplierProfile => supplierProfile.OrganizationName)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(supplierProfile => supplierProfile.OrganizationAddress)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .Property(supplierProfile => supplierProfile.OrganizationDescription)
                .HasMaxLength(500)
                .IsRequired();

            builder
                .Property(supplierProfile => supplierProfile.Website)
                .HasMaxLength(50);
        }
    }
}

namespace AutoParts.Data.EF.ContextMappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Model.Entities;

    public class SupplierInvitationMap : IEntityTypeConfiguration<SupplierInitation>
    {
        public void Configure(EntityTypeBuilder<SupplierInitation> builder)
        {
            builder
                .Property(supplierInvitation => supplierInvitation.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(supplierInitation => supplierInitation.Email)
                .IsRequired();

            builder
                .Property(supplierInitation => supplierInitation.Token)
                .IsRequired();
        }
    }
}

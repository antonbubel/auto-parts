namespace AutoParts.Data.EF.ContextMappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Model.Entities;

    public class SupplierInvitationMap : IEntityTypeConfiguration<SupplierInvitation>
    {
        public void Configure(EntityTypeBuilder<SupplierInvitation> builder)
        {
            builder
                .Property(supplierInvitation => supplierInvitation.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(supplierInvitation => supplierInvitation.Email)
                .IsRequired();

            builder
                .Property(supplierInvitation => supplierInvitation.Token)
                .IsRequired();
        }
    }
}

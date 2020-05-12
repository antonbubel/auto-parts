namespace AutoParts.Data.EF.ContextMappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Model.Entities;

    public class CountryMap : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder
                .Property(supplierInvitation => supplierInvitation.Name)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}

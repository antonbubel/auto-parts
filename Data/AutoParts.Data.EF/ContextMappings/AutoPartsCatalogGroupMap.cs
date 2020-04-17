namespace AutoParts.Data.EF.ContextMappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Model.Entities;

    public class AutoPartsCatalogGroupMapBrandMap : IEntityTypeConfiguration<AutoPartsCatalogGroup>
    {
        public void Configure(EntityTypeBuilder<AutoPartsCatalogGroup> builder)
        {
            builder
                .Property(catalogGroup => catalogGroup.Name)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}

namespace AutoParts.Data.EF.ContextMappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Model.Entities;

    public class AutoPartsCatalogSubGroupMapBrandMap : IEntityTypeConfiguration<AutoPartsCatalogSubGroup>
    {
        public void Configure(EntityTypeBuilder<AutoPartsCatalogSubGroup> builder)
        {
            builder
                .Property(catalogSubGroup => catalogSubGroup.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .HasOne(catalogSubGroup => catalogSubGroup.AutoPartsCatalogGroup)
                .WithMany(catalogGroup => catalogGroup.AutoPartsCatalogSubGroups)
                .HasForeignKey(catalogSubGroup => catalogSubGroup.AutoPartsCatalogGroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

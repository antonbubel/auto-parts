namespace AutoParts.Data.EF.ContextMappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Model.Entities;

    public class CarModificationMap : IEntityTypeConfiguration<CarModification>
    {
        public void Configure(EntityTypeBuilder<CarModification> builder)
        {
            builder
                .Property(carModification => carModification.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(carModification => carModification.Description)
                .HasMaxLength(200);

            builder
                .HasOne(carModification => carModification.CarModel)
                .WithMany(carModel => carModel.CarModifications)
                .HasForeignKey(carModification => carModification.CarModelId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

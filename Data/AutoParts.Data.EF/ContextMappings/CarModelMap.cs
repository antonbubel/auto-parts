namespace AutoParts.Data.EF.ContextMappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Model.Entities;

    public class CarModelMap : IEntityTypeConfiguration<CarModel>
    {
        public void Configure(EntityTypeBuilder<CarModel> builder)
        {
            builder
                .Property(carModel => carModel.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .HasOne(carModel => carModel.CarBrand)
                .WithMany(carBrand => carBrand.CarModels)
                .HasForeignKey(carModel => carModel.CarBrandId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

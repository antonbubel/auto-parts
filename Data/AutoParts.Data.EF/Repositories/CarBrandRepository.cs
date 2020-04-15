namespace AutoParts.Data.EF.Repositories
{
    using Base;

    using Model.Entities;
    using Model.Repositories;

    public class CarBrandRepository : Repository<long, CarBrand>, ICarBrandRepository
    {
        public CarBrandRepository(IDatabaseContext context) : base(context)
        {
        }
    }
}

namespace AutoParts.Data.EF.Repositories
{
    using Base;

    using Model.Entities;
    using Model.Repositories;

    public class CarModelRepository : Repository<long, CarModel>, ICarModelRepository
    {
        public CarModelRepository(IDatabaseContext context) : base(context)
        {
        }
    }
}

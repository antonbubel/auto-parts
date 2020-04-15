namespace AutoParts.Data.EF.Repositories
{
    using Base;

    using Model.Entities;
    using Model.Repositories;

    public class CarModificationRepository : Repository<long, CarModification>, ICarModificationRepository
    {
        public CarModificationRepository(IDatabaseContext context) : base(context)
        {
        }
    }
}

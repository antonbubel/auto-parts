namespace AutoParts.Data.EF.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Base;

    using Model.Entities;
    using Model.Repositories;

    public class CarModificationRepository : Repository<long, CarModification>, ICarModificationRepository
    {
        public CarModificationRepository(IDatabaseContext context) : base(context)
        {
        }

        public Task<CarModification[]> GetCarModificationsByCarModel(long carModelId)
        {
            return GetQueryable()
                .Where(carModification => carModification.CarModelId == carModelId)
                .ToArrayAsync();
        }

        public Task<CarModification[]> GetCarModificationsByCarModelAndYear(long carModelId, int year)
        {
            return GetQueryable()
                .Where(carModification => carModification.CarModelId == carModelId && carModification.Year == year)
                .ToArrayAsync();
        }
    }
}

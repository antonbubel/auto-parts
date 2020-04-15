namespace AutoParts.Data.EF.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Base;
    
    using Model.Entities;
    using Model.Repositories;

    public class CarModelRepository : Repository<long, CarModel>, ICarModelRepository
    {
        public CarModelRepository(IDatabaseContext context) : base(context)
        {
        }

        public Task<CarModel[]> GetCarModelsByBrand(long carBrandId)
        {
            return GetQueryable()
                .Include(carModel => carModel.CarBrand)
                .Where(carModel => carModel.CarBrandId == carBrandId)
                .ToArrayAsync();
        }
    }
}

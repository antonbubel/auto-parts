namespace AutoParts.Data.EF.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using AutoParts.Data.EF.Repositories.Base;
    
    using Model.Entities;
    using Model.Repositories;

    public class CarModelRepository : Repository<long, CarModel>, ICarModelRepository
    {
        public CarModelRepository(IDatabaseContext context) : base(context)
        {
        }

        public override async Task<CarModel> FindAsync(long key)
        {
            return await GetQueryable()
                .Include(carModel => carModel.CarBrand)
                .FirstOrDefaultAsync(carModel => carModel.Id == key);
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

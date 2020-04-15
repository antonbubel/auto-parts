namespace AutoParts.Data.EF.Repositories
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Base;

    using Model.Entities;
    using Model.Repositories;

    public class CarBrandRepository : Repository<long, CarBrand>, ICarBrandRepository
    {
        public CarBrandRepository(IDatabaseContext context) : base(context)
        {
        }

        public Task<bool> CarBrandWithNameExists(string name)
        {
            return GetQueryable()
                .AnyAsync(carBrand => carBrand.Name == name);
        }
    }
}

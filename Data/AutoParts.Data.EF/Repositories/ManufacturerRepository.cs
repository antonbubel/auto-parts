namespace AutoParts.Data.EF.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Base;
    
    using Model.Entities;
    using Model.Repositories;

    public class ManufacturerRepository : Repository<long, Manufacturer>, IManufacturerRepository
    {
        public ManufacturerRepository(IDatabaseContext context) : base(context)
        {
        }

        public async Task<Manufacturer[]> GetManufacturersByCountry(long countryId)
        {
            return await GetQueryable()
                .Where(manufacturer => manufacturer.CountryId == countryId)
                .ToArrayAsync();
        }
    }
}

namespace AutoParts.Data.EF.Repositories
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Base;
    
    using Model.Entities;
    using Model.Projections;
    using Model.Repositories;

    public class ManufacturerRepository : Repository<long, Manufacturer>, IManufacturerRepository
    {
        private readonly IMapper mapper;

        public ManufacturerRepository(IDatabaseContext context, IMapper mapper) : base(context)
        {
            this.mapper = mapper;
        }

        public async Task<ManufacturerProjection[]> GetManufacturersByCountry(long countryId)
        {
            return await GetQueryable()
                .Where(manufacturer => manufacturer.CountryId == countryId)
                .ProjectTo<ManufacturerProjection>(mapper.ConfigurationProvider)
                .ToArrayAsync();
        }
    }
}

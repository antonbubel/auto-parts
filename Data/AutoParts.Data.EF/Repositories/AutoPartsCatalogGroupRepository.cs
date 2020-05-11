namespace AutoParts.Data.EF.Repositories
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Base;

    using Model.Entities;
    using Model.Projections;
    using Model.Repositories;

    public class AutoPartsCatalogGroupRepository : Repository<long, AutoPartsCatalogGroup>, IAutoPartsCatalogGroupRepository
    {
        private readonly IMapper mapper;

        public AutoPartsCatalogGroupRepository(IDatabaseContext context, IMapper mapper) : base(context)
        {
            this.mapper = mapper;
        }

        public async Task<AutoPartsCatalogGroupProjection[]> GetAutoPartsCatalogGroups()
        {
            return await GetQueryable()
                .ProjectTo<AutoPartsCatalogGroupProjection>(mapper.ConfigurationProvider)
                .ToArrayAsync();
        }
    }
}

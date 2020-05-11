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

    public class AutoPartsCatalogSubGroupRepository : Repository<long, AutoPartsCatalogSubGroup>, IAutoPartsCatalogSubGroupRepository
    {
        private readonly IMapper mapper;

        public AutoPartsCatalogSubGroupRepository(IDatabaseContext context, IMapper mapper) : base(context)
        {
            this.mapper = mapper;
        }

        public async Task<AutoPartsCatalogSubGroupWithBaseGroupProjection> GetAutoPartsCatalogSubGroupById(long catalogSubGroupId)
        {
            return await GetQueryable()
                .Where(subGroup => subGroup.Id == catalogSubGroupId)
                .ProjectTo<AutoPartsCatalogSubGroupWithBaseGroupProjection>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<AutoPartsCatalogSubGroupProjection[]> GetAutoPartsCatalogGroupSubGroups(long catalogGroupId)
        {
            return await GetQueryable()
                .Where(subGroup => subGroup.AutoPartsCatalogGroupId == catalogGroupId)
                .ProjectTo<AutoPartsCatalogSubGroupProjection>(mapper.ConfigurationProvider)
                .ToArrayAsync();
        }
    }
}

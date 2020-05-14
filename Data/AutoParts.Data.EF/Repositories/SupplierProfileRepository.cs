namespace AutoParts.Data.EF.Repositories
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Base;

    using Model.Results;
    using Model.Entities;
    using Model.Projections;
    using Model.Repositories;

    public class SupplierProfileRepository : Repository<long, SupplierProfile>, ISupplierProfileRepository
    {
        private readonly IMapper mapper;

        public SupplierProfileRepository(IMapper mapper, IDatabaseContext context) : base(context)
        {
            this.mapper = mapper;
        }

        public override async Task<SupplierProfile> FindAsync(long key)
        {
            return await GetQueryable()
                .Include(supplierProfile => supplierProfile.User)
                .FirstOrDefaultAsync(supplierProfile => supplierProfile.Id == key);
        }

        public async Task<PaginatedResult<ShortSupplierProfileProjection>> GetSuppliers(int itemsToSkip, int itemsToTake)
        {
            var items = await GetQueryable()
                .OrderBy(supplier => supplier.Id)
                .GetPartition(itemsToSkip, itemsToTake)
                .ProjectTo<ShortSupplierProfileProjection>(mapper.ConfigurationProvider)
                .ToArrayAsync();

            var totalNumberOfItems = await GetQueryable()
                .CountAsync();

            return new PaginatedResult<ShortSupplierProfileProjection>
            {
                Items = items,
                TotalNumberOfItems = totalNumberOfItems
            };
        }
    }
}

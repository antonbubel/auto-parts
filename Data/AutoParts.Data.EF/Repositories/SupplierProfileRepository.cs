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

        public async Task<ShortSupplierProfileProjection[]> GetSuppliers(int itemsToSkip, int itemsToTake)
        {
            return await GetQueryable()
                .OrderBy(supplier => supplier.Id)
                .Skip(itemsToSkip)
                .Take(itemsToTake)
                .ProjectTo<ShortSupplierProfileProjection>(mapper.ConfigurationProvider)
                .ToArrayAsync();
        }
    }
}

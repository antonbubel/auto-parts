namespace AutoParts.Data.EF.Repositories
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Base;

    using Model.Entities;
    using Model.Repositories;

    public class SupplierProfileRepository : Repository<long, SupplierProfile>, ISupplierProfileRepository
    {
        public SupplierProfileRepository(IDatabaseContext context) : base(context)
        {
        }

        public override async Task<SupplierProfile> FindAsync(long key)
        {
            return await GetQueryable()
                .Include(supplierProfile => supplierProfile.User)
                .FirstOrDefaultAsync(supplierProfile => supplierProfile.Id == key);
        }
    }
}

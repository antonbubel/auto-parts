namespace AutoParts.Data.EF.Repositories
{
    using Base;

    using Model.Entities;
    using Model.Repositories;

    public class SupplierProfileRepository : Repository<long, SupplierProfile>, ISupplierProfileRepository
    {
        public SupplierProfileRepository(IDatabaseContext context) : base(context)
        {
        }
    }
}

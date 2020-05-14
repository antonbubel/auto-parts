namespace AutoParts.Data.Model.Repositories
{
    using System.Threading.Tasks;

    using Base;
    using Results;
    using Entities;
    using Projections;

    public interface ISupplierProfileRepository : IRepository<long, SupplierProfile>
    {
        Task<PaginatedResult<ShortSupplierProfileProjection>> GetSuppliers(int itemsToSkip, int itemsToTake);
    }
}

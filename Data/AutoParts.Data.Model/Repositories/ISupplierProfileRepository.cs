namespace AutoParts.Data.Model.Repositories
{
    using System.Threading.Tasks;

    using Base;
    using Entities;
    using Projections;

    public interface ISupplierProfileRepository : IRepository<long, SupplierProfile>
    {
        Task<ShortSupplierProfileProjection[]> GetSuppliers(int itemsToSkip, int itemsToTake);
    }
}

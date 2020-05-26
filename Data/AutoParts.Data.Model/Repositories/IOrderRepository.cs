namespace AutoParts.Data.Model.Repositories
{
    using System.Threading.Tasks;

    using Base;
    using Results;
    using Entities;

    public interface IOrderRepository : IRepository<long, Order>
    {
        Task<PaginatedResult<Order>> GetUserOrders(int itemsToSkip, int itemsToTake, long userId);

        Task<PaginatedResult<Order>> GetSupplierOrders(int itemsToSkip, int itemsToTake, long supplierId);
    }
}

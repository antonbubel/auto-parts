namespace AutoParts.Data.Model.Repositories
{
    using System.Threading.Tasks;

    using Base;
    using Results;
    using Entities;

    public interface IOrderRepository : IRepository<long, Order>
    {
        Task<PaginatedResult<Order>> GetOrders(int itemsToSkip, int itemsToTake);
    }
}

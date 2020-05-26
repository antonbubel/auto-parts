namespace AutoParts.Data.Model.Repositories
{
    using System.Threading.Tasks;

    using Base;
    using Entities;
    using Projections;

    public interface IOrderItemRepository : IRepository<long, OrderItem>
    {
        Task<OrderItemProjection[]> GetOrderItems(long orderId, long? supplierId = null);
    }
}

namespace AutoParts.Data.EF.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Base;

    using Model.Results;
    using Model.Entities;
    using Model.Repositories;

    public class OrderRepository : Repository<long, Order>, IOrderRepository
    {
        public OrderRepository(IDatabaseContext context) : base(context)
        {
        }

        public async Task<PaginatedResult<Order>> GetUserOrders(int itemsToSkip, int itemsToTake, long userId)
        {
            var items = await GetQueryable()
                .Include(order => order.Country)
                .Where(order => order.UserId == userId)
                .OrderByDescending(order => order.DateCreated)
                .ThenBy(order => order.Id)
                .GetPartition(itemsToSkip, itemsToTake)
                .ToArrayAsync();

            var totalNumberOfItems = await GetQueryable()
                .CountAsync();

            return new PaginatedResult<Order>
            {
                Items = items,
                TotalNumberOfItems = totalNumberOfItems
            };
        }

        public async Task<PaginatedResult<Order>> GetSupplierOrders(int itemsToSkip, int itemsToTake, long supplierId)
        {
            var items = await GetQueryable()
                .Include(order => order.Country)
                .Where(order => order.OrderItems.Any(orderItem => orderItem.AutoPart.SupplierId == supplierId))
                .OrderByDescending(order => order.DateCreated)
                .ThenBy(order => order.Id)
                .GetPartition(itemsToSkip, itemsToTake)
                .ToArrayAsync();

            var totalNumberOfItems = await GetQueryable()
                .CountAsync();

            return new PaginatedResult<Order>
            {
                Items = items,
                TotalNumberOfItems = totalNumberOfItems
            };
        }
    }
}

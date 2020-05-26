namespace AutoParts.Data.EF.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using AutoParts.Data.EF.Repositories.Base;

    using Model.Results;
    using Model.Entities;
    using Model.Repositories;

    public class OrderRepository : Repository<long, Order>, IOrderRepository
    {
        public OrderRepository(IDatabaseContext context) : base(context)
        {
        }

        public override Task<Order> FindAsync(long key)
        {
            return GetQueryable()
                .Include(order => order.Country)
                .FirstOrDefaultAsync(order => order.Id == key);
        }

        public async Task<PaginatedResult<Order>> GetUserOrders(int itemsToSkip, int itemsToTake, long userId)
        {
            var query = GetQueryable()
                .Include(order => order.Country)
                .Where(order => order.UserId == userId);

            var items = await query
                .OrderByDescending(order => order.DateCreated)
                .ThenBy(order => order.Id)
                .GetPartition(itemsToSkip, itemsToTake)
                .ToArrayAsync();

            var totalNumberOfItems = await query
                .CountAsync();

            return new PaginatedResult<Order>
            {
                Items = items,
                TotalNumberOfItems = totalNumberOfItems
            };
        }

        public async Task<PaginatedResult<Order>> GetSupplierOrders(int itemsToSkip, int itemsToTake, long supplierId)
        {
            var query = GetQueryable()
                .Include(order => order.Country)
                .Where(order => order.OrderItems.Any(orderItem => orderItem.AutoPart.SupplierId == supplierId));

            var items = await query
                .OrderByDescending(order => order.DateCreated)
                .ThenBy(order => order.Id)
                .GetPartition(itemsToSkip, itemsToTake)
                .ToArrayAsync();

            var totalNumberOfItems = await query
                .CountAsync();

            return new PaginatedResult<Order>
            {
                Items = items,
                TotalNumberOfItems = totalNumberOfItems
            };
        }
    }
}

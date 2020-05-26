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

        public async Task<PaginatedResult<Order>> GetOrders(int itemsToSkip, int itemsToTake)
        {
            var items = await GetQueryable()
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

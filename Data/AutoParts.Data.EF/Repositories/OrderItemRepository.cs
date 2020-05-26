namespace AutoParts.Data.EF.Repositories
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Base;

    using Model.Entities;
    using Model.Projections;
    using Model.Repositories;

    public class OrderItemRepository : Repository<long, OrderItem>, IOrderItemRepository
    {
        private readonly IMapper mapper;

        public OrderItemRepository(IMapper mapper, IDatabaseContext context) : base(context)
        {
            this.mapper = mapper;
        }

        public async Task<OrderItemProjection[]> GetOrderItems(long orderId, long? supplierId = null)
        {
            var query = GetQueryable()
                .Where(orderItem => orderItem.OrderId == orderId);

            if (supplierId.HasValue)
            {
                query = query
                    .Where(orderItem => orderItem.AutoPart.SupplierId == supplierId.Value);
            }

            return await query
                .ProjectTo<OrderItemProjection>(mapper.ConfigurationProvider)
                .ToArrayAsync();
        }
    }
}

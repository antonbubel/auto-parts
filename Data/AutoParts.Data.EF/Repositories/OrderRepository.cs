namespace AutoParts.Data.EF.Repositories
{
    using Base;

    using Model.Entities;
    using Model.Repositories;

    public class OrderRepository : Repository<long, Order>, IOrderRepository
    {
        public OrderRepository(IDatabaseContext context) : base(context)
        {
        }
    }
}

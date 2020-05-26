namespace AutoParts.Core.Contracts.Orders.Requests
{
    using MediatR;

    using Models;
    using Common.Models;

    public class GetSupplierOrdersRequest : PaginationFilterModel, IRequest<PageModel<OrderModel>>
    {
        public long SupplierId { get; set; }
    }
}

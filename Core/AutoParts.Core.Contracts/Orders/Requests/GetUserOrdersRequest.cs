namespace AutoParts.Core.Contracts.Orders.Requests
{
    using MediatR;

    using Models;
    using Common.Models;

    public class GetUserOrdersRequest : PaginationFilterModel, IRequest<PageModel<OrderModel>>
    {
        public long UserId { get; set; }
    }
}

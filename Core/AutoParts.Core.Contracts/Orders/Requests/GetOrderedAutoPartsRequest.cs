namespace AutoParts.Core.Contracts.Orders.Requests
{
    using MediatR;

    using Models;

    public class GetOrderedAutoPartsRequest : IRequest<OrderAutoPartModel[]>
    {
        public long OrderId { get; set; }

        public long UserId { get; set; }
    }
}

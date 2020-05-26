namespace AutoParts.Core.Contracts.Orders.Requests
{
    using MediatR;

    using Models;

    public class GetOrderByIdRequest : IRequest<OrderModel>
    {
        public long OrderId { get; set; }

        public long UserId { get; set; }
    }
}

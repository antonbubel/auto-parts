namespace AutoParts.Web.Server.MappingProfiles
{
    using AutoMapper;

    using Protos;

    using Core.Contracts.Orders.Models;
    using Core.Contracts.Orders.Notifications;

    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<CreateOrderRequest, CreateOrderNotification>()
                .ForMember(notification => notification.UserId, conf => conf.Ignore());

            CreateMap<OrderItem, OrderItemModel>();
        }
    }
}

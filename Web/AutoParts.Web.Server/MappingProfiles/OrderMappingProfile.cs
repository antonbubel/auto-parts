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

            CreateMap<OrderModel, Order>()
                .ForMember(order => order.StreetAddressSecondLine, conf => conf.MapFrom(model => model.StreetAddressSecondLine ?? string.Empty));

            CreateMap<OrderAutoPartModel, OrderAutoPart>()
                .ForMember(autoPart => autoPart.ImageUrl, conf => conf.MapFrom(model => model.ImageUrl ?? string.Empty));
        }
    }
}

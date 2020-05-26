namespace AutoParts.Core.Implementation.Orders.MappingProfiles
{
    using AutoMapper;

    using System;

    using Contracts.Orders.Models;
    using Contracts.Orders.Notifications;

    using Data.Model.Entities;
    using Data.Model.Projections;
    using OrderStatusEnum = Data.Model.Enums.OrderStatus;

    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<CreateOrderNotification, Order>()
                .ForMember(order => order.Id, conf => conf.Ignore())
                .ForMember(order => order.Email, conf => conf.MapFrom(notification => notification.Email))
                .ForMember(order => order.FirstName, conf => conf.MapFrom(notification => notification.FirstName))
                .ForMember(order => order.LastName, conf => conf.MapFrom(notification => notification.LastName))
                .ForMember(order => order.StreetAddress, conf => conf.MapFrom(notification => notification.StreetAddress))
                .ForMember(order => order.StreetAddressSecondLine, conf => conf.MapFrom(notification => notification.StreetAddressSecondLine))
                .ForMember(order => order.City, conf => conf.MapFrom(notification => notification.City))
                .ForMember(order => order.Region, conf => conf.MapFrom(notification => notification.Region))
                .ForMember(order => order.ZipCode, conf => conf.MapFrom(notification => notification.ZipCode))
                .ForMember(order => order.Comment, conf => conf.MapFrom(notification => notification.Comment))
                .ForMember(order => order.CountryId, conf => conf.MapFrom(notification => notification.CountryId))
                .ForMember(order => order.FirstName, conf => conf.MapFrom(notification => notification.FirstName))
                .ForMember(order => order.OrderItems, conf => conf.MapFrom(notification => notification.OrderItems))
                .ForMember(order => order.DateCreated, conf => conf.MapFrom(notification => DateTime.UtcNow))
                .ForMember(order => order.OrderStatusId, conf => conf.MapFrom(notification => OrderStatusEnum.Pending));

            CreateMap<OrderItemModel, OrderItem>()
                .ForMember(orderItem => orderItem.AutoPartId, conf => conf.MapFrom(model => model.AutoPartId))
                .ForMember(orderItem => orderItem.Quantity, conf => conf.MapFrom(model => model.Quantity))
                .ForMember(orderItem => orderItem.OrderId, conf => conf.Ignore());

            CreateMap<OrderItemProjection, OrderAutoPartModel>();

            CreateMap<Order, OrderModel>()
                .ForMember(orderModel => orderModel.CountryName, conf => conf.MapFrom(order => order.Country.Name));
        }
    }
}

namespace AutoParts.Data.EF.MappingProfiles
{
    using AutoMapper;

    using Model.Entities;
    using Model.Projections;

    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<OrderItem, OrderItemProjection>()
                .ForMember(projection => projection.AutoPartId, conf => conf.MapFrom(entity => entity.AutoPartId))
                .ForMember(projection => projection.Quantity, conf => conf.MapFrom(entity => entity.Quantity))
                .ForMember(projection => projection.Name, conf => conf.MapFrom(entity => entity.AutoPart.Name))
                .ForMember(projection => projection.Description, conf => conf.MapFrom(entity => entity.AutoPart.Description))
                .ForMember(projection => projection.Price, conf => conf.MapFrom(entity => entity.AutoPart.Price))
                .ForMember(projection => projection.Image, conf => conf.MapFrom(entity => entity.AutoPart.Image))
                .ForMember(projection => projection.SupplierId, conf => conf.MapFrom(entity => entity.AutoPart.SupplierId))
                .ForMember(projection => projection.SupplierName, conf => conf.MapFrom(entity => entity.AutoPart.Supplier.OrganizationName));
        }
    }
}

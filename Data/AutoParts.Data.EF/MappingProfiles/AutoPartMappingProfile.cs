namespace AutoParts.Data.EF.MappingProfiles
{
    using AutoMapper;

    using Model.Entities;
    using Model.Projections;

    public class AutoPartMappingProfile : Profile
    {
        public AutoPartMappingProfile()
        {
            CreateMap<AutoPart, AutoPartProjection>()
                .ForMember(projection => projection.Id, conf => conf.MapFrom(model => model.Id))
                .ForMember(projection => projection.Name, conf => conf.MapFrom(model => model.Description))
                .ForMember(projection => projection.Image, conf => conf.MapFrom(model => model.Image))
                .ForMember(projection => projection.Quantity, conf => conf.MapFrom(model => model.Quantity))
                .ForMember(projection => projection.Price, conf => conf.MapFrom(model => model.Price))
                .ForMember(projection => projection.IsAvailable, conf => conf.MapFrom(model => model.IsAvailable))
                .ForMember(projection => projection.CountryId, conf => conf.MapFrom(model => model.CountryId))
                .ForMember(projection => projection.ManufacturerId, conf => conf.MapFrom(model => model.ManufacturerId))
                .ForMember(projection => projection.ManufacturerName, conf => conf.MapFrom(model => model.Manufacturer.Name))
                .ForMember(projection => projection.SupplierId, conf => conf.MapFrom(model => model.SupplierId))
                .ForMember(projection => projection.SupplierName, conf => conf.MapFrom(model => model.Supplier.OrganizationName));
        }
    }
}

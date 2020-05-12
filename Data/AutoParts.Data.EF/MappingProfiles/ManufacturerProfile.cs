namespace AutoParts.Data.EF.MappingProfiles
{
    using AutoMapper;

    using Model.Entities;
    using Model.Projections;

    public class ManufacturerProfile : Profile
    {
        public ManufacturerProfile()
        {
            CreateMap<Manufacturer, ManufacturerProjection>()
                .ForMember(projection => projection.Id, conf => conf.MapFrom(entity => entity.Id))
                .ForMember(projection => projection.Name, conf => conf.MapFrom(entity => entity.Name))
                .ForMember(projection => projection.Description, conf => conf.MapFrom(entity => entity.Description))
                .ForMember(projection => projection.Image, conf => conf.MapFrom(entity => entity.Image))
                .ForMember(projection => projection.CountryId, conf => conf.MapFrom(entity => entity.CountryId))
                .ForMember(projection => projection.CountryName, conf => conf.MapFrom(entity => entity.Country.Name));
        }
    }
}

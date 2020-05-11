namespace AutoParts.Data.EF.MappingProfiles
{
    using AutoMapper;

    using Model.Entities;
    using Model.Projections;

    public class AutoPartsCatalogGroupMappingProfile : Profile
    {
        public AutoPartsCatalogGroupMappingProfile()
        {
            CreateMap<AutoPartsCatalogGroup, AutoPartsCatalogGroupProjection>()
                .ForMember(projection => projection.Id, conf => conf.MapFrom(entity => entity.Id))
                .ForMember(projection => projection.Name, conf => conf.MapFrom(entity => entity.Name));
        }
    }
}

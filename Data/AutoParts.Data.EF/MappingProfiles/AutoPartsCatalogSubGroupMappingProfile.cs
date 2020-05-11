namespace AutoParts.Data.EF.MappingProfiles
{
    using AutoMapper;

    using Model.Entities;
    using Model.Projections;

    public class AutoPartsCatalogSubGroupMappingProfile : Profile
    {
        public AutoPartsCatalogSubGroupMappingProfile()
        {
            CreateMap<AutoPartsCatalogSubGroup, AutoPartsCatalogSubGroupProjection>()
                .ForMember(projection => projection.Id, conf => conf.MapFrom(entity => entity.Id))
                .ForMember(projection => projection.Name, conf => conf.MapFrom(entity => entity.Name))
                .ForMember(projection => projection.AutoPartsCatalogGroupId, conf => conf.MapFrom(entity => entity.AutoPartsCatalogGroupId));

            CreateMap<AutoPartsCatalogSubGroup, AutoPartsCatalogSubGroupWithBaseGroupProjection>()
                .ForMember(projection => projection.Id, conf => conf.MapFrom(entity => entity.Id))
                .ForMember(projection => projection.Name, conf => conf.MapFrom(entity => entity.Name))
                .ForMember(projection => projection.AutoPartsCatalogGroupId, conf => conf.MapFrom(entity => entity.AutoPartsCatalogGroupId))
                .ForMember(projection => projection.AutoPartsCatalogGroupName, conf => conf.MapFrom(entity => entity.AutoPartsCatalogGroup.Name));
        }
    }
}

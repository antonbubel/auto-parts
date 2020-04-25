namespace AutoParts.Data.EF.MappingProfiles
{
    using AutoMapper;

    using Model.Entities;
    using Model.Projections;

    public class SupplierMappingProfile : Profile
    {
        public SupplierMappingProfile()
        {
            CreateMap<SupplierProfile, ShortSupplierProfileProjection>()
                .ForMember(projection => projection.Id, conf => conf.MapFrom(entity => entity.Id))
                .ForMember(projection => projection.OrganizationName, conf => conf.MapFrom(entity => entity.OrganizationName))
                .ForMember(projection => projection.Logo, conf => conf.MapFrom(entity => entity.Logo));
        }
    }
}

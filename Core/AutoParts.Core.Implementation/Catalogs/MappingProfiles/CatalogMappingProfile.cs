namespace AutoParts.Core.Implementation.Catalogs.MappingProfiles
{
    using AutoMapper;

    using Contracts.Catalogs.Models;

    using Data.Model.Projections;

    public class CatalogMappingProfile : Profile
    {
        public CatalogMappingProfile()
        {
            CreateMap<AutoPartsCatalogGroupProjection, CatalogModel>()
                .ForMember(catalogModel => catalogModel.Id, conf => conf.MapFrom(catalog => catalog.Id))
                .ForMember(catalogModel => catalogModel.Name, conf => conf.MapFrom(catalog => catalog.Name));
        }
    }
}

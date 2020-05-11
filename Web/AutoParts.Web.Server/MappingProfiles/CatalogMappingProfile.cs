namespace AutoParts.Web.Server.MappingProfiles
{
    using AutoMapper;

    using Protos;

    using Core.Contracts.Catalogs.Models;

    public class CatalogMappingProfile : Profile
    {
        public CatalogMappingProfile()
        {
            CreateMap<CatalogModel, Catalog>()
                .ForMember(catalog => catalog.Id, conf => conf.MapFrom(catalogModel => catalogModel.Id))
                .ForMember(catalog => catalog.Name, conf => conf.MapFrom(catalogModel => catalogModel.Name));

            CreateMap<SubCatalog, SubCatalog>()
                .ForMember(catalog => catalog.Id, conf => conf.MapFrom(catalogModel => catalogModel.Id))
                .ForMember(catalog => catalog.Name, conf => conf.MapFrom(catalogModel => catalogModel.Name))
                .ForMember(catalog => catalog.BaseCatalog, conf => conf.MapFrom(catalogModel => catalogModel.BaseCatalog));
        }
    }
}

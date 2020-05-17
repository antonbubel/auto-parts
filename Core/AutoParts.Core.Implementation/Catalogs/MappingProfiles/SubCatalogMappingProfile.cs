namespace AutoParts.Core.Implementation.Catalogs.MappingProfiles
{
    using AutoMapper;

    using Contracts.Catalogs.Models;

    using Data.Model.Projections;

    public class SubCatalogMappingProfile : Profile
    {
        public SubCatalogMappingProfile()
        {
            CreateMap<AutoPartsCatalogSubGroupProjection, CatalogModel>()
                .ForMember(catalogModel => catalogModel.Id, conf => conf.MapFrom(catalog => catalog.Id))
                .ForMember(catalogModel => catalogModel.Name, conf => conf.MapFrom(catalog => catalog.Name));

            CreateMap<AutoPartsCatalogSubGroupWithBaseGroupProjection, SubCatalogModel>()
                .ForMember(catalogModel => catalogModel.Id, conf => conf.MapFrom(catalog => catalog.Id))
                .ForMember(catalogModel => catalogModel.Name, conf => conf.MapFrom(catalog => catalog.Name))
                .ForMember(catalogModel => catalogModel.BaseCatalog, conf => conf.MapFrom(catalog => MapBaseCatalog(catalog)));

            CreateMap<AutoPartsCatalogSubGroupWithBaseGroupProjection, CatalogModel>()
                .ForMember(catalogModel => catalogModel.Id, conf => conf.MapFrom(catalog => catalog.Id))
                .ForMember(catalogModel => catalogModel.Name, conf => conf.MapFrom(catalog => catalog.Name));
        }

        private CatalogModel MapBaseCatalog(AutoPartsCatalogSubGroupWithBaseGroupProjection catalog)
        {
            return new CatalogModel
            {
                Id = catalog.AutoPartsCatalogGroupId,
                Name = catalog.AutoPartsCatalogGroupName
            };
        }
    }
}

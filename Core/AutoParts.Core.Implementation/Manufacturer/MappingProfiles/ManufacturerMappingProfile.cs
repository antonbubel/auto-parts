namespace AutoParts.Core.Implementation.Manufacturer.MappingProfiles
{
    using AutoMapper;

    using Contracts.Manufacturer.Models;
    using Contracts.Manufacturer.Notifications;

    using Data.Model.Entities;
    using Data.Model.Projections;

    public class ManufacturerMappingProfile : Profile
    {
        public ManufacturerMappingProfile()
        {
            CreateMap<ManufacturerProjection, ManufacturerModel>()
                .ForMember(model => model.Id, conf => conf.MapFrom(projection => projection.Id))
                .ForMember(model => model.Name, conf => conf.MapFrom(projection => projection.Name))
                .ForMember(model => model.Description, conf => conf.MapFrom(projection => projection.Description))
                .ForMember(model => model.ImageUrl, conf => conf.MapFrom(projection => projection.Image))
                .ForMember(model => model.CountryId, conf => conf.MapFrom(projection => projection.CountryId))
                .ForMember(model => model.CountryName, conf => conf.MapFrom(projection => projection.CountryName));

            CreateMap<CreateManufacturerNotification, Manufacturer>()
                .ForMember(manufacturer => manufacturer.Id, conf => conf.Ignore())
                .ForMember(manufacturer => manufacturer.Name, conf => conf.MapFrom(notification => notification.Name))
                .ForMember(manufacturer => manufacturer.NormalizedName, conf => conf.MapFrom(notification => notification.Name.ToUpperInvariant()))
                .ForMember(manufacturer => manufacturer.CountryId, conf => conf.MapFrom(notification => notification.CountryId))
                .ForMember(manufacturer => manufacturer.Image, conf => conf.Ignore());

            CreateMap<UpdateManufacturerNotification, Manufacturer>()
                .ForMember(manufacturer => manufacturer.Id, conf => conf.MapFrom(notification => notification.ManufacturerId))
                .ForMember(manufacturer => manufacturer.Name, conf => conf.MapFrom(notification => notification.Name))
                .ForMember(manufacturer => manufacturer.NormalizedName, conf => conf.MapFrom(notification => notification.Name.ToUpperInvariant()))
                .ForMember(manufacturer => manufacturer.CountryId, conf => conf.MapFrom(notification => notification.CountryId))
                .ForMember(manufacturer => manufacturer.Image, conf => conf.Ignore());
        }
    }
}

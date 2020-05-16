namespace AutoParts.Web.Server.MappingProfiles
{
    using AutoMapper;

    using Protos;

    using Core.Contracts.Manufacturer.Models;
    using Core.Contracts.Manufacturer.Notifications;

    public class ManufacturerMappingProfile : Profile
    {
        public ManufacturerMappingProfile()
        {
            CreateMap<ManufacturerModel, Manufacturer>()
                .ForMember(manufacturer => manufacturer.Id, conf => conf.MapFrom(model => model.Id))
                .ForMember(manufacturer => manufacturer.Name, conf => conf.MapFrom(model => model.Name))
                .ForMember(manufacturer => manufacturer.Description, conf => conf.MapFrom(model => model.Description))
                .ForMember(manufacturer => manufacturer.ImageUrl, conf => conf.MapFrom(model => model.ImageUrl ?? string.Empty))
                .ForMember(manufacturer => manufacturer.CountryId, conf => conf.MapFrom(model => model.CountryId))
                .ForMember(manufacturer => manufacturer.CountryName, conf => conf.MapFrom(model => model.CountryName));

            CreateMap<CreateManufacturerRequest, CreateManufacturerNotification>()
                .ForMember(notification => notification.Name, conf => conf.MapFrom(request => request.Name))
                .ForMember(notification => notification.Description, conf => conf.MapFrom(request => request.Description))
                .ForMember(notification => notification.ImageFileName, conf => conf.MapFrom(request => request.ImageFileName))
                .ForMember(notification => notification.ImageFileBuffer, conf => conf.MapFrom(request => request.ImageFileBuffer.ToByteArray()))
                .ForMember(notification => notification.CountryId, conf => conf.MapFrom(request => request.CountryId));

            CreateMap<UpdateManufacturerRequest, UpdateManufacturerNotification>()
                .ForMember(notification => notification.ManufacturerId, conf => conf.MapFrom(request => request.Id))
                .ForMember(notification => notification.Name, conf => conf.MapFrom(request => request.Name))
                .ForMember(notification => notification.Description, conf => conf.MapFrom(request => request.Description))
                .ForMember(notification => notification.ImageFileName, conf => conf.MapFrom(request => request.ImageFileName))
                .ForMember(notification => notification.ImageFileBuffer, conf => conf.MapFrom(request => request.ImageFileBuffer.ToByteArray()))
                .ForMember(notification => notification.CountryId, conf => conf.MapFrom(request => request.CountryId));
        }
    }
}

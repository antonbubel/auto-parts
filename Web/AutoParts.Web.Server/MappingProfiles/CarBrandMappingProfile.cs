namespace AutoParts.Web.Server.MappingProfiles
{
    using AutoMapper;

    using Protos;

    using Core.Contracts.CarBrands.Models;
    using Core.Contracts.CarBrands.Notifications;

    public class CarBrandMappingProfile : Profile
    {
        public CarBrandMappingProfile()
        {
            CreateMap<CarBrandModel, CarBrand>()
                .ForMember(carBrand => carBrand.Id, conf => conf.MapFrom(model => model.Id))
                .ForMember(carBrand => carBrand.Name, conf => conf.MapFrom(model => model.Name))
                .ForMember(carBrand => carBrand.ImageUrl, conf => conf.MapFrom(model => model.ImageUrl));

            CreateMap<CreateCarBrandRequest, CreateCarBrandNotification>()
                .ForMember(notification => notification.Name, conf => conf.MapFrom(request => request.Name))
                .ForMember(notification => notification.ImageFileName, conf => conf.MapFrom(request => request.ImageName))
                .ForMember(notification => notification.ImageFileBuffer, conf => conf.MapFrom(request => request.Image.ToByteArray()));

            CreateMap<UpdateCarBrandRequest, UpdateCarBrandNotification>()
                .ForMember(notification => notification.CarBrandId, conf => conf.MapFrom(request => request.Id))
                .ForMember(notification => notification.Name, conf => conf.MapFrom(request => request.Name))
                .ForMember(notification => notification.ImageFileName, conf => conf.MapFrom(request => request.ImageName))
                .ForMember(notification => notification.ImageFileBuffer, conf => conf.MapFrom(request => request.Image.ToByteArray()));
        }
    }
}

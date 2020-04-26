namespace AutoParts.Web.Server.MappingProfiles
{
    using AutoMapper;

    using Protos;

    using Core.Contracts.CarModels.Models;
    using Core.Contracts.CarModels.Notifications;

    public class CarModelMappingProfile : Profile
    {
        public CarModelMappingProfile()
        {
            CreateMap<CarModelModel, CarModel>()
                .ForMember(carModel => carModel.Id, conf => conf.MapFrom(model => model.Id))
                .ForMember(carModel => carModel.CarBrandId, conf => conf.MapFrom(model => model.CarBrandId))
                .ForMember(carModel => carModel.CarBrandName, conf => conf.MapFrom(model => model.CarBrandName))
                .ForMember(carModel => carModel.Name, conf => conf.MapFrom(model => model.Name))
                .ForMember(carModel => carModel.ImageUrl, conf => conf.MapFrom(model => model.ImageUrl));

            CreateMap<CreateCarModelRequest, CreateCarModelNotification>()
                .ForMember(notification => notification.CarBrandId, conf => conf.MapFrom(request => request.CarBrandId))
                .ForMember(notification => notification.Name, conf => conf.MapFrom(request => request.Name))
                .ForMember(notification => notification.ImageFileName, conf => conf.MapFrom(request => request.ImageName))
                .ForMember(notification => notification.ImageFileBuffer, conf => conf.MapFrom(request => request.Image.ToByteArray()));

            CreateMap<UpdateCarModelRequest, UpdateCarModelNotification>()
                .ForMember(notification => notification.CarModelId, conf => conf.MapFrom(request => request.Id))
                .ForMember(notification => notification.Name, conf => conf.MapFrom(request => request.Name))
                .ForMember(notification => notification.ImageFileName, conf => conf.MapFrom(request => request.ImageName))
                .ForMember(notification => notification.ImageFileBuffer, conf => conf.MapFrom(request => request.Image.ToByteArray()));
        }
    }
}

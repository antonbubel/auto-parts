namespace AutoParts.Web.Server.MappingProfiles
{
    using AutoMapper;

    using Protos;

    using Core.Contracts.CarModifications.Models;
    using Core.Contracts.CarModifications.Notifications;

    public class CarModificationMappingProfile : Profile
    {
        public CarModificationMappingProfile()
        {
            CreateMap<CarModificationModel, CarModification>()
                .ForMember(carModification => carModification.Id, conf => conf.MapFrom(model => model.Id))
                .ForMember(carModification => carModification.Name, conf => conf.MapFrom(model => model.Name))
                .ForMember(carModification => carModification.Description, conf => conf.MapFrom(model => model.Description ?? string.Empty))
                .ForMember(carModification => carModification.Year, conf => conf.MapFrom(model => model.Year));

            CreateMap<CreateCarModificationRequest, CreateCarModificationNotification>()
                .ForMember(notification => notification.CarModelId, conf => conf.MapFrom(request => request.CarModelId))
                .ForMember(notification => notification.Name, conf => conf.MapFrom(request => request.Name))
                .ForMember(notification => notification.Description, conf => conf.MapFrom(request => request.Description))
                .ForMember(notification => notification.Year, conf => conf.MapFrom(request => request.Year));

            CreateMap<UpdateCarModificationRequest, UpdateCarModificationNotification>()
                .ForMember(notification => notification.CarModificationId, conf => conf.MapFrom(request => request.Id))
                .ForMember(notification => notification.Name, conf => conf.MapFrom(request => request.Name))
                .ForMember(notification => notification.Description, conf => conf.MapFrom(request => request.Description))
                .ForMember(notification => notification.Year, conf => conf.MapFrom(request => request.Year));
        }
    }
}

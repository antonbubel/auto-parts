namespace AutoParts.Core.Implementation.CarModifications.MappingProfiles
{
    using AutoMapper;

    using Contracts.CarModifications.Models;
    using Contracts.CarModifications.Notifications;

    using Data.Model.Entities;

    public class CarModificationMappingProfile : Profile
    {
        public CarModificationMappingProfile()
        {
            CreateMap<CarModification, CarModificationModel>()
                .ForMember(carModificationModel => carModificationModel.Id, conf => conf.MapFrom(carModification => carModification.Id))
                .ForMember(carModificationModel => carModificationModel.Name, conf => conf.MapFrom(carModification => carModification.Name))
                .ForMember(carModificationModel => carModificationModel.Description, conf => conf.MapFrom(carModification => carModification.Description))
                .ForMember(carModificationModel => carModificationModel.Year, conf => conf.MapFrom(carModification => carModification.Year));

            CreateMap<CreateCarModificationNotification, CarModification>()
                .ForMember(carModification => carModification.Id, conf => conf.Ignore())
                .ForMember(carModification => carModification.CarModelId, conf => conf.MapFrom(notification => notification.CarModelId))
                .ForMember(carModification => carModification.Name, conf => conf.MapFrom(notification => notification.Name))
                .ForMember(carModification => carModification.Description, conf => conf.MapFrom(notification => notification.Description))
                .ForMember(carModification => carModification.Year, conf => conf.MapFrom(notification => notification.Year));

            CreateMap<UpdateCarModificationNotification, CarModification>()
                .ForMember(carModification => carModification.Id, conf => conf.Ignore())
                .ForMember(carModification => carModification.CarModelId, conf => conf.Ignore())
                .ForMember(carModification => carModification.Name, conf => conf.MapFrom(notification => notification.Name))
                .ForMember(carModification => carModification.Description, conf => conf.MapFrom(notification => notification.Description))
                .ForMember(carModification => carModification.Year, conf => conf.MapFrom(notification => notification.Year));
        }
    }
}

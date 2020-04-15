namespace AutoParts.Core.Implementation.CarModifications.MappingProfiles
{
    using AutoMapper;

    using Contracts.CarModifications.Models;

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
        }
    }
}

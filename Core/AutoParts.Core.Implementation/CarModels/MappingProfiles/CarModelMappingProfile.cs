namespace AutoParts.Core.Implementation.CarModels.MappingProfiles
{
    using AutoMapper;

    using Contracts.CarModels.Models;

    using Data.Model.Entities;

    public class CarModelMappingProfile : Profile
    {
        public CarModelMappingProfile()
        {
            CreateMap<CarModel, CarModelModel>()
                .ForMember(carModel => carModel.Id, conf => conf.MapFrom(carModel => carModel.Id))
                .ForMember(carModel => carModel.Name, conf => conf.MapFrom(carModel => carModel.Name))
                .ForMember(carModel => carModel.ImageUrl, conf => conf.MapFrom(carModel => carModel.Image))
                .ForMember(carModel => carModel.CarBrandId, conf => conf.MapFrom(carModel => carModel.CarBrandId))
                .ForMember(carModel => carModel.CarBrandName, conf => conf.MapFrom(carModel => carModel.CarBrand.Name));
        }
    }
}

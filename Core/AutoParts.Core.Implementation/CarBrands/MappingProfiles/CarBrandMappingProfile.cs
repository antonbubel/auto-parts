namespace AutoParts.Core.Implementation.CarBrands.MappingProfiles
{
    using AutoMapper;

    using Contracts.CarBrands.Models;
    using Contracts.CarBrands.Notifications;

    using Data.Model.Entities;

    public class CarBrandMappingProfile : Profile
    {
        public CarBrandMappingProfile()
        {
            CreateMap<CarBrand, CarBrandModel>()
                .ForMember(carBrandModel => carBrandModel.Id, conf => conf.MapFrom(carBrand => carBrand.Id))
                .ForMember(carBrandModel => carBrandModel.Name, conf => conf.MapFrom(carBrand => carBrand.Name))
                .ForMember(carBrandModel => carBrandModel.ImageUrl, conf => conf.MapFrom(carBrand => carBrand.Image));

            CreateMap<CreateCarBrandNotification, CarBrand>()
                .ForMember(carBrand => carBrand.Id, conf => conf.Ignore())
                .ForMember(carBrand => carBrand.Name, conf => conf.MapFrom(notification => notification.Name))
                .ForMember(carBrand => carBrand.Image, conf => conf.Ignore());

            CreateMap<UpdateCarBrandNotification, CarBrand>()
                .ForMember(carBrand => carBrand.Id, conf => conf.Ignore())
                .ForMember(carBrand => carBrand.Name, conf => conf.MapFrom(notification => notification.Name))
                .ForMember(carBrand => carBrand.Image, conf => conf.Ignore());
        }
    }
}

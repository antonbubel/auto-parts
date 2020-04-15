namespace AutoParts.Core.Implementation.CarBrands.MappingProfiles
{
    using AutoMapper;

    using Contracts.CarBrands.Models;

    using Data.Model.Entities;

    public class CarBrandMappingProfile : Profile
    {
        public CarBrandMappingProfile()
        {
            CreateMap<CarBrand, CarBrandModel>()
                .ForMember(carBrandModel => carBrandModel.Id, conf => conf.MapFrom(carBrand => carBrand.Id))
                .ForMember(carBrandModel => carBrandModel.Name, conf => conf.MapFrom(carBrand => carBrand.Name))
                .ForMember(carBrandModel => carBrandModel.ImageUrl, conf => conf.MapFrom(carBrand => carBrand.Image));
        }
    }
}

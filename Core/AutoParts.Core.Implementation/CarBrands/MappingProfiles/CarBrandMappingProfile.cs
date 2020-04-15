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
                .ForMember(carBrandModel => carBrandModel.ImageUrl, conf => conf.MapFrom(carBrand => carBrand.Image))
                .ReverseMap()
                .ForMember(carBrand => carBrand.Id, conf => conf.MapFrom(carBrandModel => carBrandModel.Id))
                .ForMember(carBrand => carBrand.Name, conf => conf.MapFrom(carBrandModel => carBrandModel.Name))
                .ForMember(carBrand => carBrand.Image, conf => conf.Ignore());
        }
    }
}

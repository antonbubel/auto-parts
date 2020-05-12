namespace AutoParts.Core.Implementation.Country.MappingProfiles
{
    using AutoMapper;

    using Contracts.Country.Models;

    using Data.Model.Entities;

    public class CountryMappingProfile : Profile
    {
        public CountryMappingProfile()
        {
            CreateMap<Country, CountryModel>()
                .ForMember(model => model.Id, conf => conf.MapFrom(country => country.Id))
                .ForMember(model => model.Name, conf => conf.MapFrom(country => country.Name));
        }
    }
}

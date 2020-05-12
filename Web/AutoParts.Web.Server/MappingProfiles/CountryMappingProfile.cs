namespace AutoParts.Web.Server.MappingProfiles
{
    using AutoMapper;

    using Protos;

    using Core.Contracts.Country.Models;

    public class CountryMappingProfile : Profile
    {
        public CountryMappingProfile()
        {
            CreateMap<CountryModel, Country>()
                .ForMember(country => country.Id, conf => conf.MapFrom(model => model.Id))
                .ForMember(country => country.Name, conf => conf.MapFrom(model => model.Name));
        }
    }
}

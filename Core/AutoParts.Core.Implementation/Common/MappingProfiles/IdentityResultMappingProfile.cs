namespace AutoParts.Core.Implementation.Common.MappingProfiles
{
    using AutoMapper;

    using Microsoft.AspNetCore.Identity;

    using Infrastructure.Exceptions.Models;

    public class IdentityResultMappingProfile : Profile
    {
        public IdentityResultMappingProfile()
        {
            CreateMap<IdentityError, Error>()
                .ForMember(error => error.Code, conf => conf.MapFrom(identityError => identityError.Code))
                .ForMember(error => error.Description, conf => conf.MapFrom(identityError => identityError.Description));
        }
    }
}

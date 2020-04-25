namespace AutoParts.Web.Server.MappingProfiles
{
    using AutoMapper;
    
    using IdentityModel.Client;

    using Protos;

    public class SignInMappingProfile : Profile
    {
        public SignInMappingProfile()
        {
            CreateMap<TokenResponse, UserSignInResponse>()
                .ForMember(response => response.AccessToken, conf => conf.MapFrom(tokenResponse => tokenResponse.AccessToken ?? string.Empty))
                .ForMember(response => response.IdentityToken, conf => conf.MapFrom(tokenResponse => tokenResponse.IdentityToken ?? string.Empty))
                .ForMember(response => response.TokenType, conf => conf.MapFrom(tokenResponse => tokenResponse.TokenType ?? string.Empty))
                .ForMember(response => response.RefreshToken, conf => conf.MapFrom(tokenResponse => tokenResponse.RefreshToken ?? string.Empty))
                .ForMember(response => response.IsError, conf => conf.MapFrom(tokenResponse => tokenResponse.IsError))
                .ForMember(response => response.Error, conf => conf.MapFrom(tokenResponse => tokenResponse.Error ?? string.Empty))
                .ForMember(response => response.ErrorDescription, conf => conf.MapFrom(tokenResponse => tokenResponse.ErrorDescription ?? string.Empty))
                .ForMember(response => response.ExpiresIn, conf => conf.MapFrom(tokenResponse => tokenResponse.ExpiresIn));
        }
    }
}

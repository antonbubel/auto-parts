namespace AutoParts.Web.Server.MappingProfiles
{
    using AutoMapper;

    using System;

    using IdentityModel.Client;

    using Protos;

    using Core.Contracts.Users.Models;

    using UserTypeBL = Core.Constants.Enums.UserType;

    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserTypeBL, UserType>()
                .ConvertUsing(userType => (UserType)Enum.Parse(typeof(UserType), userType.ToString()));

            CreateMap<UserInfoModel, GetCurrentUserInfoResponse>()
                .ForMember(response => response.Id, conf => conf.MapFrom(user => user.Id))
                .ForMember(response => response.FirstName, conf => conf.MapFrom(user => user.FirstName))
                .ForMember(response => response.LastName, conf => conf.MapFrom(user => user.LastName))
                .ForMember(response => response.Email, conf => conf.MapFrom(user => user.Email))
                .ForMember(response => response.UserType, conf => conf.MapFrom(user => user.UserType));

            CreateMap<TokenResponse, GetRefreshedTokenResponse>()
                .ForMember(response => response.AccessToken, conf => conf.MapFrom(tokenResponse => tokenResponse.AccessToken ?? string.Empty))
                .ForMember(response => response.TokenType, conf => conf.MapFrom(tokenResponse => tokenResponse.TokenType ?? string.Empty))
                .ForMember(response => response.RefreshToken, conf => conf.MapFrom(tokenResponse => tokenResponse.RefreshToken ?? string.Empty))
                .ForMember(response => response.IsError, conf => conf.MapFrom(tokenResponse => tokenResponse.IsError))
                .ForMember(response => response.Error, conf => conf.MapFrom(tokenResponse => tokenResponse.Error ?? string.Empty))
                .ForMember(response => response.ErrorDescription, conf => conf.MapFrom(tokenResponse => tokenResponse.ErrorDescription ?? string.Empty));

        }
    }
}

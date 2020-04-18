namespace AutoParts.Core.Implementation.Users.MappingProfiles
{
    using AutoMapper;

    using Data.Model.Entities;
    using UserTypeEnum = Data.Model.Enums.UserType;

    using Contracts.Users.Models;
    using Contracts.Users.Notifications;

    public class UserSignUpMappingProfile : Profile
    {
        public UserSignUpMappingProfile()
        {
            CreateMap<UserSignUpNotification, User>()
                .ForMember(user => user.UserName, conf => conf.MapFrom(notification => notification.Email))
                .ForMember(user => user.FirstName, conf => conf.MapFrom(notification => notification.FirstName))
                .ForMember(user => user.LastName, conf => conf.MapFrom(notification => notification.LastName))
                .ForMember(user => user.Email, conf => conf.MapFrom(notification => notification.Email))
                .ForMember(user => user.UserTypeId, conf => conf.MapFrom(notification => UserTypeEnum.User));

            CreateMap<User, UserInfoModel>()
                .ForMember(userInfo => userInfo.Id, conf => conf.MapFrom(user => user.Id))
                .ForMember(userInfo => userInfo.UserType, conf => conf.MapFrom(user => user.UserTypeId))
                .ForMember(userInfo => userInfo.FirstName, conf => conf.MapFrom(user => user.FirstName))
                .ForMember(userInfo => userInfo.LastName, conf => conf.MapFrom(user => user.LastName))
                .ForMember(userInfo => userInfo.Email, conf => conf.MapFrom(user => user.Email));
        }
    }
}

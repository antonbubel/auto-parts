namespace AutoParts.Core.Implementation.Suppliers.MappingProfiles
{
    using AutoMapper;

    using Contracts.Suppliers.Notifications;

    using Data.Model.Entities;
    using UserTypeEnum = Data.Model.Enums.UserType;

    public class SupplierMappingProfile : Profile
    {
        public SupplierMappingProfile()
        {
            CreateMap<SupplierSignUpNotification, User>()
                .ForMember(user => user.UserName, conf => conf.Ignore())
                .ForMember(user => user.FirstName, conf => conf.MapFrom(notification => notification.FirstName))
                .ForMember(user => user.LastName, conf => conf.MapFrom(notification => notification.LastName))
                .ForMember(user => user.PhoneNumber, conf => conf.MapFrom(notification => notification.PhoneNumber))
                .ForMember(user => user.Email, conf => conf.Ignore())
                .ForMember(user => user.UserTypeId, conf => conf.MapFrom(notification => UserTypeEnum.Supplier));

            CreateMap<SupplierSignUpNotification, SupplierProfile>()
                .ForMember(profile => profile.Id, conf => conf.Ignore())
                .ForMember(profile => profile.OrganizationName, conf => conf.MapFrom(notification => notification.OrganizationName))
                .ForMember(profile => profile.OrganizationAddress, conf => conf.MapFrom(notification => notification.OrganizationAddress))
                .ForMember(profile => profile.Website, conf => conf.MapFrom(notification => notification.Website));

        }
    }
}

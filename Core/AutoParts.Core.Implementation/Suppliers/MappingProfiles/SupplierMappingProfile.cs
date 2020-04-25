namespace AutoParts.Core.Implementation.Suppliers.MappingProfiles
{
    using AutoMapper;

    using Contracts.Suppliers.Models;
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

            CreateMap<SupplierProfile, SupplierPrivateProfileModel>()
                .ForMember(profileModel => profileModel.Id, conf => conf.MapFrom(profile => profile.Id))
                .ForMember(profileModel => profileModel.FirstName, conf => conf.MapFrom(profile => profile.User.FirstName))
                .ForMember(profileModel => profileModel.LastName, conf => conf.MapFrom(profile => profile.User.LastName))
                .ForMember(profileModel => profileModel.PhoneNumber, conf => conf.MapFrom(profile => profile.User.PhoneNumber))
                .ForMember(profileModel => profileModel.OrganizationName, conf => conf.MapFrom(profile => profile.OrganizationName))
                .ForMember(profileModel => profileModel.OrganizationAddress, conf => conf.MapFrom(profile => profile.OrganizationAddress))
                .ForMember(profileModel => profileModel.OrganizationDescription, conf => conf.MapFrom(profile => profile.OrganizationDescription))
                .ForMember(profileModel => profileModel.SalesEmail, conf => conf.MapFrom(profile => profile.SalesEmail))
                .ForMember(profileModel => profileModel.SalesPhoneNumber, conf => conf.MapFrom(profile => profile.SalesPhoneNumber))
                .ForMember(profileModel => profileModel.Website, conf => conf.MapFrom(profile => profile.Website))
                .ForMember(profileModel => profileModel.LogoUrl, conf => conf.MapFrom(profile => profile.Logo));

            CreateMap<UpdateSupplierProfileNotification, SupplierProfile>()
                .ForMember(profile => profile.Id, conf => conf.Ignore())
                .ForMember(profile => profile.User, conf => conf.Ignore())
                .ForMember(profile => profile.Logo, conf => conf.Ignore())
                .ForMember(profile => profile.OrganizationName, conf => conf.MapFrom(notification => notification.OrganizationName))
                .ForMember(profile => profile.OrganizationAddress, conf => conf.MapFrom(notification => notification.OrganizationAddress))
                .ForMember(profile => profile.OrganizationDescription, conf => conf.MapFrom(notification => notification.OrganizationDescription))
                .ForMember(profile => profile.SalesEmail, conf => conf.MapFrom(notification => notification.SalesEmail))
                .ForMember(profile => profile.SalesPhoneNumber, conf => conf.MapFrom(notification => notification.SalesPhoneNumber))
                .ForMember(profile => profile.Website, conf => conf.MapFrom(notification => notification.Website));
        }
    }
}

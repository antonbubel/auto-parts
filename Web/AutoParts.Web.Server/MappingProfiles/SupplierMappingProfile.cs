namespace AutoParts.Web.Server.MappingProfiles
{
    using AutoMapper;

    using Protos;

    using Core.Contracts.Suppliers.Models;
    using Core.Contracts.Suppliers.Notifications;

    public class SupplierMappingProfile : Profile
    {
        public SupplierMappingProfile()
        {
            CreateMap<UpdateSupplierProfileRequest, UpdateSupplierProfileNotification>()
                .ForMember(notification => notification.UserId, conf => conf.Ignore())
                .ForMember(notification => notification.OrganizationName, conf => conf.MapFrom(model => model.Name))
                .ForMember(notification => notification.OrganizationAddress, conf => conf.MapFrom(model => model.OrganizationAddress))
                .ForMember(notification => notification.OrganizationDescription, conf => conf.MapFrom(model => model.OrganizationDescription))
                .ForMember(notification => notification.SalesEmail, conf => conf.MapFrom(model => model.SalesEmail))
                .ForMember(notification => notification.SalesPhoneNumber, conf => conf.MapFrom(model => model.SalesPhoneNumber))
                .ForMember(notification => notification.Website, conf => conf.MapFrom(model => model.Website))
                .ForMember(notification => notification.LogoFileName, conf => conf.MapFrom(model => model.LogoFileName))
                .ForMember(notification => notification.LogoFileBuffer, conf => conf.MapFrom(model => model.LogoFileBuffer.ToByteArray()));

            CreateMap<SupplierPrivateProfileModel, SupplierPrivateProfile>()
                .ForMember(profile => profile.Id, conf => conf.MapFrom(model => model.Id))
                .ForMember(profile => profile.FirstName, conf => conf.MapFrom(model => model.FirstName))
                .ForMember(profile => profile.LastName, conf => conf.MapFrom(model => model.LastName))
                .ForMember(profile => profile.PhoneNumber, conf => conf.MapFrom(model => model.PhoneNumber))
                .ForMember(profile => profile.Name, conf => conf.MapFrom(model => model.OrganizationName))
                .ForMember(profile => profile.OrganizationAddress, conf => conf.MapFrom(model => model.OrganizationAddress))
                .ForMember(profile => profile.OrganizationDescription, conf => conf.MapFrom(model => model.OrganizationDescription))
                .ForMember(profile => profile.SalesEmail, conf => conf.MapFrom(model => model.SalesEmail))
                .ForMember(profile => profile.SalesPhoneNumber, conf => conf.MapFrom(model => model.SalesPhoneNumber))
                .ForMember(profile => profile.Website, conf => conf.MapFrom(model => model.Website))
                .ForMember(profile => profile.LogoUrl, conf => conf.MapFrom(model => model.LogoUrl));

            CreateMap<SupplierPublicProfileModel, SupplierPublicProfile>()
                .ForMember(profile => profile.Id, conf => conf.MapFrom(model => model.Id))
                .ForMember(profile => profile.Name, conf => conf.MapFrom(model => model.Name))
                .ForMember(profile => profile.OrganizationAddress, conf => conf.MapFrom(model => model.OrganizationAddress))
                .ForMember(profile => profile.OrganizationDescription, conf => conf.MapFrom(model => model.OrganizationDescription))
                .ForMember(profile => profile.SalesEmail, conf => conf.MapFrom(model => model.SalesEmail))
                .ForMember(profile => profile.SalesPhoneNumber, conf => conf.MapFrom(model => model.SalesPhoneNumber))
                .ForMember(profile => profile.Website, conf => conf.MapFrom(model => model.Website))
                .ForMember(profile => profile.LogoUrl, conf => conf.MapFrom(model => model.LogoUrl));

            CreateMap<SupplierShortPublicProfileModel, SupplierShortPublicProfile>()
                .ForMember(profile => profile.Id, conf => conf.MapFrom(model => model.Id))
                .ForMember(profile => profile.Name, conf => conf.MapFrom(model => model.Name))
                .ForMember(profile => profile.LogoUrl, conf => conf.MapFrom(model => model.LogoUrl));
        }
    }
}

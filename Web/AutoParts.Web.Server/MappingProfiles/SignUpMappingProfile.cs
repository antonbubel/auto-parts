namespace AutoParts.Web.Server.MappingProfiles
{
    using AutoMapper;

    using Protos;

    using Core.Contracts.Users.Notifications;
    using Core.Contracts.Suppliers.Notifications;

    public class SignUpMappingProfile : Profile
    {
        public SignUpMappingProfile()
        {
            CreateMap<UserSignUpRequest, UserSignUpNotification>()
                .ForMember(notification => notification.Email, conf => conf.MapFrom(request => request.Email))
                .ForMember(notification => notification.FirstName, conf => conf.MapFrom(request => request.FirstName))
                .ForMember(notification => notification.LastName, conf => conf.MapFrom(request => request.LastName))
                .ForMember(notification => notification.Password, conf => conf.MapFrom(request => request.Password))
                .ForMember(notification => notification.PasswordConfirmation, conf => conf.MapFrom(request => request.PasswordConfirmation));

            CreateMap<SupplierSignUpRequest, SupplierSignUpNotification>()
                .ForMember(notification => notification.FirstName, conf => conf.MapFrom(request => request.FirstName))
                .ForMember(notification => notification.LastName, conf => conf.MapFrom(request => request.LastName))
                .ForMember(notification => notification.Password, conf => conf.MapFrom(request => request.Password))
                .ForMember(notification => notification.PasswordConfirmation, conf => conf.MapFrom(request => request.PasswordConfirmation))
                .ForMember(notification => notification.PhoneNumber, conf => conf.MapFrom(request => request.PhoneNumber))
                .ForMember(notification => notification.OrganizationName, conf => conf.MapFrom(request => request.OrganizationName))
                .ForMember(notification => notification.OrganizationAddress, conf => conf.MapFrom(request => request.OrganizationAddress))
                .ForMember(notification => notification.Website, conf => conf.MapFrom(request => request.Website))
                .ForMember(notification => notification.InvitationToken, conf => conf.MapFrom(request => request.InvitationToken));
        }
    }
}

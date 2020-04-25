namespace AutoParts.Core.Implementation.Suppliers.MappingProfiles
{
    using AutoMapper;

    using Contracts.Emails.Notifications;

    using Contracts.Suppliers.Notifications;

    using Data.Model.Entities;

    public class SupplierInitationMappingProfile : Profile
    {
        public SupplierInitationMappingProfile()
        {
            CreateMap<InviteSupplierNotification, SupplierInvitation>()
                .ForMember(supplierInvitation => supplierInvitation.Email, conf => conf.MapFrom(notification => notification.Email))
                .ForMember(supplierInvitation => supplierInvitation.NormalizedEmail, conf => conf.MapFrom(notification => notification.Email.ToUpperInvariant()))
                .ForMember(supplierInvitation => supplierInvitation.Name, conf => conf.MapFrom(notification => notification.Name))
                .ForMember(supplierInvitation => supplierInvitation.Token, conf => conf.Ignore());

            CreateMap<SupplierInvitation, SendSupplierInvitationEmailNotification>()
                .ForMember(notification => notification.ToEmail, conf => conf.MapFrom(supplierInvitation => supplierInvitation.Email))
                .ForMember(notification => notification.ToName, conf => conf.MapFrom(supplierInvitation => supplierInvitation.Name))
                .ForMember(notification => notification.InvitationToken, conf => conf.MapFrom(supplierInvitation => supplierInvitation.Token));
        }
    }
}

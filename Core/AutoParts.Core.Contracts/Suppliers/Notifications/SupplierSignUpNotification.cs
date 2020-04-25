namespace AutoParts.Core.Contracts.Suppliers.Notifications
{
    using MediatR;

    public class SupplierSignUpNotification : INotification
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
        
        public string Password { get; set; }

        public string PasswordConfirmation { get; set; }

        public string OrganizationName { get; set; }

        public string OrganizationAddress { get; set; }

        public string Website { get; set; }

        public string InvitationToken { get; set; }
    }
}

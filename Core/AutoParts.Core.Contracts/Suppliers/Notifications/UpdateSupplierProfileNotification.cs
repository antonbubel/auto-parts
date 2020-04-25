namespace AutoParts.Core.Contracts.Suppliers.Notifications
{
    using MediatR;

    using System;

    public class UpdateSupplierProfileNotification : INotification
    {
        public long UserId { get; set; }

        public string OrganizationName { get; set; }

        public string OrganizationAddress { get; set; }

        public string OrganizationDescription { get; set; }

        public string SalesEmail { get; set; }

        public string SalesPhoneNumber { get; set; }

        public string Website { get; set; }

        public string LogoFileName { get; set; }

        public ReadOnlyMemory<byte> LogoFileBuffer { get; set; }
    }
}

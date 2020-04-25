namespace AutoParts.Core.Contracts.Suppliers.Notifications
{
    using MediatR;

    public class InviteSupplierNotification : INotification
    {
        public string Email { get; set; }

        public string Name { get; set; }
    }
}

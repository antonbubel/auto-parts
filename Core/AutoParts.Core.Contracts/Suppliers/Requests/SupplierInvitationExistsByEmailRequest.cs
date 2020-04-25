namespace AutoParts.Core.Contracts.Suppliers.Requests
{
    using MediatR;

    public class SupplierInvitationExistsByEmailRequest : IRequest<bool>
    {
        public string Email { get; set; }
    }
}

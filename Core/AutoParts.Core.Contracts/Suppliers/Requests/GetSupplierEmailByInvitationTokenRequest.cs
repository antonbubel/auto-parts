namespace AutoParts.Core.Contracts.Suppliers.Requests
{
    using MediatR;

    public class GetSupplierEmailByInvitationTokenRequest : IRequest<string>
    {
        public string Token { get; set; }
    }
}

namespace AutoParts.Core.Contracts.Suppliers.Requests
{
    using MediatR;

    using Models;

    public class GetPrivateSupplierProfileRequest : IRequest<SupplierPrivateProfileModel>
    {
        public long UserId { get; set; }
    }
}

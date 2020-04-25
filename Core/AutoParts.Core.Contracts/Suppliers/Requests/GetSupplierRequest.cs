namespace AutoParts.Core.Contracts.Suppliers.Requests
{
    using MediatR;

    using Models;

    public class GetSupplierRequest : IRequest<SupplierPublicProfileModel>
    {
        public long SupplierId { get; set; }
    }
}

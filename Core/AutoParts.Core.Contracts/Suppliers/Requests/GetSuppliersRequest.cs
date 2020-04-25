namespace AutoParts.Core.Contracts.Suppliers.Requests
{
    using MediatR;

    using Models;
    using Common.Models;

    public class GetSuppliersRequest : PaginationFilterModel, IRequest<SupplierShortPublicProfileModel[]>
    {
    }
}

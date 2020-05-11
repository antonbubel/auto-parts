namespace AutoParts.Core.Contracts.Catalogs.Requests
{
    using MediatR;

    using Models;

    public class GetSubCatalogsRequest : IRequest<CatalogModel[]>
    {
        public long CatalogId { get; set; }
    }
}

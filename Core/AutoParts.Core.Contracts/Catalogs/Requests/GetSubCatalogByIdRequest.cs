namespace AutoParts.Core.Contracts.Catalogs.Requests
{
    using MediatR;

    using Models;

    public class GetSubCatalogByIdRequest : IRequest<SubCatalogModel>
    {
        public long SubCatalogId { get; set; }
    }
}

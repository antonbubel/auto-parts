namespace AutoParts.Core.Contracts.Catalogs.Requests
{
    using MediatR;

    using Models;

    public class GetCatalogsRequest : IRequest<CatalogModel[]>
    {
    }
}

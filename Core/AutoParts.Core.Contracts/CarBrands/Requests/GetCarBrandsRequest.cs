namespace AutoParts.Core.Contracts.CarBrands.Requests
{
    using MediatR;

    using Models;

    public class GetCarBrandsRequest : IRequest<CarBrandModel[]>
    {
    }
}

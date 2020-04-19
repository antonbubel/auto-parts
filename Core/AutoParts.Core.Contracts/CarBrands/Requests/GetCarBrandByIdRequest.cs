namespace AutoParts.Core.Contracts.CarBrands.Requests
{
    using MediatR;

    using Models;

    public class GetCarBrandByIdRequest : IRequest<CarBrandModel>
    {
        public long CarBrandId { get; set; }
    }
}

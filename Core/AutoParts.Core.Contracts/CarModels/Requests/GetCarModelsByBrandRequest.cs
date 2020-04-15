namespace AutoParts.Core.Contracts.CarModels.Requests
{
    using MediatR;

    using Models;

    public class GetCarModelsByBrandRequest : IRequest<CarModelModel[]>
    {
        public long CarBrandId { get; set; }
    }
}

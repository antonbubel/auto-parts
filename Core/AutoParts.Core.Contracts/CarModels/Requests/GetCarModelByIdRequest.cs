namespace AutoParts.Core.Contracts.CarModels.Requests
{
    using MediatR;

    using Models;

    public class GetCarModelByIdRequest : IRequest<CarModelModel>
    {
        public long CarModelId { get; set; }
    }
}

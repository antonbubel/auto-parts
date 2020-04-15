namespace AutoParts.Core.Contracts.CarModifications.Requests
{
    using MediatR;

    using Models;

    public class GetCarModificationsByModelRequest : IRequest<CarModificationModel[]>
    {
        public long CarModelId { get; set; }

        public int? Year { get; set; }
    }
}

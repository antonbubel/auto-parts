namespace AutoParts.Core.Contracts.CarModifications.Requests
{
    using MediatR;

    using Models;

    public class GetCarModificationByIdRequest : IRequest<CarModificationModel>
    {
        public long CarModificationId { get; set; }
    }
}

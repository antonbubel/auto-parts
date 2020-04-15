namespace AutoParts.Web.Server.Services
{
    using Grpc.Core;

    using MediatR;

    using System.Linq;

    using System.Threading.Tasks;

    using Protos;

    using Core.Contracts.CarModifications.Requests;

    public class CarModificationService : GrpcCarModificationService.GrpcCarModificationServiceBase
    {
        private readonly IMediator mediator;

        public CarModificationService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override async Task<GetCarModificationsResponse> GetCarModifications(GetCarModificationsRequest request, ServerCallContext context)
        {
            var mediatorRequest = new GetCarModificationsByModelRequest
            {
                CarModelId = request.CarModelId,
                Year = request.Year == default ? null : (int?)request.Year
            };

            var carModifications = await mediator.Send(mediatorRequest);

            var response = new GetCarModificationsResponse();

            var responseCarModifications = carModifications
                .Select(carModification =>
                    new CarModification
                    {
                        Id = carModification.Id,
                        Name = carModification.Name,
                        Description = carModification.Description,
                        Year = carModification.Year
                    })
                .ToArray();

            response.CarModifications.AddRange(responseCarModifications);

            return response;
        }
    }
}

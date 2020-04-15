namespace AutoParts.Web.Server.Services
{
    using Grpc.Core;

    using MediatR;

    using System.Linq;

    using System.Threading.Tasks;

    using Protos;

    using Core.Contracts.CarModels.Requests;

    public class CarModelService : GrpcCarModelService.GrpcCarModelServiceBase
    {
        private readonly IMediator mediator;

        public CarModelService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override async Task<GetCarModelsResponse> GetCarModels(GetCarModelsRequest request, ServerCallContext context)
        {
            var carModels = await mediator.Send(new GetCarModelsByBrandRequest { CarBrandId = request.CarBrandId });

            var response = new GetCarModelsResponse();

            var responseCarModels = carModels
                .Select(carModel =>
                    new CarModel
                    {
                        Id = carModel.Id,
                        Name = carModel.Name,
                        ImageUrl = carModel.ImageUrl,
                        CarBrandId = carModel.CarBrandId,
                        CarBrandName = carModel.CarBrandName
                    })
                .ToArray();

            response.CarModels.AddRange(responseCarModels);

            return response;
        }
    }
}

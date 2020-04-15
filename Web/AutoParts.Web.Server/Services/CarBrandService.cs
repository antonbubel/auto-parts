namespace AutoParts.Web.Server.Services
{
    using Grpc.Core;

    using MediatR;

    using System.Linq;

    using System.Threading.Tasks;

    using Protos;

    using GetCarBrandsRequest = Core.Contracts.CarBrands.Requests.GetCarBrandsRequest;

    public class CarBrandService : GrpcCarBrandService.GrpcCarBrandServiceBase
    {
        private readonly IMediator mediator;

        public CarBrandService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override async Task<GetCarBrandsResponse> GetCarBrands(Protos.GetCarBrandsRequest request, ServerCallContext context)
        {
            var carBrands = await mediator.Send(new GetCarBrandsRequest());

            var response = new GetCarBrandsResponse();

            var responseCarBrands = carBrands
                .Select(carBrandModel =>
                    new CarBrand
                    {
                        Id = carBrandModel.Id,
                        Name = carBrandModel.Name,
                        ImageUrl = carBrandModel.ImageUrl
                    })
                .ToArray();

            response.CarBrands.AddRange(responseCarBrands);

            return response;
        }
    }
}

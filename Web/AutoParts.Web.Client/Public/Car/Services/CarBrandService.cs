namespace AutoParts.Web.Client.Public.Car.Services
{
    using Grpc.Net.Client;

    using System.Linq;
    using System.Threading.Tasks;

    using Protos;

    public class CarBrandService
    {
        private readonly GrpcCarBrandService.GrpcCarBrandServiceClient carBrandServiceClient;

        public CarBrandService(GrpcChannel channel)
        {
            carBrandServiceClient = new GrpcCarBrandService.GrpcCarBrandServiceClient(channel);
        }

        public async Task<CarBrand[]> GetCarBrands()
        {
            var response = await carBrandServiceClient.GetCarBrandsAsync(new GetCarBrandsRequest());

            return response.CarBrands.ToArray();
        }
    }
}

namespace AutoParts.Web.Client.Public.Car.Services
{
    using Grpc.Net.Client;

    using System.Linq;
    using System.Threading.Tasks;

    using Protos;

    public class CarModelService
    {
        private readonly GrpcCarModelService.GrpcCarModelServiceClient carModelServiceClient;

        public CarModelService(GrpcChannel channel)
        {
            carModelServiceClient = new GrpcCarModelService.GrpcCarModelServiceClient(channel);
        }

        public async Task<CarModel[]> GetCarModels(long carBrandId)
        {
            var response = await carModelServiceClient.GetCarModelsAsync(new GetCarModelsRequest() { CarBrandId = carBrandId });

            return response.CarModels.ToArray();
        }
    }
}

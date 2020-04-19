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

        public async Task<CarModel> GetCarModel(long carModelId)
        {
            var response = await carModelServiceClient.GetCarModelAsync(new GetCarModelRequest() { Id = carModelId });

            return response.Model;
        }

        public async Task<CarModel[]> GetCarModels(long carBrandId)
        {
            var response = await carModelServiceClient.GetCarModelsAsync(new GetCarModelsRequest() { CarBrandId = carBrandId });

            return response.CarModels.ToArray();
        }
    }
}

namespace AutoParts.Web.Client.Public.Car.Services
{
    using Grpc.Net.Client;

    using System.Linq;
    using System.Threading.Tasks;

    using Protos;

    public class CarModificationService
    {
        private readonly GrpcCarModificationService.GrpcCarModificationServiceClient carModificationServiceClient;

        public CarModificationService(GrpcChannel channel)
        {
            carModificationServiceClient = new GrpcCarModificationService.GrpcCarModificationServiceClient(channel);
        }

        public async Task<CarModification[]> GetCarModifications(long carModelId)
        {
            var response = await carModificationServiceClient.GetCarModificationsAsync(new GetCarModificationsRequest() { CarModelId = carModelId });

            return response.CarModifications.ToArray();
        }

        public async Task<CarModification> GetCarModification(long carModificationId)
        {
            var response = await carModificationServiceClient.GetCarModificationAsync(new GetCarModificationRequest() { CarModificationId = carModificationId });

            return response.CarModification;
        }
    }
}

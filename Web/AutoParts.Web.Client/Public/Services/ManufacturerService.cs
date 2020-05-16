namespace AutoParts.Web.Client.Public.Services
{
    using Grpc.Net.Client;

    using System.Linq;
    using System.Threading.Tasks;

    using Protos;

    public class ManufacturerService
    {
        private readonly GrpcManufacturerService.GrpcManufacturerServiceClient manufacturerServiceClient;

        public ManufacturerService(GrpcChannel channel)
        {
            manufacturerServiceClient = new GrpcManufacturerService.GrpcManufacturerServiceClient(channel);
        }

        public async Task<Manufacturer[]> GetManufactuers(long countryId)
        {
            var request = new GetManufacturersRequest
            {
                CountryId = countryId
            };

            var response = await manufacturerServiceClient.GetManufacturersAsync(request);

            return response.Manufacturers.ToArray();
        }
    }
}

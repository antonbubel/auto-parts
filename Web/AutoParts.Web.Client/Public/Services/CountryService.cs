namespace AutoParts.Web.Client.Public.Services
{
    using Grpc.Net.Client;

    using System.Linq;
    using System.Threading.Tasks;

    using Protos;

    public class CountryService
    {
        private readonly GrpcCountryService.GrpcCountryServiceClient countryServiceClient;

        public CountryService(GrpcChannel channel)
        {
            countryServiceClient = new GrpcCountryService.GrpcCountryServiceClient(channel);
        }

        public async Task<Country[]> GetCountries()
        {
            var request = new GetCountriesRequest();
            var response = await countryServiceClient.GetCountriesAsync(request);

            return response.Countries.ToArray();
        }
    }
}

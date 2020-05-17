namespace AutoParts.Web.Client.Public.AutoPart.Services
{
    using Grpc.Net.Client;

    using System.Threading.Tasks;

    using Protos;
    using Models;

    public class AutoPartService
    {
        private readonly GrpcAutoPartService.GrpcAutoPartServiceClient autoPartServiceClient;

        public AutoPartService(GrpcChannel channel)
        {
            autoPartServiceClient = new GrpcAutoPartService.GrpcAutoPartServiceClient(channel);
        }

        public async Task<GetAutoPartsResponse> GetAutoParts(int pageSize, AutoPartsFilter filter)
        {
            var request = new GetAutoPartsRequest
            {
                SearchText = filter.SearchText ?? string.Empty,
                AvailableOnly = filter.AvailableOnly,
                CarModificationId = filter.CarModificationId,
                CountryId = filter.CountryId,
                ManufacturerId = filter.ManufacturerId,
                PageNumber = filter.PageNumber,
                PageSize = pageSize,
                SortBy = filter.Sorting,
                SubCatalogId = filter.SubCatalogId,
                SupplierId = filter.SupplierId
            };

            return await autoPartServiceClient.GetAutoPartsAsync(request);
        }
    }
}

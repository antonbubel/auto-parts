using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoParts.Web.Client.Public.Services
{
    using Grpc.Net.Client;

    using System.Linq;
    using System.Threading.Tasks;

    using Protos;

    public class CatalogService
    {
        private readonly GrpcCatalogService.GrpcCatalogServiceClient catalogServiceClient;

        public CatalogService(GrpcChannel channel)
        {
            catalogServiceClient = new GrpcCatalogService.GrpcCatalogServiceClient(channel);
        }

        public async Task<Catalog[]> GetCatalogs()
        {
            var request = new GetAutoPartsCatalogsRequest();
            var response = await catalogServiceClient.GetCatalogsAsync(request);

            return response.Catalogs.ToArray();
        }

        public async Task<Catalog[]> GetSubCatalogs(long catalogId)
        {
            var request = new GetAutoPartsSubCatalogsRequest
            {
                CatalogId = catalogId
            };

            var response = await catalogServiceClient.GetSubCatalogsAsync(request);

            return response.Catalogs.ToArray();
        }

        public async Task<SubCatalog> GetSubCatalog(long subCatalogId)
        {
            var request = new GetAutoPartsSubCatalogRequest
            {
                SubCatalogId = subCatalogId
            };

            var response = await catalogServiceClient.GetSubCatalogAsync(request);

            return response.Model;
        }
    }
}

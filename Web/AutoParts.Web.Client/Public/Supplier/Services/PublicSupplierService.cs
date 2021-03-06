﻿namespace AutoParts.Web.Client.Public.Supplier.Services
{
    using Grpc.Net.Client;

    using System.Threading.Tasks;

    using Protos;

    public class PublicSupplierService
    {
        private readonly GrpcSupplierService.GrpcSupplierServiceClient supplierServiceClient;

        public PublicSupplierService(GrpcChannel channel)
        {
            supplierServiceClient = new GrpcSupplierService.GrpcSupplierServiceClient(channel);
        }

        public async Task<GetSupplierPublicProfileByIdResponse> GetSupplierProfile(long supplierId)
        {
            var request = new GetSupplierPublicProfileByIdRequest
            {
                Id = supplierId
            };

            return await supplierServiceClient.GetSupplierPublicProfileByIdAsync(request);
        }

        public async Task<GetSuppliersResponse> GetSuppliers(int pageNumber, int pageSize)
        {
            var request = new PaginationFilter
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return await supplierServiceClient.GetSuppliersAsync(request);
        }
    }
}

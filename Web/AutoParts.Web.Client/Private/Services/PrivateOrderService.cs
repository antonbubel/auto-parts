using AutoParts.Web.Client.Shared.Utils;
using AutoParts.Web.Protos;
using Blazored.LocalStorage;
using Grpc.Net.Client;
using System.Linq;
using System.Threading.Tasks;

namespace AutoParts.Web.Client.Private.Services
{
    public class PrivateOrderService
    {
        private readonly ISyncLocalStorageService localStorage;
        private readonly GrpcOrderService.GrpcOrderServiceClient orderServiceClient;

        public PrivateOrderService(ISyncLocalStorageService localStorage, GrpcChannel channel)
        {
            this.localStorage = localStorage;
            orderServiceClient = new GrpcOrderService.GrpcOrderServiceClient(channel);
        }

        public async Task<GetUserOrdersResponse> GetUserOrders(int pageNumber, int pageSize)
        {
            var filter = new PaginationFilter
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var headers = RequestHeadersUtility.GetRequestHeaders(localStorage);

            return await orderServiceClient.GetUserOrdersAsync(filter, headers);
        }

        public async Task<GetSupplierOrdersResponse> GetSupplierOrders(int pageNumber, int pageSize)
        {
            var filter = new PaginationFilter
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var headers = RequestHeadersUtility.GetRequestHeaders(localStorage);

            return await orderServiceClient.GetSupplierOrdersAsync(filter, headers);
        }

        public async Task<GetOrderResponse> GetOrder(long orderId)
        {
            var request = new GetOrderRequest
            {
                OrderId = orderId
            };

            var headers = RequestHeadersUtility.GetRequestHeaders(localStorage);

            return await orderServiceClient.GetOrderAsync(request, headers);
        }

        public async Task<OrderAutoPart[]> GetOrderItems(long orderId)
        {
            var request = new GetOrderItemsRequest
            {
                OrderId = orderId
            };

            var headers = RequestHeadersUtility.GetRequestHeaders(localStorage);
            var response = await orderServiceClient.GetOrderItemsAsync(request, headers);

            return response.Items.ToArray();
        }
    }
}

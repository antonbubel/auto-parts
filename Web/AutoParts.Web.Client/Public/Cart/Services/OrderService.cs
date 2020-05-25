using AutoParts.Web.Client.Public.Cart.Models;
using AutoParts.Web.Client.Shared.Utils;
using AutoParts.Web.Protos;
using Blazored.LocalStorage;
using Grpc.Net.Client;
using System.Linq;
using System.Threading.Tasks;

namespace AutoParts.Web.Client.Public.Cart.Services
{
    public class OrderService
    {
        private readonly ISyncLocalStorageService localStorage;
        private readonly CartService cartService;
        private readonly GrpcOrderService.GrpcOrderServiceClient orderServiceClient;

        public OrderService(ISyncLocalStorageService localStorage, CartService cartService, GrpcChannel channel)
        {
            this.localStorage = localStorage;
            this.cartService = cartService;
            orderServiceClient = new GrpcOrderService.GrpcOrderServiceClient(channel);
        }

        public async Task CreateOrder(CreateOrderFormModel formModel)
        {
            var request = new CreateOrderRequest
            {
                FirstName = formModel.FirstName,
                LastName = formModel.LastName,
                Email = formModel.Email,
                City = formModel.City,
                Comment = string.Empty,
                CountryId = formModel.CountryId,
                Region = formModel.Region,
                SaveShippingInfo = false,
                StreetAddress = formModel.StreetAddress,
                StreetAddressSecondLine = formModel.StreetAddressSecondLine ?? string.Empty,
                ZipCode = formModel.ZipCode
            };

            var orderItems = cartService.GetAutoParts()
                .Select(cartItem => new OrderItem { AutoPartId = cartItem.AutoPart.Id, Quantity = cartItem.Quantity })
                .ToArray();

            request.OrderItems.AddRange(orderItems);

            var headers = RequestHeadersUtility.GetRequestHeaders(localStorage);

            await orderServiceClient.CreateOrderAsync(request, headers);
        }
    }
}

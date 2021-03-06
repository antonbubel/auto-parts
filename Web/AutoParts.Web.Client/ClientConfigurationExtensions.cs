﻿using AutoParts.Web.Client.Private.Administrator.Services;
using AutoParts.Web.Client.Private.Services;
using AutoParts.Web.Client.Private.Supplier.Services;
using AutoParts.Web.Client.Public.AutoPart.Services;
using AutoParts.Web.Client.Public.Car.Services;
using AutoParts.Web.Client.Public.Cart.Services;
using AutoParts.Web.Client.Public.Services;
using AutoParts.Web.Client.Public.Supplier.Services;
using AutoParts.Web.Client.Public.User.Services;
using AutoParts.Web.Client.Shared.Services;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace AutoParts.Web.Client
{
    public static class ClientConfigurationExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            ConfigureGrpcChannel(services);

            ConfigureApplicationServices(services);
        }

        private static void ConfigureGrpcChannel(IServiceCollection services)
        {
            services.AddTransient(serviceProvider =>
             {
                 var config = serviceProvider.GetRequiredService<IConfiguration>();
                 var backendUrl = config["BackendUrl"];

                 // Create a gRPC-Web channel pointing to the backend server.
                 //
                 // GrpcWebText is used because server streaming requires it. If server streaming is not used in your app
                 // then GrpcWeb is recommended because it produces smaller messages.
                 var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler()));

                 var channel = GrpcChannel.ForAddress(backendUrl, new GrpcChannelOptions { HttpClient = httpClient });

                 return channel;
             });
        }

        private static void ConfigureApplicationServices(IServiceCollection services)
        {
            services.AddTransient<UserSignInService>();
            services.AddTransient<UserSignUpService>();
            services.AddTransient<CurrentUserService>();
            services.AddTransient<CarBrandService>();
            services.AddTransient<CarBrandsManagerService>();
            services.AddTransient<CarModelService>();
            services.AddTransient<CarModelsManagerService>();
            services.AddTransient<CarModificationService>();
            services.AddTransient<CarModificationsManagerService>();
            services.AddTransient<SuppliersManagerService>();
            services.AddTransient<PrivateSupplierService>();
            services.AddTransient<PublicSupplierService>();
            services.AddTransient<CountryService>();
            services.AddTransient<ManufacturerService>();
            services.AddTransient<ManufacturerManagerService>();
            services.AddTransient<CatalogService>();
            services.AddTransient<AutoPartManagerService>();
            services.AddTransient<AutoPartService>();
            services.AddTransient<PrivateOrderService>();

            services.AddScoped<CartService>();
            services.AddScoped<OrderService>();

            services.AddSingleton<CurrentUserProvider>();
        }
    }
}

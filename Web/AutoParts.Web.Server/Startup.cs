﻿namespace AutoParts.Web.Server
{
    using AutoMapper;

    using System;
    using System.IO;
    using System.Linq;
    using System.Security.Claims;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.ResponseCompression;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.Extensions.FileProviders;

    using AutoParts.Core.Implementation;
    using AutoParts.Data.EF.Migrations;
    using AutoParts.Infrastructure.Web;
    using AutoParts.Infrastructure.Web.Authorization;
    using AutoParts.Infrastructure.Web.Options;
    using AutoParts.Web.Server.Services;
    using AutoParts.Core.Constants;
    using AutoParts.Core.Constants.Enums;
    using AutoParts.Data.EF;

    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions<IdentityOptions>()
                .Bind(Configuration.GetSection(Constants.ConfigSections.IdentitySection))
                .ValidateDataAnnotations();
            
            services.AddOptions<JwtOptions>()
                .Bind(Configuration.GetSection(Constants.ConfigSections.JwtSection))
                .ValidateDataAnnotations();

            services.ConfigureDataAccess(Configuration);

            services.ConfigureBusinessLayer(Configuration);

            services.SeedData();

            services.AddAutoMapper(
                typeof(BusinessLayerConfigurationExtensions).Assembly,
                typeof(DatabaseContext).Assembly,
                typeof(Startup).Assembly);

            services.AddLogging();

            services.AddCors();

            services.AddGrpc();

            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            var jwtOptions = Configuration.GetSection(Constants.ConfigSections.JwtSection).Get<JwtOptions>();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = jwtOptions.Authority;
                    options.ClaimsIssuer = jwtOptions.Issuer;
                    options.Audience = jwtOptions.Audience;
                    options.RequireHttpsMetadata = jwtOptions.RequireHttpsMetadata;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = jwtOptions.Audience,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        NameClaimType = ClaimTypes.NameIdentifier
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(nameof(UserType.User), builder => { builder.RequireRole(nameof(UserType.User)); });
                options.AddPolicy(nameof(UserType.Supplier), builder => { builder.RequireRole(nameof(UserType.Supplier)); });
                options.AddPolicy(nameof(UserType.Administrator), builder => { builder.RequireRole(nameof(UserType.Administrator)); });
            });

            services.AddGrpc();

            services.AddHttpClient<IIdentityClient, IdentityClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }

            app.UseHttpsRedirection();

            app.UseDefaultFiles();

            var assemblyDirectory = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            var filesDirectory = Path.Combine(assemblyDirectory, FileConstants.LocalFilesFolderName);

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(filesDirectory),
                RequestPath = $"/{FileConstants.LocalFilesFolderName}"
            });

            app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseBlazorFrameworkFiles();

            app.UseRouting();

            app.UseGrpcWeb();
            
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<SignUpService>()
                    .EnableGrpcWeb();

                endpoints.MapGrpcService<SignInService>()
                    .EnableGrpcWeb();
                
                endpoints.MapGrpcService<CarBrandService>()
                    .EnableGrpcWeb();
                
                endpoints.MapGrpcService<CarModelService>()
                    .EnableGrpcWeb();

                endpoints.MapGrpcService<CarModificationService>()
                    .EnableGrpcWeb();

                endpoints.MapGrpcService<UserService>()
                    .EnableGrpcWeb();

                endpoints.MapGrpcService<SupplierService>()
                    .EnableGrpcWeb();

                endpoints.MapGrpcService<CatalogService>()
                    .EnableGrpcWeb();

                endpoints.MapGrpcService<CountryService>()
                    .EnableGrpcWeb();

                endpoints.MapGrpcService<ManufacturerService>()
                    .EnableGrpcWeb();

                endpoints.MapGrpcService<AutoPartService>()
                    .EnableGrpcWeb();

                endpoints.MapGrpcService<OrderService>()
                    .EnableGrpcWeb();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}

using AutoParts.Data.EF.Migrations;
using AutoParts.Data.Model.Entities;
using AutoParts.Utilities.Common.Constants;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using UserTypeEnum = AutoParts.Data.Model.Enums.UserType;

namespace AutoParts.Web.IdentityServer
{
    public class Startup
    {
        /// <summary>
        /// Gets the Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Gets the current hosting environment
        /// </summary>
        public IWebHostEnvironment Environment { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration <see cref="IConfiguration"/></param>
        /// <param name="environment">The environment <see cref="IWebHostEnvironment"/></param>
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<HstsOptions>(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(1);
            });

            services.ConfigureDataAccess(Configuration);

            services.AddMvc();

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
        {
            var allowedOrigins = new string[] { "http://localhost:4200" };

            if (environment.EnvironmentName == EnvironmentNames.Development)
            {
                InitializeDatabase(app, allowedOrigins)
                    .GetAwaiter()
                    .GetResult();

                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(builder => builder
                .WithOrigins(allowedOrigins)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
            );

            app.UseIdentityServer();
        }

        private async Task InitializeDatabase(IApplicationBuilder app, string[] corsOrigins)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

                if (!context.Clients.Any())
                {
                    foreach (var client in AuthenticationDataStore.GetClients(corsOrigins))
                    {
                        await context.Clients.AddAsync(client.ToEntity());
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in AuthenticationDataStore.GetIdentityResources())
                    {
                        await context.IdentityResources.AddAsync(resource.ToEntity());
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in AuthenticationDataStore.GetApiResources())
                    {
                        await context.ApiResources.AddAsync(resource.ToEntity());
                    }

                    await context.SaveChangesAsync();
                }

                await InitializeApiRoles(app, serviceScope);
            }
        }

        private async Task InitializeApiRoles(IApplicationBuilder app, IServiceScope serviceScope)
        {
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

            foreach (var userType in Enum.GetValues(typeof(UserTypeEnum)).Cast<UserTypeEnum>())
            {
                var role = new Role
                {
                    Name = userType.ToString()
                };

                var isRoleExist = await roleManager.RoleExistsAsync(role.Name);

                if (!isRoleExist)
                {
                    await roleManager.CreateAsync(role);
                }
            }
        }
    }
}

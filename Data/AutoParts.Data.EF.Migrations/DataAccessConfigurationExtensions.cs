namespace AutoParts.Data.EF.Migrations
{
    using System.Linq;
    using System.Reflection;

    using Microsoft.AspNetCore.Identity;

    using Microsoft.EntityFrameworkCore;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using IdentityServer4.EntityFramework.Options;

    using Data.Model.Entities;
    using Data.Model.Repositories;
    using Data.Model.Repositories.Base;

    using Data.EF;
    using Data.EF.Repositories;
    using Data.EF.Repositories.Base;

    using Utilities.Common.Extensions;

    public static class DataAccessConfigurationExtensions
    {
        public static void ConfigureDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            RegisterDatabaseContext(services, configuration);
            RegisterRepositories(services);

            services.AddScoped<IUserRepository, UserRepository>();
        }

        private static void RegisterDatabaseContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<DatabaseContext>(optionsBuilder => ConfigureSqlServerDatabase(optionsBuilder, configuration))
                .BuildServiceProvider();

            services.AddIdentity<User, Role>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 6;

                    options.User.AllowedUserNameCharacters = null;
                    options.User.RequireUniqueEmail = true;
                })
               .AddEntityFrameworkStores<DatabaseContext>()
               .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddConfigurationStore(options => ConfigureIdentityServerConfigurationStore(options, configuration))
                .AddOperationalStore(options => ConfigureIdentityServerOperationalStore(options, configuration))
                .AddAspNetIdentity<User>();

            services.AddScoped<IDatabaseContext, DatabaseContext>();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            var repositoryAbstractionsAssembly = typeof(IRepository<,>).Assembly;
            var repositoryImplementationsAssembly = typeof(Repository<,>).Assembly;

            var repositoryTypes = repositoryAbstractionsAssembly.GetTypes()
                .Where(type => type.ImplementGenericInterface(typeof(IRepository<,>)))
                .ToArray();

            var repositoryImplementationsAssemblyTypes = repositoryImplementationsAssembly.GetTypes();

            foreach (var repositoryType in repositoryTypes)
            {
                var repositoryImplementationType = repositoryImplementationsAssemblyTypes
                    .FirstOrDefault(type => repositoryType.IsAssignableFrom(type));

                services.AddScoped(repositoryType, repositoryImplementationType);
            }
        }

        private static void ConfigureSqlServerDatabase(DbContextOptionsBuilder optionsBuilder, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(nameof(DatabaseContext));
            var assemblyName = Assembly.GetExecutingAssembly()
                .GetName();

            optionsBuilder.UseSqlServer(
                connectionString,
                serverOptions => serverOptions.MigrationsAssembly(assemblyName.Name)
            );
        }

        private static void ConfigureIdentityServerConfigurationStore(ConfigurationStoreOptions options, IConfiguration configuration)
        {
            options.ConfigureDbContext = optionsBuilder => ConfigureSqlServerDatabase(optionsBuilder, configuration);
        }

        private static void ConfigureIdentityServerOperationalStore(OperationalStoreOptions options, IConfiguration configuration)
        {
            options.ConfigureDbContext = optionsBuilder => ConfigureSqlServerDatabase(optionsBuilder, configuration);
            options.EnableTokenCleanup = true;
        }
    }
}

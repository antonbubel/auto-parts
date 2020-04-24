namespace AutoParts.Core.Implementation
{
    using AutoMapper;

    using MediatR;

    using FluentValidation;

    using SendGrid;

    using System.Linq;
    using System.Reflection;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Infrastructure.CQS;

    using Constants;
    using Constants.Options;

    using Utilities.Common.Extensions;

    public static class BusinessLayerConfigurationExtensions
    {
        public static void ConfigureBusinessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();

            RegisterAutoMapper(services, executingAssembly);
            RegisterMediatR(services, executingAssembly);
            RegisterValidators(services, executingAssembly);
            RegisterSendGrid(services, configuration);
        }

        private static void RegisterAutoMapper(IServiceCollection services, Assembly assembly)
        {
            services.AddAutoMapper(assembly);
        }

        private static void RegisterMediatR(IServiceCollection services, Assembly assembly)
        {
            services.AddMediatR(
                configuration => configuration.Using<CustomMediator>(),
                assembly);
        }

        private static void RegisterValidators(IServiceCollection services, Assembly assembly)
        {
            var validatorTypes = assembly
                .GetTypes()
                .Where(type => type.ImplementGenericInterface(typeof(IValidator<>)))
                .ToList();

            validatorTypes.ForEach(validatorType =>
                services.AddTransient(validatorType.GetGenericInterfaceDefinition(typeof(IValidator<>)), validatorType));
        }

        private static void RegisterSendGrid(IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<SendGridOptions>()
                .Bind(configuration.GetSection(ConfigurationConstants.SendGridSectionKey))
                .ValidateDataAnnotations();

            var sendGridOptions = configuration.GetSection(ConfigurationConstants.SendGridSectionKey)
                .Get<SendGridOptions>();

            services.AddScoped<ISendGridClient>(x => new SendGridClient(sendGridOptions.ApiKey));
        }
    }
}

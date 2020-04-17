namespace AutoParts.Core.Implementation.Files.LocalFolderFileStorage.RequestHandlers
{
    using MediatR;

    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Constants;
    using Contracts.Files.Requests;
    using Microsoft.Extensions.Configuration;

    public class GetFileUrlRequestHandler : IRequestHandler<GetFileUrlRequest, string>
    {
        private readonly IConfiguration configuration;

        public GetFileUrlRequestHandler(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Task<string> Handle(GetFileUrlRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{nameof(request)} of type {nameof(GetFileUrlRequest)} argument cannot be null.");
            }

            var apiBaseUrl = configuration.GetValue<string>(ConfigurationConstants.ApiBaseUrlConfigurationSectionKey);
            var fileUrl = $"{apiBaseUrl}/{FileConstants.LocalFilesFolderName}/{request.FileName}";

            return Task.FromResult(fileUrl);
        }
    }
}

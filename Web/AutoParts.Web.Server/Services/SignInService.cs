namespace AutoParts.Web.Server.Services
{
    using AutoMapper;

    using Grpc.Core;

    using Microsoft.Extensions.Logging;
    
    using System.Threading.Tasks;

    using Protos;

    using Infrastructure.Web.Authorization;

    public class SignInService : GrpcSignInService.GrpcSignInServiceBase
    {
        private readonly IMapper mapper;
        private readonly IIdentityClient identityClient;
        private readonly ILogger<SignInService> logger;

        public SignInService(IMapper mapper, IIdentityClient identityClient, ILogger<SignInService> logger)
        {
            this.mapper = mapper;
            this.identityClient = identityClient;
            this.logger = logger;
        }

        public override async Task<UserSignInResponse> SignIn(UserSignInRequest request, ServerCallContext context)
        {
            logger.LogDebug($"User with email is about to sign in, email: {request.Email}");

            var tokenResponse = await identityClient.GetAccessTokenAsync(request.Email, request.Password);

            if (tokenResponse.IsError && tokenResponse.Exception != null)
            {
                logger.LogError(tokenResponse.Exception, $"User sign in failed with an exception. Error: {tokenResponse.Error}. Error description: {tokenResponse.ErrorDescription}");
            }
            else if (!tokenResponse.IsError)
            {
                logger.LogDebug($"User with email {request.Email} successfully signed in.");
            }
            else
            {
                logger.LogDebug($"User sign in failed with an error.  Error: {tokenResponse.Error}. Error description: {tokenResponse.ErrorDescription}");
            }

            return mapper.Map<UserSignInResponse>(tokenResponse);
        }
    }
}

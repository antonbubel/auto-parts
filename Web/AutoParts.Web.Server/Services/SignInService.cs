namespace AutoParts.Web.Server.Services
{
    using Grpc.Core;

    using Microsoft.Extensions.Logging;
    
    using System.Threading.Tasks;

    using Protos;

    using Infrastructure.Web.Authorization;

    public class SignInService : GrpcSignInService.GrpcSignInServiceBase
    {
        private readonly IIdentityClient identityClient;
        private readonly ILogger<SignInService> logger;

        public SignInService(IIdentityClient identityClient, ILogger<SignInService> logger)
        {
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

            return new UserSignInResponse
            {
                AccessToken = tokenResponse.AccessToken ?? string.Empty,
                IdentityToken = tokenResponse.IdentityToken ?? string.Empty,
                TokenType = tokenResponse.TokenType ?? string.Empty,
                RefreshToken = tokenResponse.RefreshToken ?? string.Empty,
                IsError = tokenResponse.IsError,
                Error = tokenResponse.Error ?? string.Empty,
                ErrorDescription = tokenResponse.ErrorDescription ?? string.Empty,
                ExpiresIn = tokenResponse.ExpiresIn
            };
        }
    }
}

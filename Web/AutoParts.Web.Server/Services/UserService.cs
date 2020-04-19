namespace AutoParts.Web.Server.Services
{
    using MediatR;

    using Grpc.Core;

    using Microsoft.AspNetCore.Authorization;

    using System;
    using System.Threading.Tasks;

    using Protos;

    using Core.Contracts.Users.Requests;

    using Infrastructure.Web.Authorization;

    public class UserService : GrpcUserService.GrpcUserServiceBase
    {
        private readonly IMediator mediator;
        private readonly IIdentityClient identityClient;

        public UserService(IMediator mediator, IIdentityClient identityClient)
        {
            this.mediator = mediator;
            this.identityClient = identityClient;
        }

        [Authorize]
        public override async Task<GetCurrentUserInfoResponse> GetCurrentUserInfo(GetCurrentUserInfoRequest request, ServerCallContext context)
        {
            var userId = context.GetLoggedInUserId();

            var user = await mediator.Send(new GetUserInfoRequest { UserId = userId.Value });

            return new GetCurrentUserInfoResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserType = (UserType)Enum.Parse(typeof(UserType), user.UserType.ToString())
            };
        }

        public override async Task<GetRefreshedTokenResponse> GetRefreshedToken(GetRefreshedTokenRequest request, ServerCallContext context)
        {
            var response = await identityClient.GetRefreshedTokenAsync(request.RefreshToken);

            return new GetRefreshedTokenResponse
            {
                AccessToken = response.AccessToken ?? string.Empty,
                RefreshToken = response.RefreshToken ?? string.Empty,
                TokenType = response.TokenType ?? string.Empty,
                IsError = response.IsError,
                Error = response.Error ?? string.Empty,
                ErrorDescription = response.ErrorDescription ?? string.Empty
            };
        }

        [Authorize]
        public override async Task<SignOutResponse> SignOut(SignOutRequest request, ServerCallContext context)
        {
            await identityClient.RevokeAccessTokenAsync(request.RefreshToken);

            return new SignOutResponse();
        }
    }
}

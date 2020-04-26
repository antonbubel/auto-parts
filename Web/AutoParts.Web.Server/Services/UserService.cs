namespace AutoParts.Web.Server.Services
{
    using AutoMapper;

    using MediatR;

    using Grpc.Core;

    using Microsoft.AspNetCore.Authorization;

    using System.Threading.Tasks;

    using Protos;

    using Core.Contracts.Users.Requests;

    using Infrastructure.Web.Authorization;

    public class UserService : GrpcUserService.GrpcUserServiceBase
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly IIdentityClient identityClient;

        public UserService(IMapper mapper, IMediator mediator, IIdentityClient identityClient)
        {
            this.mapper = mapper;
            this.mediator = mediator;
            this.identityClient = identityClient;
        }

        [Authorize]
        public override async Task<GetCurrentUserInfoResponse> GetCurrentUserInfo(GetCurrentUserInfoRequest request, ServerCallContext context)
        {
            var userId = context.GetLoggedInUserId();
            var user = await mediator.Send(new GetUserInfoRequest { UserId = userId.Value });

            return mapper.Map<GetCurrentUserInfoResponse>(user);
        }

        [AllowAnonymous]
        public override async Task<GetRefreshedTokenResponse> GetRefreshedToken(GetRefreshedTokenRequest request, ServerCallContext context)
        {
            var response = await identityClient.GetRefreshedTokenAsync(request.RefreshToken);

            return mapper.Map<GetRefreshedTokenResponse>(response);
        }

        [Authorize]
        public override async Task<SignOutResponse> SignOut(SignOutRequest request, ServerCallContext context)
        {
            await identityClient.RevokeAccessTokenAsync(request.RefreshToken);

            return new SignOutResponse();
        }
    }
}

namespace AutoParts.Web.Server.Services
{
    using MediatR;

    using Grpc.Core;

    using Microsoft.AspNetCore.Authorization;

    using System;
    using System.Threading.Tasks;

    using Protos;

    using Core.Contracts.Users.Requests;

    public class UserService : GrpcUserService.GrpcUserServiceBase
    {
        private readonly IMediator mediator;

        public UserService(IMediator mediator)
        {
            this.mediator = mediator;
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
    }
}

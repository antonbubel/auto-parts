namespace AutoParts.Core.Contracts.Users.Requests
{
    using MediatR;

    using Models;

    public class GetUserInfoRequest : IRequest<UserInfoModel>
    {
        public long UserId { get; set; }
    }
}

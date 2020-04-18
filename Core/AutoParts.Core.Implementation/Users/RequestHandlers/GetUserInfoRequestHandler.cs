namespace AutoParts.Core.Implementation.Users.RequestHandlers
{
    using MediatR;

    using AutoMapper;

    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.Users.Models;
    using Contracts.Users.Requests;

    using Data.Model.Repositories;

    public class GetUserInfoRequestHandler : IRequestHandler<GetUserInfoRequest, UserInfoModel>
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public GetUserInfoRequestHandler(IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public async Task<UserInfoModel> Handle(GetUserInfoRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{nameof(request)} of type {nameof(GetUserInfoRequest)} argument cannot be null.");
            }

            var user = await userRepository.FindAsync(request.UserId)
                .ConfigureAwait(false);

            return mapper.Map<UserInfoModel>(user);
        }
    }
}

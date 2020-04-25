namespace AutoParts.Core.Implementation.Users.RequestHandlers
{
    using MediatR;

    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.Users.Requests;

    using Data.Model.Repositories;

    public class UserExistsByEmailRequestHandler : IRequestHandler<UserExistsByEmailRequest, bool>
    {
        private readonly IUserRepository userRepository;

        public UserExistsByEmailRequestHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<bool> Handle(UserExistsByEmailRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{nameof(request)} of type {nameof(UserExistsByEmailRequestHandler)} argument cannot be null.");
            }

            return await userRepository.UserExistsByEmail(request.Email)
                .ConfigureAwait(false);
        }
    }
}

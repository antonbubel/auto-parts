namespace AutoParts.Core.Implementation.Users.NotificationHandlers
{
    using MediatR;

    using AutoMapper;

    using Microsoft.AspNetCore.Identity;

    using Microsoft.Extensions.Logging;

    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Security.Claims;
    using System.Collections.Generic;

    using Data.Model.Entities;

    using Contracts.Users.Exceptions;
    using Contracts.Users.Notifications;

    using Infrastructure.Exceptions.Models;

    public class UserSignUpNotificationHanler : INotificationHandler<UserSignUpNotification>
    {
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly ILogger<UserSignUpNotificationHanler> logger;

        public UserSignUpNotificationHanler(
            IMapper mapper,
            UserManager<User> userManager,
            ILogger<UserSignUpNotificationHanler> logger)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.logger = logger;
        }

        public async Task Handle(UserSignUpNotification notification, CancellationToken cancellationToken)
        {
            var user = mapper.Map<User>(notification);

            var createUserResult = await userManager.CreateAsync(user, notification.Password);

            ValidateIdentityResult(createUserResult, user);

            var addToRoleResult = await userManager.AddToRoleAsync(user, user.UserTypeId.ToString());

            ValidateIdentityResult(addToRoleResult, user);

            var addClaimsResult = await AddClaimsToUser(user);

            ValidateIdentityResult(addClaimsResult, user);

            logger.LogInformation($"Successfully created a user with username: {user.UserName}");
        }

        private async Task<IdentityResult> AddClaimsToUser(User user)
        {
            return await userManager.AddClaimsAsync(user, new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Role, user.UserTypeId.ToString())
            });
        }

        private void ValidateIdentityResult(IdentityResult result, User user)
        {
            if (!result.Succeeded)
            {
                logger.LogError($@"User with username {user.UserName} creation failed with identity errors:
                    {string.Join(", ", result.Errors.Select(error => error.Description))}");

                var errors = mapper.Map<Error[]>(result.Errors);

                throw new UserSignUpException(errors);
            }
        }
    }
}

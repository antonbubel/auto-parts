namespace AutoParts.Core.Implementation.Suppliers.NotificationHandlers
{
    using MediatR;

    using AutoMapper;

    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Security.Claims;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;

    using Contracts.Suppliers.Exceptions;
    using Contracts.Suppliers.Notifications;

    using Data.Model.Entities;
    using Data.Model.Repositories;

    using Infrastructure.Exceptions.Models;

    public class SupplierSignUpNotificationHandler : INotificationHandler<SupplierSignUpNotification>
    {
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly ISupplierProfileRepository supplierProfileRepository;
        private readonly ISupplierInvitationRepository supplierInvitationRepository;
        private readonly ILogger<SupplierSignUpNotificationHandler> logger;

        public SupplierSignUpNotificationHandler(
            IMapper mapper,
            UserManager<User> userManager,
            ISupplierProfileRepository supplierProfileRepository,
            ISupplierInvitationRepository supplierInvitationRepository,
            ILogger<SupplierSignUpNotificationHandler> logger)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.supplierProfileRepository = supplierProfileRepository;
            this.supplierInvitationRepository = supplierInvitationRepository;
            this.logger = logger;
        }

        public async Task Handle(SupplierSignUpNotification notification, CancellationToken cancellationToken)
        {
            var supplierInvitation = await supplierInvitationRepository.GetInvitationByToken(notification.InvitationToken)
                .ConfigureAwait(false);

            if (supplierInvitation == null)
            {
                throw new SupplierSignUpException();
            }

            var user = mapper.Map<User>(notification);

            user.UserName = supplierInvitation.Email;
            user.Email = supplierInvitation.Email;

            await CreateUser(user, notification);

            await CreateSupplierProfile(user, notification);

            await supplierInvitationRepository.RemoveAsync(supplierInvitation.Id)
                .ConfigureAwait(false);

            logger.LogInformation($"Successfully created a user with username: {user.UserName}");
        }

        private async Task CreateUser(User user, SupplierSignUpNotification notification)
        {
            var createUserResult = await userManager.CreateAsync(user, notification.Password)
                .ConfigureAwait(false);

            ValidateIdentityResult(createUserResult, user);

            var addToRoleResult = await userManager.AddToRoleAsync(user, user.UserTypeId.ToString())
                .ConfigureAwait(false);

            ValidateIdentityResult(addToRoleResult, user);

            var addClaimsResult = await AddClaimsToUser(user)
                .ConfigureAwait(false);

            ValidateIdentityResult(addClaimsResult, user);
        }

        private async Task CreateSupplierProfile(User user, SupplierSignUpNotification notification)
        {
            var supplierProfile = mapper.Map<SupplierProfile>(notification);

            supplierProfile.Id = user.Id;

            await supplierProfileRepository.CreateAsync(supplierProfile)
                .ConfigureAwait(false);
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

                throw new SupplierSignUpException(errors);
            }
        }
    }
}

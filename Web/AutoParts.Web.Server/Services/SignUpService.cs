namespace AutoParts.Web.Server.Services
{
    using Grpc.Core;

    using MediatR;

    using FluentValidation;

    using System.Threading.Tasks;

    using Protos;

    using Core.Contracts.Users.Exceptions;
    using Core.Contracts.Users.Notifications;

    using Core.Contracts.Suppliers.Exceptions;
    using Core.Contracts.Suppliers.Notifications;

    public class SignUpService : GrpcSignUpService.GrpcSignUpServiceBase
    {
        private readonly IMediator mediator;

        public SignUpService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override async Task<UserSignUpResponse> UserSignUp(UserSignUpRequest request, ServerCallContext context)
        {
            var notification = new UserSignUpNotification()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = request.Password,
                PasswordConfirmation = request.PasswordConfirmation
            };

            try
            {
                await mediator.Publish(notification);
            }
            catch (ValidationException exception)
            {
                return new UserSignUpResponse
                {
                    IsError = true,
                    Error = exception.Message
                };
            }
            catch (UserSignUpException exception)
            {
                return new UserSignUpResponse
                {
                    IsError = true,
                    Error = exception.Message
                };
            }

            return new UserSignUpResponse
            {
                IsError = false
            };
        }

        public override async Task<SupplierSignUpResponse> SupplierSignUp(SupplierSignUpRequest request, ServerCallContext context)
        {
            var notification = new SupplierSignUpNotification()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Password = request.Password,
                PasswordConfirmation = request.PasswordConfirmation,
                OrganizationName = request.OrganizationName,
                OrganizationAddress = request.OrganizationAddress,
                Website = request.Website,
                InvitationToken = request.InvitationToken
            };

            try
            {
                await mediator.Publish(notification);
            }
            catch (ValidationException exception)
            {
                return new SupplierSignUpResponse
                {
                    IsError = true,
                    Error = exception.Message
                };
            }
            catch (SupplierSignUpException exception)
            {
                return new SupplierSignUpResponse
                {
                    IsError = true,
                    Error = exception.Message
                };
            }

            return new SupplierSignUpResponse
            {
                IsError = false
            };
        }
    }
}

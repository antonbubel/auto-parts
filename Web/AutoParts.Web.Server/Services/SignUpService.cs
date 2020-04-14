namespace AutoParts.Web.Server.Services
{
    using Grpc.Core;

    using MediatR;

    using FluentValidation;

    using Microsoft.Extensions.Logging;
    
    using System.Threading.Tasks;

    using Protos;

    using Core.Contracts.Users.Exceptions;
    using Core.Contracts.Users.Notifications;

    public class SignUpService : GrpcSignUpService.GrpcSignUpServiceBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<SignUpService> logger;

        public SignUpService(IMediator mediator, ILogger<SignUpService> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
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
    }
}

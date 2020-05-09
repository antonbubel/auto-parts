namespace AutoParts.Web.Server.Services
{
    using AutoMapper;

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
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public SignUpService(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        public override async Task<ServiceResponse> UserSignUp(UserSignUpRequest request, ServerCallContext context)
        {
            var notification = mapper.Map<UserSignUpNotification>(request);

            try
            {
                await mediator.Publish(notification);
            }
            catch (ValidationException exception)
            {
                return ServiceResponseBuilder.FromValidationException(exception);
            }
            catch (UserSignUpException exception)
            {
                return ServiceResponseBuilder.FromApiException(exception);
            }

            return ServiceResponseBuilder.Ok;
        }

        public override async Task<ServiceResponse> SupplierSignUp(SupplierSignUpRequest request, ServerCallContext context)
        {
            var notification = mapper.Map<SupplierSignUpNotification>(request);

            try
            {
                await mediator.Publish(notification);
            }
            catch (ValidationException exception)
            {
                return ServiceResponseBuilder.FromValidationException(exception);
            }
            catch (SupplierSignUpException exception)
            {
                return ServiceResponseBuilder.FromApiException(exception);
            }

            return ServiceResponseBuilder.Ok;
        }
    }
}

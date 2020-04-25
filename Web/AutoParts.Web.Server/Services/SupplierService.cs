namespace AutoParts.Web.Server.Services
{
    using MediatR;

    using FluentValidation;

    using Grpc.Core;

    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;

    using Protos;

    using Core.Contracts.Suppliers.Requests;
    using Core.Contracts.Suppliers.Exceptions;
    using Core.Contracts.Suppliers.Notifications;

    using Infrastructure.Exceptions;

    public class SupplierService : GrpcSupplierService.GrpcSupplierServiceBase
    {
        private readonly IMediator mediator;

        public SupplierService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(nameof(UserType.Administrator))]
        public override async Task<InviteSupplierResponse> InviteSupplier(InviteSupplierRequest request, ServerCallContext context)
        {
            var notification = new InviteSupplierNotification
            {
                Email = request.Name,
                Name = request.Name
            };

            try
            {
                await mediator.Publish(notification);
            }
            catch (ValidationException exception)
            {
                return new InviteSupplierResponse
                {
                    IsError = true,
                    Error = exception.Message
                };
            }
            catch (InviteSupplierException exception)
            {
                return new InviteSupplierResponse
                {
                    IsError = true,
                    Error = exception.Message
                };
            }

            return new InviteSupplierResponse
            {
                IsError = false
            };
        }

        public override async Task<GetSupplierEmailFromInvitationResponse> GetSupplierEmailFromInvitation(GetSupplierEmailFromInvitationRequest request, ServerCallContext context)
        {
            var email = string.Empty;

            try
            {
                email = await mediator.Send(new GetSupplierEmailByInvitationTokenRequest { Token = request.InvitationToken });
            }
            catch (NotFoundException)
            {
                return new GetSupplierEmailFromInvitationResponse
                {
                    Email = email,
                    Status = RequestStatus.NotFound
                };
            }

            return new GetSupplierEmailFromInvitationResponse
            {
                Email = email,
                Status = RequestStatus.Ok
            };
        }
    }
}

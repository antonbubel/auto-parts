namespace AutoParts.Web.Server.Services
{
    using MediatR;

    using FluentValidation;

    using Grpc.Core;

    using System.Threading.Tasks;

    using Protos;

    using Core.Contracts.Suppliers.Exceptions;
    using Core.Contracts.Suppliers.Notifications;

    public class SupplierService : GrpcSupplierService.GrpcSupplierServiceBase
    {
        private readonly IMediator mediator;

        public SupplierService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override async Task<InviteSupplierResponse> IniteSupplier(InviteSupplierRequest request, ServerCallContext context)
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
    }
}

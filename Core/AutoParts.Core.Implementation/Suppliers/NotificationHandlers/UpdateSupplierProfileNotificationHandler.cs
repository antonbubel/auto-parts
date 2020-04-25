namespace AutoParts.Core.Implementation.Suppliers.NotificationHandlers
{
    using MediatR;

    using AutoMapper;

    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.Files.Requests;
    using Contracts.Files.Notifications;

    using Contracts.Suppliers.Exceptions;
    using Contracts.Suppliers.Notifications;

    using Data.Model.Results;
    using Data.Model.Repositories;

    using Infrastructure.Exceptions;

    public class UpdateSupplierProfileNotificationHandler : INotificationHandler<UpdateSupplierProfileNotification>
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly ISupplierProfileRepository supplierProfileRepository;

        public UpdateSupplierProfileNotificationHandler(IMapper mapper, IMediator mediator, ISupplierProfileRepository supplierProfileRepository)
        {
            this.mapper = mapper;
            this.mediator = mediator;
            this.supplierProfileRepository = supplierProfileRepository;
        }

        public async Task Handle(UpdateSupplierProfileNotification notification, CancellationToken cancellationToken)
        {
            var supplierProfile = await supplierProfileRepository.FindAsync(notification.UserId)
                .ConfigureAwait(false);

            if (supplierProfile == null)
            {
                throw new NotFoundException();
            }

            mapper.Map(notification, supplierProfile);

            if (!string.IsNullOrEmpty(notification.LogoFileName) && !notification.LogoFileBuffer.IsEmpty)
            {
                if (!string.IsNullOrEmpty(supplierProfile.Logo))
                {
                    await mediator.Publish(new DeleteFileNotification { FileName = supplierProfile.Logo });
                }

                supplierProfile.Logo = await mediator.Send(new SaveFileRequest { FileName = notification.LogoFileName, Buffer = notification.LogoFileBuffer });
            }

            var operationResult = await supplierProfileRepository.UpdateAsync(supplierProfile)
                .ConfigureAwait(false);

            if (operationResult.Status != OperationStatus.Successful)
            {
                throw new UpdateSupplierProfileException(operationResult);
            }
        }
    }
}

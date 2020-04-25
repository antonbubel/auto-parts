namespace AutoParts.Core.Implementation.Suppliers.NotificationHandlers
{
    using AutoMapper;

    using MediatR;

    using System.Threading;
    using System.Threading.Tasks;

    using Utilities.Common.Cryptography;

    using Contracts.Emails.Notifications;

    using Contracts.Suppliers.Exceptions;
    using Contracts.Suppliers.Notifications;

    using Data.Model.Results;
    using Data.Model.Entities;
    using Data.Model.Repositories;

    public class InviteSupplierNotificationHandler : INotificationHandler<InviteSupplierNotification>
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly ISupplierInvitationRepository supplierInvitationRepository;

        public InviteSupplierNotificationHandler(IMapper mapper, IMediator mediator, ISupplierInvitationRepository supplierInvitationRepository)
        {
            this.mapper = mapper;
            this.mediator = mediator;
            this.supplierInvitationRepository = supplierInvitationRepository;
        }

        public async Task Handle(InviteSupplierNotification notification, CancellationToken cancellationToken)
        {
            var supplierInvitation = mapper.Map<SupplierInvitation>(notification);

            supplierInvitation.Token = TokenGenerator.Generate();

            var operationResult = await supplierInvitationRepository.CreateAsync(supplierInvitation)
                .ConfigureAwait(false);

            if (operationResult.Status != OperationStatus.Successful)
            {
                throw new InviteSupplierException(operationResult);
            }

            var sendSupplierInvitationEmailNotification = mapper.Map<SendSupplierInvitationEmailNotification>(supplierInvitation);

            await mediator.Publish(sendSupplierInvitationEmailNotification)
                .ConfigureAwait(false);
        }
    }
}

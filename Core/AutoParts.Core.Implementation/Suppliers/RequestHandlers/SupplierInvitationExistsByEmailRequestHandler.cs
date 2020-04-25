namespace AutoParts.Core.Implementation.Suppliers.RequestHandlers
{
    using MediatR;

    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.Suppliers.Requests;

    using Data.Model.Repositories;

    public class SupplierInvitationExistsByEmailRequestHandler : IRequestHandler<SupplierInvitationExistsByEmailRequest, bool>
    {
        private readonly ISupplierInvitationRepository supplierInvitationRepository;

        public SupplierInvitationExistsByEmailRequestHandler(ISupplierInvitationRepository supplierInvitationRepository)
        {
            this.supplierInvitationRepository = supplierInvitationRepository;
        }

        public async Task<bool> Handle(SupplierInvitationExistsByEmailRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{nameof(request)} of type {nameof(SupplierInvitationExistsByEmailRequest)} argument cannot be null.");
            }

            return await supplierInvitationRepository.SupplierInvitationExistsByEmail(request.Email)
                .ConfigureAwait(false);
        }
    }
}

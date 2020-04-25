namespace AutoParts.Core.Implementation.Suppliers.RequestHandlers
{
    using MediatR;

    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Infrastructure.Exceptions;

    using Contracts.Suppliers.Requests;

    using Data.Model.Repositories;

    public class GetSupplierEmailByInvitationTokenRequestHandler : IRequestHandler<GetSupplierEmailByInvitationTokenRequest, string>
    {
        private readonly ISupplierInvitationRepository supplierInvitationRepository;

        public GetSupplierEmailByInvitationTokenRequestHandler(ISupplierInvitationRepository supplierInvitationRepository)
        {
            this.supplierInvitationRepository = supplierInvitationRepository;
        }

        public async Task<string> Handle(GetSupplierEmailByInvitationTokenRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{nameof(request)} of type {nameof(GetSupplierEmailByInvitationTokenRequest)} argument cannot be null.");
            }

            var supplierInvitation = await supplierInvitationRepository.GetInvitationByToken(request.Token)
                .ConfigureAwait(false);

            if (supplierInvitation == null)
            {
                throw new NotFoundException();
            }

            return supplierInvitation.Email;
        }
    }
}

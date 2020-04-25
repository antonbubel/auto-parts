namespace AutoParts.Core.Implementation.Suppliers.RequestHandlers
{
    using AutoMapper;

    using MediatR;

    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Common.Extensions;

    using Contracts.Suppliers.Models;
    using Contracts.Suppliers.Requests;

    using Contracts.Files.Requests;

    using Data.Model.Repositories;

    public class GetSuppliersRequestHandler : IRequestHandler<GetSuppliersRequest, SupplierShortPublicProfileModel[]>
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly ISupplierProfileRepository supplierProfileRepository;

        public GetSuppliersRequestHandler(IMapper mapper, IMediator mediator, ISupplierProfileRepository supplierProfileRepository)
        {
            this.mapper = mapper;
            this.mediator = mediator;
            this.supplierProfileRepository = supplierProfileRepository;
        }

        public async Task<SupplierShortPublicProfileModel[]> Handle(GetSuppliersRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{nameof(request)} of type {nameof(GetSupplierEmailByInvitationTokenRequest)} argument cannot be null.");
            }

            var itemsToSkip = request.GetItemsToSkip();
            var itemsToTake = request.GetItemsToTake();

            var suppliers = await supplierProfileRepository.GetSuppliers(itemsToSkip, itemsToTake)
                .ConfigureAwait(false);

            var models = mapper.Map<SupplierShortPublicProfileModel[]>(suppliers);

            foreach (var supplier in models)
            {
                if (!string.IsNullOrEmpty(supplier.LogoUrl))
                {
                    supplier.LogoUrl = await mediator.Send(new GetFileUrlRequest { FileName = supplier.LogoUrl });
                }
            }

            return models;
        }
    }
}

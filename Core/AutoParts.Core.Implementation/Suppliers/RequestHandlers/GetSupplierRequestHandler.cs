namespace AutoParts.Core.Implementation.Suppliers.RequestHandlers
{
    using AutoMapper;

    using MediatR;

    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.Files.Requests;

    using Contracts.Suppliers.Models;
    using Contracts.Suppliers.Requests;

    using Data.Model.Repositories;

    using Infrastructure.Exceptions;

    public class GetSupplierRequestHandler : IRequestHandler<GetSupplierRequest, SupplierPublicProfileModel>
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly ISupplierProfileRepository supplierProfileRepository;

        public GetSupplierRequestHandler(IMapper mapper, IMediator mediator, ISupplierProfileRepository supplierProfileRepository)
        {
            this.mapper = mapper;
            this.mediator = mediator;
            this.supplierProfileRepository = supplierProfileRepository;
        }

        public async Task<SupplierPublicProfileModel> Handle(GetSupplierRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{nameof(request)} of type {nameof(GetSupplierRequest)} argument cannot be null.");
            }

            var supplierProfile = await supplierProfileRepository.FindAsync(request.SupplierId)
                .ConfigureAwait(false);

            if (supplierProfile == null)
            {
                throw new NotFoundException();
            }

            var supplierProfileModel = mapper.Map<SupplierPublicProfileModel>(supplierProfile);

            if (!string.IsNullOrEmpty(supplierProfileModel.LogoUrl))
            {
                supplierProfileModel.LogoUrl = await mediator.Send(new GetFileUrlRequest { FileName = supplierProfileModel.LogoUrl });
            }

            return supplierProfileModel;
        }
    }
}

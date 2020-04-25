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

    public class GetPrivateSupplierProfileRequestHandler : IRequestHandler<GetPrivateSupplierProfileRequest, SupplierPrivateProfileModel>
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly ISupplierProfileRepository supplierProfileRepository;

        public GetPrivateSupplierProfileRequestHandler(IMapper mapper, IMediator mediator, ISupplierProfileRepository supplierProfileRepository)
        {
            this.mapper = mapper;
            this.mediator = mediator;
            this.supplierProfileRepository = supplierProfileRepository;
        }

        public async Task<SupplierPrivateProfileModel> Handle(GetPrivateSupplierProfileRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{nameof(request)} of type {nameof(GetPrivateSupplierProfileRequest)} argument cannot be null.");
            }

            var supplierProfile = await supplierProfileRepository.FindAsync(request.UserId)
                .ConfigureAwait(false);

            if (supplierProfile == null)
            {
                throw new NotFoundException();
            }

            var supplierProfileModel = mapper.Map<SupplierPrivateProfileModel>(supplierProfile);

            if (!string.IsNullOrEmpty(supplierProfileModel.LogoUrl))
            {
                supplierProfileModel.LogoUrl = await mediator.Send(new GetFileUrlRequest { FileName = supplierProfileModel.LogoUrl });
            }

            return supplierProfileModel;
        }
    }
}

namespace AutoParts.Core.Implementation.Catalogs.RequestHandlers
{
    using MediatR;

    using AutoMapper;

    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.Catalogs.Models;
    using Contracts.Catalogs.Requests;

    using Data.Model.Repositories;

    using Infrastructure.Exceptions;

    public class GetSubCatalogByIdRequestHandler : IRequestHandler<GetSubCatalogByIdRequest, SubCatalogModel>
    {
        private readonly IMapper mapper;
        private readonly IAutoPartsCatalogSubGroupRepository autoPartsCatalogSubGroupRepository;

        public GetSubCatalogByIdRequestHandler(IMapper mapper, IAutoPartsCatalogSubGroupRepository autoPartsCatalogSubGroupRepository)
        {
            this.mapper = mapper;
            this.autoPartsCatalogSubGroupRepository = autoPartsCatalogSubGroupRepository;
        }

        public async Task<SubCatalogModel> Handle(GetSubCatalogByIdRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{nameof(request)} of type {nameof(GetSubCatalogByIdRequest)} argument cannot be null.");
            }

            var subCatalog = await autoPartsCatalogSubGroupRepository.GetAutoPartsCatalogSubGroupById(request.SubCatalogId)
                .ConfigureAwait(false);

            if (subCatalog == null)
            {
                throw new NotFoundException();
            }

            return mapper.Map<SubCatalogModel>(subCatalog);
        }
    }
}

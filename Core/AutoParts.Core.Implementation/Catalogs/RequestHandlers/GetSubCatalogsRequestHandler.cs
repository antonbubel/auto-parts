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

    public class GetSubCatalogsRequestHandler : IRequestHandler<GetSubCatalogsRequest, CatalogModel[]>
    {
        private readonly IMapper mapper;
        private readonly IAutoPartsCatalogSubGroupRepository autoPartsCatalogSubGroupRepository;

        public GetSubCatalogsRequestHandler(IMapper mapper, IAutoPartsCatalogSubGroupRepository autoPartsCatalogSubGroupRepository)
        {
            this.mapper = mapper;
            this.autoPartsCatalogSubGroupRepository = autoPartsCatalogSubGroupRepository;
        }

        public async Task<CatalogModel[]> Handle(GetSubCatalogsRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{nameof(request)} of type {nameof(GetSubCatalogsRequest)} argument cannot be null.");
            }

            var subCatalogs = await autoPartsCatalogSubGroupRepository.GetAutoPartsCatalogGroupSubGroups(request.CatalogId)
                .ConfigureAwait(false);

            return mapper.Map<CatalogModel[]>(subCatalogs);
        }
    }
}

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

    public class GetCatalogsRequestHandler : IRequestHandler<GetCatalogsRequest, CatalogModel[]>
    {
        private readonly IMapper mapper;
        private readonly IAutoPartsCatalogGroupRepository autoPartsCatalogGroupRepository;

        public GetCatalogsRequestHandler(IMapper mapper, IAutoPartsCatalogGroupRepository autoPartsCatalogGroupRepository)
        {
            this.mapper = mapper;
            this.autoPartsCatalogGroupRepository = autoPartsCatalogGroupRepository;
        }

        public async Task<CatalogModel[]> Handle(GetCatalogsRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{nameof(request)} of type {nameof(GetCatalogsRequest)} argument cannot be null.");
            }
            var catalogs = await autoPartsCatalogGroupRepository.GetAutoPartsCatalogGroups()
                .ConfigureAwait(false);

            return mapper.Map<CatalogModel[]>(catalogs);
        }
    }
}

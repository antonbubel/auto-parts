namespace AutoParts.Web.Server.Services
{
    using AutoMapper;

    using Grpc.Core;

    using MediatR;

    using Microsoft.AspNetCore.Authorization;

    using System.Threading.Tasks;

    using Protos;

    using Core.Contracts.Catalogs.Models;
    using Core.Contracts.Catalogs.Requests;

    using Infrastructure.Exceptions;

    public class CatalogService : GrpcCatalogService.GrpcCatalogServiceBase
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public CatalogService(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [AllowAnonymous]
        public override async Task<GetAutoPartsCatalogsResponse> GetCatalogs(GetAutoPartsCatalogsRequest request, ServerCallContext context)
        {
            var catalogs = await mediator.Send(new GetCatalogsRequest());
            var response = new GetAutoPartsCatalogsResponse();

            mapper.Map(catalogs, response.Catalogs);

            return response;
        }

        [AllowAnonymous]
        public override async Task<GetAutoPartsSubCatalogsResponse> GetSubCatalogs(GetAutoPartsSubCatalogsRequest request, ServerCallContext context)
        {
            var subCatalogs = await mediator.Send(new GetSubCatalogsRequest { CatalogId = request.CatalogId });
            var response = new GetAutoPartsSubCatalogsResponse();

            mapper.Map(subCatalogs, response.Catalogs);

            return response;
        }

        [AllowAnonymous]
        public override async Task<GetAutoPartsSubCatalogResponse> GetSubCatalog(GetAutoPartsSubCatalogRequest request, ServerCallContext context)
        {
            SubCatalogModel subCatalog;

            try
            {
                subCatalog = await mediator.Send(new GetSubCatalogByIdRequest { SubCatalogId = request.SubCatalogId });
            }
            catch (NotFoundException)
            {
                return new GetAutoPartsSubCatalogResponse
                {
                    Status = ResponseStatus.NotFound
                };
            }

            return new GetAutoPartsSubCatalogResponse
            {
                Status = ResponseStatus.Ok,
                Model = mapper.Map<SubCatalog>(subCatalog)
            };
        }
    }
}

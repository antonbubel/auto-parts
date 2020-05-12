namespace AutoParts.Web.Server.Services
{
    using MediatR;

    using AutoMapper;

    using Grpc.Core;

    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;

    using Protos;

    using GetCountriesRequestBL = Core.Contracts.Country.Requests.GetCountriesRequest;

    public class CountryService : GrpcCountryService.GrpcCountryServiceBase
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public CountryService(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [AllowAnonymous]
        public override async Task<GetCountriesResponse> GetCountries(GetCountriesRequest request, ServerCallContext context)
        {
            var countries = await mediator.Send(new GetCountriesRequestBL());
            var response = new GetCountriesResponse();

            mapper.Map(countries, response.Countries);

            return response;
        }
    }
}

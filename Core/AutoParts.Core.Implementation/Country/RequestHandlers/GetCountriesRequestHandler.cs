namespace AutoParts.Core.Implementation.Country.RequestHandlers
{
    using MediatR;

    using AutoMapper;

    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.Country.Models;
    using Contracts.Country.Requests;

    using Data.Model.Repositories;

    public class GetCountriesRequestHandler : IRequestHandler<GetCountriesRequest, CountryModel[]>
    {
        private readonly IMapper mapper;
        private readonly ICountryRepository countryRepository;

        public GetCountriesRequestHandler(IMapper mapper, ICountryRepository countryRepository)
        {
            this.mapper = mapper;
            this.countryRepository = countryRepository;
        }

        public async Task<CountryModel[]> Handle(GetCountriesRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{nameof(request)} of type {nameof(GetCountriesRequest)} argument cannot be null.");
            }

            var countries = await countryRepository.GetAllAsync()
                .ConfigureAwait(false);

            return mapper.Map<CountryModel[]>(countries);
        }
    }
}

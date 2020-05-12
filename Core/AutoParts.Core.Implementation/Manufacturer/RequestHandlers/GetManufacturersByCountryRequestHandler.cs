namespace AutoParts.Core.Implementation.Manufacturer.RequestHandlers
{
    using MediatR;

    using AutoMapper;

    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.Manufacturer.Models;
    using Contracts.Manufacturer.Requests;

    using Contracts.Files.Requests;

    using Data.Model.Repositories;

    public class GetManufacturersByCountryRequestHandler : IRequestHandler<GetManufacturersByCountryRequest, ManufacturerModel[]>
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly IManufacturerRepository manufacturerRepository;

        public GetManufacturersByCountryRequestHandler(
            IMediator mediator,
            IMapper mapper,
            IManufacturerRepository manufacturerRepository)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.manufacturerRepository = manufacturerRepository;
        }

        public async Task<ManufacturerModel[]> Handle(GetManufacturersByCountryRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{nameof(request)} of type {nameof(GetManufacturersByCountryRequest)} argument cannot be null.");
            }

            var projections = await manufacturerRepository.GetManufacturersByCountry(request.CountryId)
                .ConfigureAwait(false);

            var manufacturers = mapper.Map<ManufacturerModel[]>(projections);

            foreach (var manufacturer in manufacturers)
            {
                if (!string.IsNullOrEmpty(manufacturer.ImageUrl))
                {
                    manufacturer.ImageUrl = await mediator.Send(new GetFileUrlRequest { FileName = manufacturer.ImageUrl })
                        .ConfigureAwait(false);
                }
            }

            return manufacturers;
        }
    }
}

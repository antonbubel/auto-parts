namespace AutoParts.Core.Implementation.CarBrands.RequestHandlers
{
    using MediatR;

    using AutoMapper;

    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.CarBrands.Models;
    using Contracts.CarBrands.Requests;

    using Contracts.Files.Requests;

    using Data.Model.Repositories;

    public class GetCarBrandsRequestHandler : IRequestHandler<GetCarBrandsRequest, CarBrandModel[]>
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly ICarBrandRepository carBrandRepository;

        public GetCarBrandsRequestHandler(IMapper mapper, IMediator mediator, ICarBrandRepository carBrandRepository)
        {
            this.mapper = mapper;
            this.mediator = mediator;
            this.carBrandRepository = carBrandRepository;
        }

        public async Task<CarBrandModel[]> Handle(GetCarBrandsRequest request, CancellationToken cancellationToken)
        {
            var carBrands = await carBrandRepository.GetAllAsync()
                .ConfigureAwait(false);

            foreach (var carBrand in carBrands)
            {
                if (!string.IsNullOrEmpty(carBrand.Image))
                {
                    carBrand.Image = await mediator.Send(new GetFileUrlRequest { FileName = carBrand.Image });
                }
            }

            return mapper.Map<CarBrandModel[]>(carBrands);
        }
    }
}

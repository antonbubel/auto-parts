namespace AutoParts.Core.Implementation.CarBrands.RequestHandlers
{
    using MediatR;

    using AutoMapper;

    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.CarBrands.Models;
    using Contracts.CarBrands.Requests;

    using Data.Model.Repositories;

    public class GetCarBrandsRequestHandler : IRequestHandler<GetCarBrandsRequest, CarBrandModel[]>
    {
        private readonly IMapper mapper;
        private readonly ICarBrandRepository carBrandRepository;

        public GetCarBrandsRequestHandler(IMapper mapper, ICarBrandRepository carBrandRepository)
        {
            this.mapper = mapper;
            this.carBrandRepository = carBrandRepository;
        }

        public async Task<CarBrandModel[]> Handle(GetCarBrandsRequest request, CancellationToken cancellationToken)
        {
            var carBrands = await carBrandRepository.GetAllAsync()
                .ConfigureAwait(false);

            return mapper.Map<CarBrandModel[]>(carBrands);
        }
    }
}

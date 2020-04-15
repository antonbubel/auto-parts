namespace AutoParts.Core.Implementation.CarModels.RequestHandlers
{
    using MediatR;

    using AutoMapper;

    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.CarModels.Models;
    using Contracts.CarModels.Requests;

    using Data.Model.Repositories;

    public class GetCarModelsByBrandRequestHandler : IRequestHandler<GetCarModelsByBrandRequest, CarModelModel[]>
    {
        private readonly IMapper mapper;
        private readonly ICarModelRepository carModelRepository;

        public GetCarModelsByBrandRequestHandler(IMapper mapper, ICarModelRepository carModelRepository)
        {
            this.mapper = mapper;
            this.carModelRepository = carModelRepository;
        }

        public async Task<CarModelModel[]> Handle(GetCarModelsByBrandRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{nameof(request)} of type {nameof(GetCarModelsByBrandRequest)} argument cannot be null.");
            }

            var carModels = await carModelRepository.GetCarModelsByBrand(request.CarBrandId)
                .ConfigureAwait(false);

            return mapper.Map<CarModelModel[]>(carModels);
        }
    }
}

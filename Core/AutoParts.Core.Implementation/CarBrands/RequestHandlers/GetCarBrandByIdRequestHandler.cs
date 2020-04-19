namespace AutoParts.Core.Implementation.CarBrands.RequestHandlers
{
    using MediatR;

    using AutoMapper;

    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.CarBrands.Models;
    using Contracts.CarBrands.Requests;

    using Contracts.Files.Requests;

    using Data.Model.Repositories;

    public class GetCarBrandByIdRequestHandler : IRequestHandler<GetCarBrandByIdRequest, CarBrandModel>
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly ICarBrandRepository carBrandRepository;

        public GetCarBrandByIdRequestHandler(IMapper mapper, IMediator mediator, ICarBrandRepository carBrandRepository)
        {
            this.mapper = mapper;
            this.mediator = mediator;
            this.carBrandRepository = carBrandRepository;
        }

        public async Task<CarBrandModel> Handle(GetCarBrandByIdRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{nameof(request)} of type {nameof(GetCarBrandByIdRequest)} argument cannot be null.");
            }

            var carBrand = await carBrandRepository.FindAsync(request.CarBrandId);

            if (carBrand == null)
            {
                // TODO: throw more concrete exception instead of System.ArgumentException
                throw new ArgumentException();
            }

            carBrand.Image = await mediator.Send(new GetFileUrlRequest { FileName = carBrand.Image });

            return mapper.Map<CarBrandModel>(carBrand);
        }
    }
}

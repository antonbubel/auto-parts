namespace AutoParts.Core.Implementation.CarModels.RequestHandlers
{
    using MediatR;

    using AutoMapper;

    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.CarModels.Models;
    using Contracts.CarModels.Requests;

    using Contracts.Files.Requests;

    using Data.Model.Repositories;

    public class GetCarModelByIdRequestHandler : IRequestHandler<GetCarModelByIdRequest, CarModelModel>
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly ICarModelRepository carModelRepository;

        public GetCarModelByIdRequestHandler(IMapper mapper, IMediator mediator, ICarModelRepository carModelRepository)
        {
            this.mapper = mapper;
            this.mediator = mediator;
            this.carModelRepository = carModelRepository;
        }

        public async Task<CarModelModel> Handle(GetCarModelByIdRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{nameof(request)} of type {nameof(GetCarModelByIdRequest)} argument cannot be null.");
            }

            var carBrand = await carModelRepository.FindAsync(request.CarModelId);

            if (carBrand == null)
            {
                // TODO: throw more concrete exception instead of System.ArgumentException
                throw new ArgumentException();
            }

            carBrand.Image = await mediator.Send(new GetFileUrlRequest { FileName = carBrand.Image });

            return mapper.Map<CarModelModel>(carBrand);
        }
    }
}

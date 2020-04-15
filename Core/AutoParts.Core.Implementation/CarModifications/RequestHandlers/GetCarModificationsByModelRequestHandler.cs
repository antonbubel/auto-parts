namespace AutoParts.Core.Implementation.CarModels.RequestHandlers
{
    using MediatR;

    using AutoMapper;

    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.CarModifications.Models;
    using Contracts.CarModifications.Requests;

    using Data.Model.Entities;
    using Data.Model.Repositories;

    public class GetCarModificationsByModelRequestHandler : IRequestHandler<GetCarModificationsByModelRequest, CarModificationModel[]>
    {
        private readonly IMapper mapper;
        private readonly ICarModificationRepository carModificationRepository;

        public GetCarModificationsByModelRequestHandler(IMapper mapper, ICarModificationRepository carModificationRepository)
        {
            this.mapper = mapper;
            this.carModificationRepository = carModificationRepository;
        }

        public async Task<CarModificationModel[]> Handle(GetCarModificationsByModelRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{nameof(request)} of type {nameof(GetCarModificationsByModelRequest)} argument cannot be null.");
            }

            CarModification[] carModifications;

            if (request.Year.HasValue && request.Year.Value != default)
            {
                carModifications = await carModificationRepository.GetCarModificationsByCarModelAndYear(request.CarModelId, request.Year.Value)
                    .ConfigureAwait(false);
            }
            else
            {
                carModifications = await carModificationRepository.GetCarModificationsByCarModel(request.CarModelId)
                    .ConfigureAwait(false);
            }

            return mapper.Map<CarModificationModel[]>(carModifications);
        }
    }
}

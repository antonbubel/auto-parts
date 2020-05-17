namespace AutoParts.Core.Implementation.CarModifications.RequestHandlers
{
    using MediatR;

    using AutoMapper;

    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.CarModifications.Models;
    using Contracts.CarModifications.Requests;

    using Data.Model.Repositories;

    using Infrastructure.Exceptions;

    public class GetCarModificationByIdRequestHandler : IRequestHandler<GetCarModificationByIdRequest, CarModificationModel>
    {
        private readonly IMapper mapper;
        private readonly ICarModificationRepository carModificationRepository;

        public GetCarModificationByIdRequestHandler(IMapper mapper, ICarModificationRepository carModificationRepository)
        {
            this.mapper = mapper;
            this.carModificationRepository = carModificationRepository;
        }

        public async Task<CarModificationModel> Handle(GetCarModificationByIdRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{nameof(request)} of type {nameof(GetCarModificationByIdRequest)} argument cannot be null.");
            }

            var carModification = await carModificationRepository.FindAsync(request.CarModificationId)
                .ConfigureAwait(false);

            if (carModification == null)
            {
                throw new NotFoundException();
            }

            return mapper.Map<CarModificationModel>(carModification);
        }
    }
}

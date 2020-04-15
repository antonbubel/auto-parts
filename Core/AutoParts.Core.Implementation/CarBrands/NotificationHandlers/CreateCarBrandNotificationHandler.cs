namespace AutoParts.Core.Implementation.CarBrands.NotificationHandlers
{
    using MediatR;

    using AutoMapper;

    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.CarBrands.Exceptions;
    using Contracts.CarBrands.Notifications;

    using Data.Model.Results;
    using Data.Model.Entities;
    using Data.Model.Repositories;
    
    public class CreateCarBrandNotificationHandler : INotificationHandler<CreateCarBrandNotification>
    {
        private readonly IMapper mapper;
        private readonly ICarBrandRepository carBrandRepository;

        public CreateCarBrandNotificationHandler(IMapper mapper, ICarBrandRepository carBrandRepository)
        {
            this.mapper = mapper;
            this.carBrandRepository = carBrandRepository;
        }

        public async Task Handle(CreateCarBrandNotification notification, CancellationToken cancellationToken)
        {
            var carBrand = mapper.Map<CarBrand>(notification);

            var operationResult = await carBrandRepository.CreateAsync(carBrand)
                .ConfigureAwait(false);

            if (operationResult.Status != OperationStatus.Successful)
            {
                throw new CreateCarBrandException(operationResult);
            }
        }
    }
}

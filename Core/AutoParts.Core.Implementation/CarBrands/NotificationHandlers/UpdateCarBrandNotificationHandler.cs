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

    public class UpdateCarBrandNotificationHandler : INotificationHandler<UpdateCarBrandNotification>
    {
        private readonly IMapper mapper;
        private readonly ICarBrandRepository carBrandRepository;

        public UpdateCarBrandNotificationHandler(IMapper mapper, ICarBrandRepository carBrandRepository)
        {
            this.mapper = mapper;
            this.carBrandRepository = carBrandRepository;
        }

        public async Task Handle(UpdateCarBrandNotification notification, CancellationToken cancellationToken)
        {
            var carBrand = mapper.Map<CarBrand>(notification);

            var operationResult = await carBrandRepository.UpdateAsync(carBrand)
                .ConfigureAwait(false);

            if (operationResult.Status != OperationStatus.Successful)
            {
                throw new UpdateCarBrandException(operationResult);
            }
        }
    }
}

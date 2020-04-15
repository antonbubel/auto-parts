namespace AutoParts.Core.Implementation.CarBrands.RequestHandlers
{
    using MediatR;

    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.CarBrands.Requests;

    using Data.Model.Repositories;

    public class CarBrandExistsByNameRequestHandler : IRequestHandler<CarBrandExistsByNameRequest, bool>
    {
        private readonly ICarBrandRepository carBrandRepository;

        public CarBrandExistsByNameRequestHandler(ICarBrandRepository carBrandRepository)
        {
            this.carBrandRepository = carBrandRepository;
        }

        public async Task<bool> Handle(CarBrandExistsByNameRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{nameof(request)} of type {nameof(CarBrandExistsByNameRequest)} argument cannot be null.");
            }

            return await carBrandRepository.CarBrandWithNameExists(request.Name)
                .ConfigureAwait(false);
        }
    }
}

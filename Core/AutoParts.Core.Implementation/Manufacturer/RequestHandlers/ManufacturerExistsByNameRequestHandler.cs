namespace AutoParts.Core.Implementation.Manufacturer.RequestHandlers
{
    using MediatR;

    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.Manufacturer.Requests;

    using Data.Model.Repositories;

    public class ManufacturerExistsByNameRequestHandler : IRequestHandler<ManufacturerExistsByNameRequest, bool>
    {
        private readonly IManufacturerRepository manufacturerRepository;

        public ManufacturerExistsByNameRequestHandler(IManufacturerRepository manufacturerRepository)
        {
            this.manufacturerRepository = manufacturerRepository;
        }

        public async Task<bool> Handle(ManufacturerExistsByNameRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{nameof(request)} of type {nameof(ManufacturerExistsByNameRequest)} argument cannot be null.");
            }

            if (string.IsNullOrEmpty(request.Name))
            {
                throw new ArgumentException($"{nameof(ManufacturerExistsByNameRequest.Name)} argument on type {nameof(ManufacturerExistsByNameRequest)} cannot be null or empty.");
            }

            return await manufacturerRepository.ManufacturerExistsByName(request.Name)
                .ConfigureAwait(false);
        }
    }
}

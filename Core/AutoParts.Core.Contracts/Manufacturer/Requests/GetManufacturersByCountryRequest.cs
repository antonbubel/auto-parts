namespace AutoParts.Core.Contracts.Manufacturer.Requests
{
    using MediatR;

    using Models;

    public class GetManufacturersByCountryRequest : IRequest<ManufacturerModel[]>
    {
        public long CountryId { get; set; }
    }
}

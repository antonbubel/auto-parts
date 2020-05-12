namespace AutoParts.Core.Contracts.Country.Requests
{
    using MediatR;

    using Models;

    public class GetCountriesRequest : IRequest<CountryModel[]>
    {
    }
}

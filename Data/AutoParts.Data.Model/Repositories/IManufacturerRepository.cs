namespace AutoParts.Data.Model.Repositories
{
    using System.Threading.Tasks;

    using Base;
    using Entities;

    public interface IManufacturerRepository : IRepository<long, Manufacturer>
    {
        Task<Manufacturer[]> GetManufacturersByCountry(long countryId);
    }
}

namespace AutoParts.Data.Model.Repositories
{
    using System.Threading.Tasks;

    using Base;
    using Entities;
    using Projections;

    public interface IManufacturerRepository : IRepository<long, Manufacturer>
    {
        Task<ManufacturerProjection[]> GetManufacturersByCountry(long countryId);
    }
}

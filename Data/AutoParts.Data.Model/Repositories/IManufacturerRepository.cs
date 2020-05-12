namespace AutoParts.Data.Model.Repositories
{
    using System.Threading.Tasks;

    using Base;
    using Entities;
    using Projections;

    public interface IManufacturerRepository : IRepository<long, Manufacturer>
    {
        Task<bool> ManufacturerExistsByName(string name);

        Task<ManufacturerProjection[]> GetManufacturersByCountry(long countryId);
    }
}

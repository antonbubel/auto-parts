namespace AutoParts.Data.Model.Repositories
{
    using System.Threading.Tasks;

    using Base;
    using Entities;

    public interface ICarBrandRepository : IRepository<long, CarBrand>
    {
        Task<bool> CarBrandWithNameExists(string name);
    }
}

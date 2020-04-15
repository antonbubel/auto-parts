namespace AutoParts.Data.Model.Repositories
{
    using System.Threading.Tasks;
 
    using Base;
    using Entities;

    public interface ICarModelRepository : IRepository<long, CarModel>
    {
        Task<CarModel[]> GetCarModelsByBrand(long carBrandId);
    }
}

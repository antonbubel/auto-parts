namespace AutoParts.Data.Model.Repositories
{
    using System.Threading.Tasks;

    using Base;
    using Entities;

    public interface ICarModificationRepository : IRepository<long, CarModification>
    {
        Task<CarModification[]> GetCarModificationsByCarModel(long carModelId);

        Task<CarModification[]> GetCarModificationsByCarModelAndYear(long carModelId, int year);
    }
}

namespace AutoParts.Data.Model.Repositories
{
    using System.Threading.Tasks;

    using Base;
    using Entities;
    using Projections;

    public interface IAutoPartsCatalogGroupRepository : IRepository<long, AutoPartsCatalogGroup>
    {
        Task<AutoPartsCatalogGroupProjection[]> GetAutoPartsCatalogGroups();
    }
}

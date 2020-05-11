namespace AutoParts.Data.Model.Repositories
{
    using System.Threading.Tasks;

    using Base;
    using Entities;
    using Projections;

    public interface IAutoPartsCatalogSubGroupRepository : IRepository<long, AutoPartsCatalogSubGroup>
    {
        Task<AutoPartsCatalogSubGroupWithBaseGroupProjection> GetAutoPartsCatalogSubGroupById(long catalogSubGroupId);

        Task<AutoPartsCatalogSubGroupProjection[]> GetAutoPartsCatalogGroupSubGroups(long catalogGroupId);
    }
}

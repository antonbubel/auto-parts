﻿namespace AutoParts.Data.Model.Repositories
{
    using System.Threading.Tasks;

    using Base;
    using Enums;
    using Filters;
    using Results;
    using Entities;
    using Projections;

    public interface IAutoPartRepository : IRepository<long, AutoPart>
    {
        Task<User[]> GetAutoPartsSuppliers(long[] autoPartIds);

        Task<PaginatedResult<AutoPartProjection>> GetAutoParts(
            AutoPartsFilter filter,
            AutoPartsSortingOption sorting = AutoPartsSortingOption.Name,
            SortingDirection direction = SortingDirection.Ascending);
    }
}

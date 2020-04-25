namespace AutoParts.Core.Implementation.Common.Extensions
{
    using Contracts.Common.Models;

    public static class PaginationFilterExtensions
    {
        public static int GetItemsToTake(this PaginationFilterModel paginationFilter)
        {
            return paginationFilter.PageSize;
        }

        public static int GetItemsToSkip(this PaginationFilterModel paginationFilter)
        {
            return (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
        }
    }
}

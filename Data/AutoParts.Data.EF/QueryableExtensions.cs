namespace AutoParts.Data.EF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Model.Enums;

    public static class QueryableExtensions
    {
        /// <summary>
        /// Skips given number of items and takes given number of items
        /// </summary>
        /// <typeparam name="TSource">
        /// <see cref="IQueryable"/> generic type parameter
        /// </typeparam>
        /// <param name="skip">Number of items to skip</param>
        /// <param name="take">Number of items to take</param>
        /// <returns></returns>
        public static IQueryable<TSource> GetPartition<TSource>(this IQueryable<TSource> source, int skip, int take)
        {
            return source.Skip(skip)
                .Take(take);
        }

        /// <summary>
        /// Orders a <see cref="IQueryable"/> by given sorting. Order expression is being taken from sortingExpressions dictionary. 
        /// If <see cref="SortingDirectionAttribute" /> is not provided on the sorting enum value the default sorting order is Ascending
        /// </summary>
        /// <typeparam name="TSource">
        /// <see cref="IQueryable"/> generic type parameter
        /// </typeparam>
        /// <typeparam name="TSortingEnum">
        /// Enum type which represents sorting option
        /// </typeparam>
        /// <param name="source"></param>
        /// <param name="sortingExpressions">Dictionary of expressions of sorting options</param>
        /// <param name="sorting">Enum value which represents sorting option</param>
        /// <param name="sortingDirection">Enum value which represents order of sorting options</param>
        public static IQueryable<TSource> ApplyOrderByExpressionFromDictionary<TSource, TSortingEnum>(
            this IQueryable<TSource> source,
            IReadOnlyDictionary<TSortingEnum, Expression<Func<TSource, IComparable>>> sortingExpressions,
            TSortingEnum sorting,
            SortingDirection sortingDirection = SortingDirection.Descending
        )
            where TSortingEnum : struct, IConvertible
        {
            var sortingType = typeof(TSortingEnum);

            if (!sortingType.IsEnum)
            {
                throw new ArgumentException($"{nameof(TSortingEnum)} must be an enumerated type");
            }

            if (!sortingExpressions.ContainsKey(sorting))
            {
                return source;
            }

            if (sortingDirection == SortingDirection.Ascending)
            {
                return source.OrderBy(sortingExpressions[sorting]).AsQueryable();
            }

            return source.OrderByDescending(sortingExpressions[sorting]).AsQueryable();
        }
    }
}

﻿namespace AutoParts.Data.EF.Repositories
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Base;

    using Model.Enums;
    using Model.Filters;
    using Model.Entities;
    using Model.Projections;
    using Model.Repositories;

    public class AutoPartRepository : Repository<long, AutoPart>, IAutoPartRepository
    {
        private readonly IMapper mapper;
        private readonly IReadOnlyDictionary<AutoPartsSortingOption, Expression<Func<AutoPart, IComparable>>> autoPartsSortingExpressions =
            new Dictionary<AutoPartsSortingOption, Expression<Func<AutoPart, IComparable>>>
            {
                {
                    AutoPartsSortingOption.Name,
                    autoPart => autoPart.Name
                },
                {
                    AutoPartsSortingOption.Price,
                    autoPart => autoPart.Price
                },
                {
                    AutoPartsSortingOption.Quantity,
                    autoPart => autoPart.Quantity
                }
            };

        public AutoPartRepository(IDatabaseContext context, IMapper mapper) : base(context)
        {
            this.mapper = mapper;
        }

        public async Task<AutoPartProjection[]> GetAutoParts(
            AutoPartsFilter filter,
            AutoPartsSortingOption sorting = AutoPartsSortingOption.Name,
            SortingDirection direction = SortingDirection.Ascending)
        {
            if (filter == null)
            {
                throw new ArgumentNullException($"Parameter {nameof(filter)} of type {nameof(AutoPartsFilter)} cannot be null.");
            }

            var query = GetQueryable();

            if (filter.CarModificationId.HasValue)
            {
                query = query.Where(autoPart => autoPart.CarModificationId == filter.CarModificationId.Value);
            }

            if (filter.ManufacturerId.HasValue)
            {
                query = query.Where(autoPart => autoPart.ManufacturerId == filter.ManufacturerId.Value);
            }

            if (filter.CountryId.HasValue)
            {
                query = query.Where(autoPart => autoPart.CountryId == filter.CountryId.Value);
            }

            if (filter.AvailableOnly)
            {
                query = query.Where(autoPart => autoPart.IsAvailable);
            }

            if (!string.IsNullOrEmpty(filter.SearchText))
            {
                var normalizedSearchText = filter.SearchText.ToUpperInvariant();

                query = query.Where(autoPart => autoPart.NormalizedName.Contains(normalizedSearchText));
            }

            return await query
                .ApplyOrderByExpressionFromDictionary(autoPartsSortingExpressions, sorting, direction)
                .GetPartition(filter.ItemsToSkip, filter.ItemsToTake)
                .ProjectTo<AutoPartProjection>(mapper.ConfigurationProvider)
                .ToArrayAsync();
        }
    }
}

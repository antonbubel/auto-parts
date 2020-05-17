namespace AutoParts.Core.Implementation.AutoParts.RequestHandlers
{
    using MediatR;

    using AutoMapper;

    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    
    using Contracts.AutoParts.Models;
    using Contracts.AutoParts.Requests;

    using Contracts.Files.Requests;

    using Contracts.Common.Models;

    using Constants.Enums;

    using Data.Model.Enums;
    using Data.Model.Filters;
    using Data.Model.Repositories;

    public class GetAutoPartsRequestHandler : IRequestHandler<GetAutoPartsRequest, PageModel<AutoPartModel>>
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly IAutoPartRepository autoPartRepository;

        private readonly IReadOnlyDictionary<AutoPartsSortingType, KeyValuePair<AutoPartsSortingOption, SortingDirection>> autoPartsSortingTypeMap =
            new Dictionary<AutoPartsSortingType, KeyValuePair<AutoPartsSortingOption, SortingDirection>>
            {
                {
                    AutoPartsSortingType.NameAscending,
                    new KeyValuePair<AutoPartsSortingOption, SortingDirection>(AutoPartsSortingOption.Name, SortingDirection.Ascending)
                },
                {
                    AutoPartsSortingType.NameDescending,
                    new KeyValuePair<AutoPartsSortingOption, SortingDirection>(AutoPartsSortingOption.Name, SortingDirection.Descending)
                },
                {
                    AutoPartsSortingType.PriceAscending,
                    new KeyValuePair<AutoPartsSortingOption, SortingDirection>(AutoPartsSortingOption.Price, SortingDirection.Ascending)
                },
                {
                    AutoPartsSortingType.PriceDescending,
                    new KeyValuePair<AutoPartsSortingOption, SortingDirection>(AutoPartsSortingOption.Price, SortingDirection.Descending)
                },
                {
                    AutoPartsSortingType.QuantityAscending,
                    new KeyValuePair<AutoPartsSortingOption, SortingDirection>(AutoPartsSortingOption.Name, SortingDirection.Ascending)
                },
                {
                    AutoPartsSortingType.QuantityDescending,
                    new KeyValuePair<AutoPartsSortingOption, SortingDirection>(AutoPartsSortingOption.Quantity, SortingDirection.Descending)
                }
            };

        public GetAutoPartsRequestHandler(
            IMediator mediator,
            IMapper mapper,
            IAutoPartRepository autoPartRepository)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.autoPartRepository = autoPartRepository;
        }

        public async Task<PageModel<AutoPartModel>> Handle(GetAutoPartsRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{nameof(request)} of type {nameof(GetAutoPartsRequest)} argument cannot be null.");
            }

            if (!autoPartsSortingTypeMap.ContainsKey(request.SortBy))
            {
                throw new ArgumentException($"No key found with value {request.SortBy} in the {nameof(autoPartsSortingTypeMap)} dictionary. Cannot map the sorting expression.");
            }

            var filter = mapper.Map<AutoPartsFilter>(request);
            var sorting = autoPartsSortingTypeMap[request.SortBy];

            var result = await autoPartRepository.GetAutoParts(filter, sorting.Key, sorting.Value)
                .ConfigureAwait(false);

            var items = mapper.Map<AutoPartModel[]>(result.Items);

            foreach (var item in items)
            {
                if (!string.IsNullOrEmpty(item.ImageUrl))
                {
                    item.ImageUrl = await mediator.Send(new GetFileUrlRequest { FileName = item.ImageUrl });
                }
            }

            return new PageModel<AutoPartModel>
            {
                TotalNumberOfItems = result.TotalNumberOfItems,
                Items = items
            };
        }
    }
}

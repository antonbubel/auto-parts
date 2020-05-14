namespace AutoParts.Core.Contracts.AutoParts.Requests
{
    using MediatR;

    using Models;
    using Common.Models;

    using Constants.Enums;

    public class GetAutoPartsRequest : PaginationFilterModel, IRequest<PageModel<AutoPartModel>>
    {
        public string SearchText { get; set; }

        public long? CarModificationId { get; set; }

        public long? ManufacturerId { get; set; }

        public long? CountryId { get; set; }

        public long? SupplierId { get; set; }

        public bool AvailableOnly { get; set; }

        public AutoPartsSortingType SortBy { get; set; }
    }
}

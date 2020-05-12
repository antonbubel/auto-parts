namespace AutoParts.Core.Contracts.Manufacturer.Requests
{
    using MediatR;

    public class ManufacturerExistsByNameRequest : IRequest<bool>
    {
        public string Name { get; set; }
    }
}

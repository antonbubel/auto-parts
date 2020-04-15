namespace AutoParts.Core.Contracts.CarBrands.Requests
{
    using MediatR;

    public class CarBrandExistsByNameRequest : IRequest<bool>
    {
        public string Name { get; set; }
    }
}

namespace AutoParts.Core.Contracts.CarModels.Models
{
    public class CarModelModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long CarBrandId { get; set; }

        public string CarBrandName { get; set; }

        public string ImageUrl { get; set; }
    }
}

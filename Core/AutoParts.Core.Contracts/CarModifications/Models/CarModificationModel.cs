namespace AutoParts.Core.Contracts.CarModifications.Models
{
    public class CarModificationModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public long CarModelId { get; set; }

        public int Year { get; set; }
    }
}

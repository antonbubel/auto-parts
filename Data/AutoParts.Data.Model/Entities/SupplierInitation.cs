namespace AutoParts.Data.Model.Entities
{
    using Base;

    public class SupplierInitation : BaseEntity<long>
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string Token { get; set; }
    }
}

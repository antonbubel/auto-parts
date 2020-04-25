namespace AutoParts.Data.Model.Entities
{
    using Base;

    public class SupplierInvitation : BaseEntity<long>
    {
        public string Email { get; set; }

        public string NormalizedEmail { get; set; }

        public string Name { get; set; }

        public string Token { get; set; }
    }
}

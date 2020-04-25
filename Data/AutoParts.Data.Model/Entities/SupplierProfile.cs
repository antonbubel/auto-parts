namespace AutoParts.Data.Model.Entities
{
    using Base;

    public class SupplierProfile : BaseEntity<long>
    {
        public string OrganizationName { get; set; }

        public string OrganizationAddress { get; set; }

        public string OrganizationDescription { get; set; }

        public string SalesEmail { get; set; }

        public string SalesPhoneNumber { get; set; }

        public string Website { get; set; }

        public string Logo { get; set; }

        public User User { get; set; }
    }
}


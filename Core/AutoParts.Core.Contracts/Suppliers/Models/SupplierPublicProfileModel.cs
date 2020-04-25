namespace AutoParts.Core.Contracts.Suppliers.Models
{
    public class SupplierPublicProfileModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string OrganizationAddress { get; set; }

        public string OrganizationDescription { get; set; }

        public string SalesEmail { get; set; }

        public string SalesPhoneNumber { get; set; }

        public string Website { get; set; }

        public string LogoUrl { get; set; }
    }
}

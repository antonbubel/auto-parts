namespace AutoParts.Core.Contracts.Suppliers.Models
{
    public class SupplierPrivateProfileModel
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string OrganizationName { get; set; }

        public string OrganizationAddress { get; set; }

        public string OrganizationDescription { get; set; }

        public string SalesEmail { get; set; }

        public string SalesPhoneNumber { get; set; }

        public string Website { get; set; }

        public string LogoUrl { get; set; }
    }
}

namespace AutoParts.Data.Model.Entities
{
    using Base;

    public class UserShippingInfo : BaseEntity<long>
    {
        public string StreetAddress { get; set; }

        public string StreetAddressSecondLine { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string ZipCode { get; set; }

        public long CountryId { get; set; }

        public Country Country { get; set; }

        public long UserProfileId { get; set; }

        public UserProfile UserProfile { get; set; }
    }
}

namespace AutoParts.Core.Contracts.Orders.Models
{
    using System;

    public class OrderModel
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string StreetAddress { get; set; }

        public string StreetAddressSecondLine { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string ZipCode { get; set; }

        public string Comment { get; set; }

        public DateTime DateCreated { get; set; }

        public string CountryName { get; set; }
    }
}

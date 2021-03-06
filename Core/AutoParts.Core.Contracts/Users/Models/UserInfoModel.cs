﻿namespace AutoParts.Core.Contracts.Users.Models
{
    using Constants.Enums;

    public class UserInfoModel
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public UserType UserType { get; set; }
    }
}

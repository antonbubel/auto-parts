namespace AutoParts.Core.Constants.ValidationConstants
{
    public static class UserValidationConstants
    {
        public const int DefaultMaxLength = 50;

        public const int EmailMaxLength = 254;

        public const int PhoneNumberMinLength = 9;

        public const int PhoneNumberMaxLength = 16;

        public const string PhoneNumberFormat = @"^[+][0-9]*$";
    }
}

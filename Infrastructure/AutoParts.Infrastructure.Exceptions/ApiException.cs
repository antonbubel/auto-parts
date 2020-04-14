namespace AutoParts.Infrastructure.Exceptions
{
    using System;

    using Models;

    public class ApiException : Exception
    {
        public Error[] Errors { get; set; }

        public ApiException(string message) : base(message)
        {
        }

        public ApiException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ApiException(params Error[] errors)
        {
            Errors = errors;
        }
    }
}

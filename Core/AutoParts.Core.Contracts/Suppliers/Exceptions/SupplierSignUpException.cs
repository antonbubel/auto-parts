namespace AutoParts.Core.Contracts.Suppliers.Exceptions
{
    using Infrastructure.Exceptions;
    using Infrastructure.Exceptions.Models;

    public class SupplierSignUpException : ApiException
    {
        public SupplierSignUpException()
        {
        }

        public SupplierSignUpException(Error[] errors) : base(errors)
        {
        }
    }
}

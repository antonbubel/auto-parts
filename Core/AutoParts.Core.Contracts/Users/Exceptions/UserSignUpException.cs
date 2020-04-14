namespace AutoParts.Core.Contracts.Users.Exceptions
{
    using Infrastructure.Exceptions;
    using Infrastructure.Exceptions.Models;

    public class UserSignUpException : ApiException
    {
        public UserSignUpException(Error[] errors) : base(errors)
        {
        }
    }
}

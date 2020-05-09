namespace AutoParts.Web.Server
{
    using FluentValidation;

    using System.Linq;

    using Protos;

    using Infrastructure.Exceptions;

    public static class ServiceResponseBuilder
    {
        public static ServiceResponse Ok => new ServiceResponse
        {
            Status = ResponseStatus.Ok
        };

        public static ServiceResponse FromApiException(ApiException exception)
        {
            var response = new ServiceResponse
            {
                Status = ResponseStatus.BadRequest
            };

            var errors = exception.Errors
                .Select(error => new Error { Cause = error.Code, Message = error.Description })
                .ToArray();

            response.Errors.AddRange(errors);

            return response;
        }

        public static ServiceResponse FromValidationException(ValidationException exception)
        {
            var response = new ServiceResponse
            {
                Status = ResponseStatus.ValidationFailure
            };

            var errors = exception.Errors
                .Select(error => new Error { Cause = error.PropertyName, Message = error.ErrorMessage })
                .ToArray();

            response.Errors.AddRange(errors);

            return response;
        }
    }
}

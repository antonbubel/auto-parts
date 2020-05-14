namespace AutoParts.Core.Contracts.AutoParts.Exceptions
{
    using Infrastructure.Exceptions;

    using Data.Model.Results;
    using Data.Model.Entities;

    public class CreateAutoPartException : ApiException
    {
        public CreateAutoPartException(OperationResult<AutoPart> operationResult)
            : base(operationResult.Message, operationResult.Exception)
        {
        }
    }
}

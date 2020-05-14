namespace AutoParts.Core.Contracts.AutoParts.Exceptions
{
    using Infrastructure.Exceptions;

    using Data.Model.Results;
    using Data.Model.Entities;

    public class DeleteAutoPartException : ApiException
    {
        public DeleteAutoPartException(OperationResult<AutoPart> operationResult)
            : base(operationResult.Message, operationResult.Exception)
        {
        }
    }
}

namespace AutoParts.Core.Contracts.CarModifications.Exceptions
{
    using Infrastructure.Exceptions;

    using Data.Model.Results;
    using Data.Model.Entities;

    public class DeleteCarModificationException : ApiException
    {
        public DeleteCarModificationException(OperationResult<CarModification> operationResult)
            : base(operationResult.Message, operationResult.Exception)
        {
        }
    }
}

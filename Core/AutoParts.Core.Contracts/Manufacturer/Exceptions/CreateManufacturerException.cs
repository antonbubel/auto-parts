namespace AutoParts.Core.Contracts.Manufacturer.Exceptions
{
    using Infrastructure.Exceptions;

    using Data.Model.Results;
    using Data.Model.Entities;

    public class CreateManufacturerException : ApiException
    {
        public CreateManufacturerException(OperationResult<Manufacturer> operationResult)
            : base(operationResult.Message, operationResult.Exception)
        {
        }
    }
}

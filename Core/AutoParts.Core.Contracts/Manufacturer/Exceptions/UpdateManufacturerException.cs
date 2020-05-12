namespace AutoParts.Core.Contracts.Manufacturer.Exceptions
{
    using Infrastructure.Exceptions;

    using Data.Model.Results;
    using Data.Model.Entities;

    public class UpdateManufacturerException : ApiException
    {
        public UpdateManufacturerException(OperationResult<Manufacturer> operationResult)
            : base(operationResult.Message, operationResult.Exception)
        {
        }
    }
}

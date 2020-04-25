namespace AutoParts.Core.Contracts.Suppliers.Exceptions
{
    using Infrastructure.Exceptions;

    using Data.Model.Results;
    using Data.Model.Entities;

    public class UpdateSupplierProfileException : ApiException
    {
        public UpdateSupplierProfileException(OperationResult<SupplierProfile> operationResult)
            : base(operationResult.Message, operationResult.Exception)
        {
        }
    }
}

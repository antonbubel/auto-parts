namespace AutoParts.Core.Contracts.Suppliers.Exceptions
{
    using Infrastructure.Exceptions;

    using Data.Model.Results;
    using Data.Model.Entities;

    public class InviteSupplierException : ApiException
    {
        public InviteSupplierException(OperationResult<SupplierInvitation> operationResult)
            : base(operationResult.Message, operationResult.Exception)
        {
        }
    }
}

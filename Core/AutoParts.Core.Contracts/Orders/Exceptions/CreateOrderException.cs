namespace AutoParts.Core.Contracts.Orders.Exceptions
{
    using Infrastructure.Exceptions;

    using Data.Model.Results;
    using Data.Model.Entities;

    public class CreateOrderException : ApiException
    {
        public CreateOrderException(OperationResult<Order> operationResult)
            : base(operationResult.Message, operationResult.Exception)
        {
        }
    }
}

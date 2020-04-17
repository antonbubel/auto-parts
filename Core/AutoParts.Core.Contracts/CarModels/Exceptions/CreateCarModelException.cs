namespace AutoParts.Core.Contracts.CarModels.Exceptions
{
    using Infrastructure.Exceptions;

    using Data.Model.Results;
    using Data.Model.Entities;

    public class CreateCarModelException : ApiException
    {
        public CreateCarModelException(OperationResult<CarModel> operationResult)
            : base(operationResult.Message, operationResult.Exception)
        {
        }
    }
}

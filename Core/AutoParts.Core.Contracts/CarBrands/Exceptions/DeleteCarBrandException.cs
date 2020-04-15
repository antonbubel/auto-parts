namespace AutoParts.Core.Contracts.CarBrands.Exceptions
{
    using Infrastructure.Exceptions;

    using Data.Model.Results;
    using Data.Model.Entities;

    public class DeleteCarBrandException : ApiException
    {
        public DeleteCarBrandException(OperationResult<CarBrand> operationResult)
            : base(operationResult.Message, operationResult.Exception)
        {
        }
    }
}

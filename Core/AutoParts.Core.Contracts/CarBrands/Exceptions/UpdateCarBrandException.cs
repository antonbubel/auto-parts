namespace AutoParts.Core.Contracts.CarBrands.Exceptions
{
    using Infrastructure.Exceptions;

    using Data.Model.Results;
    using Data.Model.Entities;

    public class UpdateCarBrandException : ApiException
    {
        public UpdateCarBrandException(OperationResult<CarBrand> operationResult)
            : base(operationResult.Message, operationResult.Exception)
        {
        }
    }
}

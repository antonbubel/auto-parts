namespace AutoParts.Data.Model.Results
{
    using System;

    public class OperationResult<TEntity> where TEntity : class
    {
        public OperationResult(OperationStatus status)
        {
            Status = status;
        }

        public OperationResult(OperationStatus status, string message)
            : this(status)
        {
            Message = message;
        }

        public OperationResult(OperationStatus status, Exception exception)
            : this(status, exception.Message)
        {
            Exception = exception;
        }

        public OperationResult(OperationStatus status, TEntity entity)
            : this(status)
        {
            Entity = entity;
        }

        public OperationStatus Status { get; private set; }

        public string Message { get; private set; }

        public Exception Exception { get; private set; }

        public TEntity Entity { get; private set; }
    }
}

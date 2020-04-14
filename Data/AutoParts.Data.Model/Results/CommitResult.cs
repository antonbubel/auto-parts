namespace AutoParts.Data.Model.Results
{
    using System;

    public class CommitResult
    {
        public bool Success { get; }

        public Exception Exception { get; }

        public CommitResult(bool success)
        {
            Success = success;
        }

        public CommitResult(Exception exception) : this(false)
        {
            Exception = exception;
        }
    }
}

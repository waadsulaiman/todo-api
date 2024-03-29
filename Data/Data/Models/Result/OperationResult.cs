﻿namespace Todo.Api.Data.Data.Models.Result
{
    /// <summary>
    /// Represents a model which can be used when executing different form
    /// of operations. It contains detailes about how the operation went.
    /// </summary>
    /// <typeparam name="T">Represents the data returned from the operation.</typeparam>
    public class OperationResult<T>
    {
        /// <summary>
        /// Represents whether an operation was a success or not.
        /// </summary>
        public bool Succeeded { get; private set; }

        /// <summary>
        /// The data returned from the operation.
        /// </summary>
        public T Result { get; private set; } = default!;

        /// <summary>
        /// Used for providing detailes about an operation. Incase of failure.
        /// </summary>
        public OperationFailure Fault { get; private set; } = default!;

        /// <summary>
        /// Represents that the operation was a success.
        /// </summary>
        /// <param name="result">The data returned from the operation.</param>
        /// <returns>
        /// An <see cref="OperationResult{T}"/> object.
        /// </returns>
        public static OperationResult<T> Success(T result)
        {
            return new OperationResult<T>()
            {
                Succeeded = true,
                Result = result
            };
        }

        /// <summary>
        /// Represents that the operation was a failure.
        /// </summary>
        /// <param name="errorCode">Represents an error code.</param>
        /// <param name="message">Represents an error message.</param>
        /// <returns>
        /// An <see cref="OperationResult{T}"/> object.
        /// </returns>
        public static OperationResult<T> Failure(short errorCode, string message)
        {
            return new OperationResult<T>()
            {
                Succeeded = false,
                Fault = new OperationFailure()
                {
                    ErrorCode = errorCode,
                    ErrorMessage = message,
                }
            };
        }
    }
}
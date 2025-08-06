namespace GestorBiblioteca.Application.ViewModels
{
 
    public class ResultViewModel<T>
    {
        private ResultViewModel(bool isSuccess, string? message, T? data)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }

     
        public bool IsSuccess { get; }

        /// <summary>
        /// Optional message describing the result.  This will typically contain an error message when <see cref="IsSuccess"/> is false.
        /// </summary>
        public string? Message { get; }

        /// <summary>
        /// Payload returned from the operation.  This will be null when <see cref="IsSuccess"/> is false.
        /// </summary>
        public T? Data { get; }

        public static ResultViewModel<T> Success(T data) => new(true, null, data);

        public static ResultViewModel<T> Error(string message) => new(false, message, default);
    }
}
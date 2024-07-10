namespace PostsAPI.Models
{
    public class ApiError
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }

        public ApiError(int statusCode, string message, string detail = null)
        {
            StatusCode = statusCode;
            Message = message;
            Detail = detail;
        }
    }
}

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using PostsAPI.Models;

namespace PostsAPI.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ApiExceptionFilter> _logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var statusCode = context.Exception is MyCustomException ? 400 : 500;
            var error = new ApiError(statusCode, "Произошла ошибка.", context.Exception.Message);

            _logger.LogError(context.Exception, "Ошибка API");

            context.Result = new ObjectResult(error)
            {
                StatusCode = statusCode
            };
            context.ExceptionHandled = true;
        }
    }
}

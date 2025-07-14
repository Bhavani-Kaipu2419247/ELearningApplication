using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace ELearningApplication.API.Infrastructure
{

    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly ILog _logger;

        public CustomExceptionFilter()
        {
            _logger = LogManager.GetLogger(typeof(CustomExceptionFilter));
        }

        public void OnException(ExceptionContext context)
        {
            string exceptionMessage = context.Exception.Message;

            _logger.Error("An unhandled exception occurred.", context.Exception);

            if(!context.HttpContext.Response.Headers.ContainsKey("X-Error-Message"))
{
                context.HttpContext.Response.Headers.Add("X-Error-Message", context.Exception.Message);
            }

            context.Result = new JsonResult(new
            {
                message = exceptionMessage, // Return full or partial message
                exceptionType = context.Exception.GetType().Name,
                timestamp = DateTime.UtcNow
            })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }

    }

}

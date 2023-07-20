using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace E2.Filters
{
    public class ExceptionFilter : IActionFilter , IOrderedFilter
    {
        public int Order { get;} =int.MaxValue-100;
        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if(context.Exception != null)
            {
                var exception=context.Exception;
                var  exceptionType = exception.GetType();
                var exceptionDetails= exception.ToString();
                HttpStatusCode status = HttpStatusCode.InternalServerError;
                var message = exceptionDetails.ToString();
                if(exceptionType==typeof(UnauthorizedAccessException)) {
                    message = "YOU ARE NOT AUTHORIZED!";
                    status = HttpStatusCode.Unauthorized;
                }
                else if(exceptionType==typeof(HttpRequestException))
                {
                    message = "Internal Service Error";
                    status = HttpStatusCode.InternalServerError;
                }
                var statuscode = Convert.ToInt32(status);
                var responseData = new
                {
                    ErrorCode = statuscode,
                    ErrorMessage = message
                };
                context.Result = new ObjectResult(responseData)
                {
                    StatusCode = statuscode
                };
                context.ExceptionHandled= true;

               // return View();
            }
        }

    }
}

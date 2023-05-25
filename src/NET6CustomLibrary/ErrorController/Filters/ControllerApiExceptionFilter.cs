using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NET6CustomLibrary.CustomResponse;

namespace NET6CustomLibrary.ErrorController.Filters;

public class ControllerApiExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception.GetType() != typeof(ExceptionResponse))
            return;

        var ex = (ExceptionResponse)context.Exception;

        ErrorResponse errore = new()
        {
            Error = new Error
            {
                Code = ex.ErrorCode,
                ErrorDetail = ex.ErrorDetail,
                Message = ex.ErrorMessage,
                Details = ex.ResponseBody
            }
        };
        ObjectResult result = new(errore)
        {
            StatusCode = (int)ex.StatusCode
        };

        context.Result = result;
        context.ExceptionHandled = true;
    }
}
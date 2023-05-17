namespace NET6CustomLibrary.Errors.Interfaces;

public interface IErrorResult
{
    //Errore 400 - Bad Request

    //Errore 401 - Unauthorized

    //Errore 404 - Not Found
    StatusCodeResult ResultNotFound(object message, HttpContext httpContext);

    //Errore 406 - Not Acceptable

    //Errore 409 - Conflict

    //Errore 422 - Unprocessable Entity
    StatusCodeResult ResultUnprocessableEntity(object errors, HttpContext httpContext);
    //StatusCodeResult ResultUnprocessableEntity(List<string> listErrors, HttpContext httpContext);

    //Errore 500 - Internal Server Error
}
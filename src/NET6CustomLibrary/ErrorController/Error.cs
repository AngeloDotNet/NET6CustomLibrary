namespace NET6CustomLibrary.ErrorController;

public class Error
{
    public int TypeCode { get; set; }
    public string Code { get; set; }
    public string ErrorDetail { get; set; }
    public string Message { get; set; }
    public object Details { get; set; }
}
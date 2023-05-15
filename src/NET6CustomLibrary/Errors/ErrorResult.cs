namespace NET6CustomLibrary.Errors;

public class ErrorResult
{
    public string TitleCode { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public int TypeCode { get; set; } = 0;
    public string InstancePath { get; set; }
    public List<string> Message { get; set; }
}
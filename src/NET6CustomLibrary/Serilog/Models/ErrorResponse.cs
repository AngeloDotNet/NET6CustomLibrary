namespace NET6CustomLibrary.Serilog.Models;

public class ErrorResponse
{
    public string TitleCode { get; set; }
    public int StatusCode { get; set; }
    public int TypeCode { get; set; } = 0;
    public string InstancePath { get; set; }
    public List<string> Message { get; set; }

    public ErrorResponse(int statusCode, string titleCode, int typeCode, string instancePath, List<string> message)
    {
        StatusCode = statusCode;
        TitleCode = titleCode;
        TypeCode = typeCode;
        InstancePath = instancePath;
        Message = message;
    }
}
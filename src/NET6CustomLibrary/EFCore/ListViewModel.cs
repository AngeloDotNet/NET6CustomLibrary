namespace NET6CustomLibrary.EFCore;

public class ListViewModel<T>
{
    public List<T> Results { get; set; }
    public int TotalCount { get; set; }
}
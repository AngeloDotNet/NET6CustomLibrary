namespace NET6CustomLibrary.EFCoreTransaction.Core.Interfaces;

public interface ITEntity<TKey>
{
    TKey Id { get; set; }
}
namespace NET6CustomLibrary.RabbitMQ.Abstractions;

public interface IMessagingBuilder
{
    IServiceCollection Services { get; }
}
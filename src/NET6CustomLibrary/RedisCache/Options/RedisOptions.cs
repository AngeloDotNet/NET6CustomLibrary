namespace NET6CustomLibrary.MailKit.Options;

public class RedisOptions
{
    public string Hostname { get; set; }
    public string InstanceName { get; set; }
    public TimeSpan AbsoluteExpireTime { get; set; }
    public TimeSpan SlidingExpireTime { get; set; }
}
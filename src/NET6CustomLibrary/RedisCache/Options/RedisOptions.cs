namespace NET6CustomLibrary.MailKit.Options;

public class RedisOptions
{
    public string InstanceName { get; set; }
    public int AbsoluteExpireTime { get; set; }
    public int SlidingExpireTime { get; set; }
}
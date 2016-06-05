
namespace CommonProvider.Example.Lib.ZeroConfigProviders
{
    public interface ISmsZeroConfigProvider : IZeroConfigProvider
    {
        string SendSms(Message message);
    }
}

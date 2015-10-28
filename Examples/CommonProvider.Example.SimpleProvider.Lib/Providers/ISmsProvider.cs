
namespace CommonProvider.Example.SimpleProvider.Lib.Providers
{
    public interface ISmsProvider : ISimpleProvider
    {
        string SendSms(Message message);
    }
}

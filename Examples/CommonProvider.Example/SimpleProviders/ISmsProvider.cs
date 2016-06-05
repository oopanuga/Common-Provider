
namespace CommonProvider.Example.SimpleProviders
{
    public interface ISmsProvider : ISimpleProvider
    {
        string SendSms(Message message);
    }
}

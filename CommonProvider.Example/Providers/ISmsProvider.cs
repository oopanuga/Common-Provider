
namespace CommonProvider.Example.Providers
{
    public interface ISmsProvider : IProvider
    {
        string SendSms(Message message);
    }
}

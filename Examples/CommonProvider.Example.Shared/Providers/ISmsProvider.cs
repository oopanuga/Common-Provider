
namespace CommonProvider.Example.Shared.Providers
{
    public interface ISmsProvider : IProvider
    {
        string SendSms(Message message);
    }
}


namespace CommonProvider.Example.Providers
{
    public interface ISmsProvider : IProvider
    {
        void SendSms(Message message);
    }
}

using CommonProvider.Data;

namespace CommonProvider.Example.Lib.Providers
{
    public abstract class SmsProviderBase : IProvider
    {
        public string Name { get; set; }

        public IProviderSettings Settings { get; set; }

        public string Group { get; set; }

        public abstract string SendSms(Message message);
    }
}

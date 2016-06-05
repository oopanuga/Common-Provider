using System;
using CommonProvider.Example.Lib;
using CommonProvider.Example.Lib.ZeroConfigProviders;
using CommonProvider.Example.Lib.Providers;

namespace CommonProvider.Example
{
    class Program
    {
        static void Main()
        {
            //send sms using config based providers
            SendSmsUsingConfigBasedProviders();

            //send sms using zero config providers
            SendSmsUsingZeroConfigProviders();


            Console.ReadLine();
        }

        private static void SendSmsUsingConfigBasedProviders()
        {
            Console.WriteLine("##############Sending sms using config based provider##############\n");

            var providerManager = new ProviderManager();
            var smsProviders = providerManager.Providers.All<SmsProviderBase>();

            var message = providerManager.Settings.Get<Message>("Message");

            foreach (SmsProviderBase smsProvider in smsProviders)
            {
                var result = smsProvider.SendSms(message);
                Console.WriteLine(result);
                Console.WriteLine();
            }
        }

        private static void SendSmsUsingZeroConfigProviders()
        {
            Console.WriteLine("##############Sending sms using zero config provider##############\n");

            var providerManager = new ZeroConfigProviderManager(Environment.CurrentDirectory);    
            var smsProviders = providerManager.Providers.All<ISmsZeroConfigProvider>();

            var message = new Message()
            {
                Sender = "007",
                Text = "Hello World",
                PhoneNumbers = "1010101010,2020202020,3030303030"
            };

            foreach (ISmsZeroConfigProvider smsProvider in smsProviders)
            {
                var result = smsProvider.SendSms(message);
                Console.WriteLine(result);
                Console.WriteLine();
            }
        }
    }
}

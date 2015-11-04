using CommonProvider.Example.SimpleProvider.Lib;
using CommonProvider.Example.SimpleProvider.Lib.Providers;
using System;

namespace CommonProvider.Example.SimpleProvider
{
    class Program
    {
        static void Main()
        {
            SendSms();
        }

        private static void SendSms()
        {
            var providerManager = SimpleProviderManagerFactory.Create();
            var smsProviders = providerManager.Providers.All<ISmsProvider>();

            var message = new Message()
            {
                Sender = "007",
                Text = "Hello World",
                PhoneNumbers = "1010101010,2020202020,3030303030"
            };

            foreach (ISmsProvider smsProvider in smsProviders)
            {
                var result = smsProvider.SendSms(message);
                Console.WriteLine(result);
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}

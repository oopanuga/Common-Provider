using System;
using CommonProvider.Example.Lib;
using CommonProvider.Example.Lib.Providers;

namespace CommonProvider.Example
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

            //sender:007|text:Hello World!!!|phoneNumbers:1010101010,2020202020,3030303030
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

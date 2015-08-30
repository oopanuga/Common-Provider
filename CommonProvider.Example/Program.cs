using CommonProvider.Example.Providers;
using Microsoft.Practices.Unity;
using System;

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
            var providerManager = Bootstrapper.UnityContainer.Resolve<IProviderManager>();
            var smsProviders = providerManager.Providers.All<ISmsProvider>();

            var message = providerManager.Settings.Get<Message>("Message");

            foreach (ISmsProvider smsProvider in smsProviders)
            {
                smsProvider.SendSms(message);

                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}

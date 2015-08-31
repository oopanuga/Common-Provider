using CommonProvider.Example.Shared;
using CommonProvider.Example.Shared.Providers;
using Microsoft.Practices.Unity;
using System;

namespace CommonProvider.Unity.Example
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
                var result = smsProvider.SendSms(message);
                Console.WriteLine(result);
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}

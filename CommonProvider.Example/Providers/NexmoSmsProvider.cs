using CommonProvider.Data;
using System;
using System.Text;

namespace CommonProvider.Example.Providers
{
    public class NexmoSmsProvider : ISmsProvider
    {
        public void SendSms(Message message)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Message sent using NEXMO Provider. See details below");
            stringBuilder.AppendLine(string.Format("Text: {0}", message.Text));
            stringBuilder.AppendLine(string.Format("From: {0}", message.Sender));
            stringBuilder.AppendLine(string.Format("To: {0}", message.PhoneNumbers));
            stringBuilder.AppendLine(string.Format("Endpoint: {0}", this.Settings.Get<string>("endpoint")));
            stringBuilder.AppendLine(string.Format("ApiKey: {0}", this.Settings.Get<string>("apikey")));

            Console.WriteLine(stringBuilder.ToString());
        }

        public string Name { get; set; }

        public ISettings Settings { get; set; }

        public string Group { get; set; }
    }
}

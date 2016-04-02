# CommonProvider

CommonProvider is a simple library built to give you an easy and consistent way of loading and accessing your providers (Provider model/pattern) or strategies (Strategy pattern). These really similar patterns promote loose coupling and enables extensibility in your applications.

### Key Features
Some key features of CommonProvider are,

1. Simple and consistent way of accessing providers.
2. Different provider load options,
    1. Load providers and settings based on definitions in a config file.
    2. Load providers from a file directory.
3. Dependency resolution via IOC containers e.g. Unity, Castle etc.
4. Support for Provider settings with the config provider load option. Settings could be simple or complex (serialized objects).
5. Uses data parsers to parse/deserialize complex settings.
6. Various extension points.

### Key Extension Points
CommonProvider has various extension points but the key ones are,

1. Write custom provider loaders.
2. Write custom dependency resolvers.
3. Write custom data parsers for dealing with complex settings.

### Using CommonProvider
Checkout detailed examples on its usage [here](https://github.com/oopanuga/common-provider/tree/master/Examples).

Implement either the IProvider (requires configuration) or ISimpleProvider (does't require configuration) interface
```c#
public abstract class SmsProviderBase : IProvider
{
    public string Name { get; set; }
    public ISettings Settings { get; set; }
    public string Group { get; set; }
    public abstract string SendSms(Message message);
}

public class NexmoSmsProvider : SmsProviderBase
{
    public override string SendSms(Message message)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("Message sent using NEXMO Provider. See details below");
        stringBuilder.AppendLine(string.Format("Text: {0}", message.Text));
        stringBuilder.AppendLine(string.Format("From: {0}", message.Sender));
        stringBuilder.AppendLine(string.Format("To: {0}", message.PhoneNumbers));
        stringBuilder.AppendLine(string.Format("Endpoint: {0}", this.Settings.Get<string>("Endpoint")));
        stringBuilder.AppendLine(string.Format("ApiKey: {0}", this.Settings.Get<string>("ApiKey")));
        return stringBuilder.ToString();
    }
}
```

Config setup for CommonProvider (not required for ISimpleProvider implementation)
```xml
<configSections>
    <section name="commonProvider" type="CommonProvider.Configuration.ProviderConfigSection, CommonProvider"/>
</configSections>

<commonProvider>
    <!--Define your types here-->
    <types>
      <add name="NexmoProvider" type="CommonProvider.Example.Providers.NexmoSmsProvider, CommonProvider.Example"/>
      <add name="TwilioProvider" type="CommonProvider.Example.Providers.TwilioSmsProvider,CommonProvider.Example"/>
      <add name="PipedDataParser" type="CommonProvider.Data.Parsers.PipedDataParser, CommonProvider"/>
    </types>
    <!--Define provider global settings here-->
    <settings dataParserType="PipedDataParser">
      <add key="Message" value="sender:007|text:Hello World!!!|phoneNumbers:1010101010,2020202020,3030303030"/>
    </settings>
    <!--Define providers here-->
    <providers>
      <provider name="Nexmo" enabled="true" type="NexmoProvider" group="">
        <settings>
          <add key="Endpoint" value="http://www.nexmo.com/sendsms"/>
          <add key="ApiKey" value="FakeApiKey"/>
        </settings>
      </provider>
      <provider name="Twilio" enabled="true" type="TwilioProvider" group="">
        <settings>
          <add key="Endpoint" value="http://www.twilio.com/sendsms"/>
          <add key="ApiKey" value="FakeApiKey"/>
        </settings>
      </provider>
    </providers>
</commonProvider>
```

Create Provider Manager
```c#
var providerManager = new ProviderManager(new ConfigProviderLoader(new ProviderConfigurationManager()));
```

Get Provider
```c#
var nexmoProvider = providerManager.Providers.ByName<SmsProviderBase>("Nexmo");
```

Get Provider setting
```c#
var apiKey = nexmoProvider.Settings.Get<string>("ApiKey");
```

Get global setting
```c#
var message = providerManager.Settings.Get<Message>("Message");
```

### Installing CommonProvider [nuget](https://www.nuget.org/packages/CommonProvider/)

```
PM> Install-Package CommonProvider
```

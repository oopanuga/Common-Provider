# CommonProvider

CommonProvider is a simple library that gives you an easy and consistent way of loading and accessing your providers/strategies. The provider/strategy pattern promotes loose coupling and enables extensibility in your applications.

### Key Features
Some key features of CommonProvider are,

1. Simple and consistent way of accessing providers.
2. Get provider meta data/settings from different config sources. Xml config source currently supported, can write your own custom config source.
3. Support for zero configuration providers. Load these from assemblies in a specified directory.
4. Dependency resolution via IOC containers e.g. Unity, Castle etc.
5. Support for simple and complex (serialized) provider settings. 
6. Use data parsers to parse/deserialize complex settings.
7. Various extension points.

### Key Extension Points
CommonProvider has various extension points but the key ones are,

1. Write custom provider configuration sources.
2. Write custom dependency resolvers.
3. Write custom data parsers for dealing with complex settings.

### Installing CommonProvider - [nuget](https://www.nuget.org/packages/CommonProvider/)

```
PM> Install-Package CommonProvider
```

### Release Notes
Release notes can be found [here](https://github.com/oopanuga/common-provider/blob/master/RELEASE-NOTES.txt)

### Using CommonProvider

Implement either the IProvider (requires configuration) or IZeroConfigProvider (doesn't require configuration) interface
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
    <section name="commonProvider" type="CommonProvider.ConfigSources.Xml.Configuration.ProviderConfigSection, CommonProvider"/>
</configSections>

<commonProvider>
    <!--Define your types here-->
    <types>
      <add name="NexmoProvider" type="CommonProvider.Example.Lib.Providers.NexmoSmsProvider, CommonProvider.Example.Lib"/>
      <add name="TwilioProvider" type="CommonProvider.Example.Lib.Providers.TwilioSmsProvider, CommonProvider.Example.Lib"/>
      <add name="PipeDelimitedDataParser" type="CommonProvider.Data.PipeDelimitedDataParser, CommonProvider"/>
    </types>
    <!--Define provider global settings here-->
    <settings dataParserType="PipeDelimitedDataParser">
      <add key="Message" value="sender:007|text:Hello World!!!|phoneNumbers:1010101010,2020202020,3030303030"/>
    </settings>
    <!--Define providers here-->
    <providers>
      <provider name="Nexmo" enabled="true" type="NexmoProvider" group="">
        <settings>
          <add key="Endpoint" value="http://www.nexmo.test/sendsms"/>
          <add key="ApiKey" value="FakeApiKey"/>
        </settings>
      </provider>
      <provider name="Twilio" enabled="true" type="TwilioProvider" group="">
        <settings>
          <add key="Endpoint" value="http://www.twilio.test/sendsms"/>
          <add key="ApiKey" value="FakeApiKey"/>
        </settings>
      </provider>
    </providers>
</commonProvider>
```

Create Provider Manager
```c#
var providerManager = new ProviderManager(new XmlProviderConfigSource());
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

More detailed examples [here](https://github.com/oopanuga/common-provider/tree/master/Examples)

### License

CommonProvider is released under the MIT license.

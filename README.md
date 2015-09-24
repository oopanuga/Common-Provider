# CommonProvider

CommonProvider is a simple library built to give you an easy and consistent way of accessing your providers (from the Provder model/pattern) or strategies (from the Strategy pattern). These really similar patterns promote loose coupling and enable extensibility in your applications.

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
Examples on how to use this can be found in the solution [here](https://github.com/commonprovider/common-provider/tree/master/CommonProvider.Example)

### Installing CommonProvider [Nuget Package](https://www.nuget.org/packages/CommonProvider/)

```
PM> Install-Package CommonProvider
```

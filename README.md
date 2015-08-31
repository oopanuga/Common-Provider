# CommonProvider

CommonProvider is a simple library built to give you an easy and consistent way of accessing your providers (from the Provder model/pattern) or strategies (from the Strategy pattern). These patterns (with really similar features) promote loosely coupling and enable extensibility in your applications.

### Key Features
Some key features of CommonProvider are,

1. Different provider load options,
    1. Load providers and settings based on definitions in a config file.
    2. Load providers from a file directory.
2. Dependency resolution via IOC containers e.g. Unity, Castle etc.
3. Support for Provider settings with the config provider load option. Settings could be simple or complex (serialized objects).
4. Various extension points.

### Key Extension Points
CommonProvider has various extension points but the key ones are,

1. Create custom provider loaders
2. Create custom dependency resolvers
3. Create custom data parsers for dealing with complex settings

### Using CommonProvider
Examples on how to use this can be found in the solution [here](https://github.com/commonprovider/common-provider/tree/master/Examples)

### Installing CommonProvider Nuget Packages

[CommonProvider Nuget](https://www.nuget.org/packages/CommonProvider/)
```
PM> Install-Package CommonProvider
```

[CommonProvider.Unity Nuget](https://www.nuget.org/packages/CommonProvider.Unity/)
```
PM> Install-Package CommonProvider.Unity
```

[CommonProvider.Autofac Nuget](https://www.nuget.org/packages/CommonProvider.Autofac/)
```
PM> Install-Package CommonProvider.Autofac
```

[CommonProvider.Castle Nuget](https://www.nuget.org/packages/CommonProvider.Castle/)
```
PM> Install-Package CommonProvider.Castle
```

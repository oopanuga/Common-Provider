# CommonProvider

CommonProvider is a simple library built to give you an easy and consistent way of accessing your providers (multiple implementations of a common interface AKA Provider Model or Strategy Pattern). Providers enable extensibility in
your applications.

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

### Installing CommonProvider
You should install CommonProvider with NuGet:

```
  Install-Package CommonProvider
```

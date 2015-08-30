# CommonProvider

CommonProvider is a simple library built to give you an easy and consistent way to access your providers (multiple implementations of a common interface AKA Provider Model or Strategy Pattern). Providers enables extensibility in
your applications.

### Key Features
Out of the box CommonProvider supports the following,

1. Provider load options,
    1. Load providers and settings defined in a configuration file.
    2. Load providers from a predefined directory.
2. Integration to various IOC containers for dependency resolution e.g. Unity, Castle etc.
3. Simple and complex (serialized objects) settings. Uses data parsers to load complex settings into complex types.


### Key Extension Points
CommonProvider has several extension points but the key ones are,

1. Custom provider loaders for loading providers
2. Integration to other IOC containers
3. Custom data parsers for complex settings

### Installing CommonProvider
You should install MediatR with NuGet:

```
  Install-Package CommonProvider
```

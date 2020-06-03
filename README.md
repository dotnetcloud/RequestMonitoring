# Request Monitoring

This project is a work-in-progress repository which provides an opinionated approach to record standard metrics.

## Packages

TODO

### Quick Start

**WARNING**
These packages are considered alpha quality. They are not thoroughly tested, and the public API is likely to change during development and based on feedback. I encourage you to try the packages to provide your thoughts and requirements, but perhaps be wary of using this in production!

Currently, this library supports recording metrics to DataDog via their DogStatsd library.

Add the following to your `ConfigureServices` method in `Startup`.

```csharp
services.AddDefaultRequestMetrics().WithDataDogRecorder();
```

The above code registers the default tag providers and a metric recorder which sends metrics to DogStatsd. It registers an `IStartupFilter` to add the required middleware to the beginning of the application pipeline.

# Support

If this library has helped you, feel free to [buy me a coffee](https://www.buymeacoffee.com/stevejgordon) or see the "Sponsor" link [at the top of the GitHub page](https://https://github.com/dotnetcloud/RequestMonitoring).

## Feedback

I welcome ideas for features and improvements to be raised as issues which I will respond to as soon as I can. This is a hobby project, so it may not be immediate!
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

In some cases, you may not want to record metrics for certain endpoints.

Add the `` attribute to any controllers/actions you wish to exclude from metric recording.

```csharp
[HttpGet]
[ExcludeFromRequestMetrics]
public IActionResult Get()
{
    return Ok();
}
```

You can also exclude endpoints when mapping them.

```csharp
app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/healthcheck").WithMetadata(new ExcludeFromRequestMetricsAttribute());
    endpoints.MapControllers();
});
```

The above code excludes health checks from appearing in the recorded metrics.

## Metrics

In its current form, the DataDog recorder will send two metrics to DataDog.

Each metric will by default be tagged with six tags which can be used to analyse the data:

* Request Route - The Endpoint route pattern. If endpoint routing is not used, this will use the request path. This may introduce high cardinality.
* Request Method - The HTTP method of the request.
* User Agent - The user agent of the request. Suitable for internal APIs where the number of consumers is known and low volume.
* Status Code - The status code of the response.
* Status Code Range - The high level range of status codes that the response status code belongs to. For example, the 200, 201 and 204 status codes all belong the the range '2xx'.
* Response Result - Either success (status codes less than 400) or failure (status codes 400 and greater).

The default tags for a request may appear as follows:

* request_route:WeatherForecast
* request_method:GET
* user_agent:PostmanRuntime/7.25.0
* status_code:200
* status_code_range:2xx
* request_result:success

# Future

This library handles a specific scenario for internal API monitoring. I plan to add configuration and builder methods which can be used to control the tags that are applied and the behaviour of the metric recording.

# Support

If this library has helped you, feel free to [buy me a coffee](https://www.buymeacoffee.com/stevejgordon) or see the "Sponsor" link [at the top of the GitHub page](https://https://github.com/dotnetcloud/RequestMonitoring).

## Feedback

I welcome ideas for features and improvements to be raised as issues which I will respond to as soon as I can. This is a hobby project, so it may not be immediate!
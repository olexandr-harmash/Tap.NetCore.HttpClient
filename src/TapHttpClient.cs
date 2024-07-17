using Polly;
using Polly.Retry;

namespace Tap.NetCore.HttpClient;

/// <summary>
/// Class TapHttpClient for configure HTTP GET and POST operations.
/// </summary>
public class TapHttpClient : System.Net.Http.HttpClient
{
    /// <summary>
    /// Pipeline for resilience and HTTP request management using Polly.
    /// </summary>
    public ResiliencePipeline HttpPipeline { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TapHttpClient"/> class with default settings.
    /// </summary>
    public TapHttpClient() : base()
    {
        /* Setup Polly policies, headers, and other base operations for simplified API usage */

        // Set the base address for API requests
        // TODO: initialize via configuration
        BaseAddress =  new Uri("https://domain.com");

        // Configure resilience policies using Polly
        // See: https://github.com/App-vNext/Polly
        HttpPipeline = new ResiliencePipelineBuilder()
            // Add default retry policy for handling transient faults
            .AddRetry(new RetryStrategyOptions())
            .Build();
    }
}
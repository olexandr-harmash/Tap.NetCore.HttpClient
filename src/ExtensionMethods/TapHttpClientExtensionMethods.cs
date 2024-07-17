using System.Web;

namespace Tap.NetCore.HttpClient.ExtensionMethods;

/// <summary>
/// Extension methods for TapHttpClient to simplify HTTP GET and POST operations.
/// </summary>
public static class TapHttpClientExtensionMethods
{
    /// <summary>
    /// Performs an asynchronous HTTP GET request.
    /// </summary>
    /// <param name="httpClient">The TapHttpClient instance.</param>
    /// <param name="endpoint">The API endpoint to send the request to.</param>
    /// <param name="args">Optional dictionary of query parameters.</param>
    /// <param name="options">HTTP completion options.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the HTTP response.</returns>
    public static async Task<HttpResponseMessage> GetAsync(
        this TapHttpClient httpClient,
        string endpoint,
        IDictionary<string, string> args = null,
        HttpCompletionOption options = default,
        CancellationToken cancellationToken = default)
    {
        if (args is null) 
        { 
            args = new Dictionary<string, string>(); 
        }

        //TODO: refactor ExecuteAsync method
        return await httpClient.HttpPipeline.ExecuteAsync(
            async (token) =>
            {
                return await httpClient.GetAsync(BuildEndpoint(endpoint, args), options, token);
            },
            cancellationToken
        );
    }

    /// <summary>
    /// Performs an asynchronous HTTP POST request.
    /// </summary>
    /// <param name="httpClient">The TapHttpClient instance.</param>
    /// <param name="endpoint">The API endpoint to send the request to.</param>
    /// <param name="content">HTTP content to send in the request body.</param>
    /// <param name="args">Optional dictionary of query parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the HTTP response.</returns>
    public static async Task<HttpResponseMessage> PostAsync(
        this TapHttpClient httpClient,
        string endpoint,
        HttpContent content,
        IDictionary<string, string> args = null,
        CancellationToken cancellationToken = default)
    {
        if (args is null) 
        { 
            args = new Dictionary<string, string>(); 
        }

        //TODO: refactor ExecuteAsync method
        return await httpClient.HttpPipeline.ExecuteAsync(
            async (token) =>
            {
                return await httpClient.PostAsync(BuildEndpoint(endpoint, args), content, token);
            },
            cancellationToken
        );
    }

    /// <summary>
    /// Builds the full API endpoint URL with query parameters.
    /// </summary>
    /// <param name="endpoint">The API endpoint path.</param>
    /// <param name="args">Dictionary of query parameters.</param>
    /// <returns>The constructed endpoint URL including query parameters.</returns>
    private static string BuildEndpoint(
        string endpoint,
        IDictionary<string, string> args)
    {
        string path = endpoint;

        if (args.Count != 0)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query.Add(args.ToNameValueCollection());
            path = string.Join("?", endpoint, query.ToString());
        }

        return path;
    }
}
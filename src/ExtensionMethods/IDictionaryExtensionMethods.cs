using System.Collections.Specialized;

namespace Tap.NetCore.HttpClient.ExtensionMethods;

/// <summary>
/// Extension methods for IDictionary&lt;string, string&gt; to convert to NameValueCollection.
/// </summary>
public static class IDictionaryExtensionMethods
{
    /// <summary>
    /// Converts the IDictionary&lt;string, string&gt; to a NameValueCollection.
    /// </summary>
    /// <param name="dictionary">The IDictionary to convert.</param>
    /// <returns>NameValueCollection containing the key-value pairs from the IDictionary.</returns>
    public static NameValueCollection ToNameValueCollection(this IDictionary<string, string> dictionary)
    {
        var collection = new NameValueCollection();

        foreach (var pair in dictionary)
        {
            collection.Add(pair.Key, pair.Value);
        }

        return collection;
    }
}
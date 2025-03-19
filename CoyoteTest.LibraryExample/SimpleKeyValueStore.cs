namespace CoyoteTest.LibraryExample;

using System.Collections.Concurrent;

public class SimpleKeyValueStore
{
    ConcurrentDictionary<string, string> dictionary = new();

    public bool Set(string key, string value)
    {
        return dictionary.TryAdd(key, value);        
    }

    public string Get(string key)
    {
        if (dictionary.TryGetValue(key, out string? value))
            return value;
        return string.Empty;
    }
}

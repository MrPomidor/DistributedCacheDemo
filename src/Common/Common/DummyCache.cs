using System.Diagnostics.CodeAnalysis;

namespace Common;

public class DummyCache : ICache
{
    public Task<T> GetOrCreateAsync<T>([NotNull] string key, Func<string, Task<T>> itemFactory)
    {
        return itemFactory(key);
    }

    public Task ClearAsync([NotNull] string key)
    {
        return Task.CompletedTask;
    }
}

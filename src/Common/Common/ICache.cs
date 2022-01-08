using System.Diagnostics.CodeAnalysis;

namespace Common;

public interface ICache
{
    Task<T> GetOrCreateAsync<T>([NotNull] string key, [NotNull] Func<string, Task<T>> itemFactory);
    Task ClearAsync([NotNull] string key);
}

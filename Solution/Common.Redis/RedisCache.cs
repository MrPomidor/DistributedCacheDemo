using System.Diagnostics.CodeAnalysis;
using RedLockNet;
using StackExchange.Redis;

namespace Common.Redis;

public class RedisCache : ICache
{
    private static readonly TimeSpan LockExpiryTime = TimeSpan.FromSeconds(5);
    private static readonly TimeSpan LockWaitTime = TimeSpan.FromSeconds(10);
    private static readonly TimeSpan LockRetryTime = TimeSpan.FromSeconds(1);

    private readonly ISerializer _serializer;
    private readonly IConnectionMultiplexer _redis;
    private readonly IDistributedLockFactory _redisLock;
    public RedisCache(
        ISerializer serializer,
        IConnectionMultiplexer redis,
        IDistributedLockFactory redisLock)
    {
        _serializer = serializer;
        _redis = redis;
        _redisLock = redisLock;
    }

    public async Task<T> GetOrCreateAsync<T>([NotNull] string key, [NotNull] Func<string, Task<T>> itemFactory)
    {
        var database = _redis.GetDatabase();

        var redisValue = await database.StringGetAsync(key);
        if (!redisValue.IsNullOrEmpty)
            return _serializer.Deserialize<T>(redisValue);

        await using (var redLock = await _redisLock.CreateLockAsync(
            resource: GetLockKey(key),
            expiryTime: LockExpiryTime,
            waitTime: LockWaitTime,
            retryTime: LockRetryTime))
        {
            if (!redLock.IsAcquired)
                throw new Exception("Error acquiring lock for key: " + key);

            // check if value was set in another lock
            redisValue = await database.StringGetAsync(key);
            if (!redisValue.IsNullOrEmpty)
                return _serializer.Deserialize<T>(redisValue);

            var value = await itemFactory(key);
            var valueSerialized = _serializer.Serialize<T>(value);

            var keySet = await database.StringSetAsync(key, valueSerialized, expiry: TimeSpan.FromMilliseconds(Consts.CacheAbsoluteExpirationMilliseconds));
            if (!keySet)
                throw new Exception("Error setting cache value for key: " + key);

            return value;
        }
    }

    public async Task ClearAsync([NotNull] string key)
    {
        var database = _redis.GetDatabase();
        await database.KeyDeleteAsync(key);
    }

    private string GetLockKey(string key) => "redlock-" + key;
}

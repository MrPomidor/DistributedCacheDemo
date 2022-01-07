namespace HybridCacheApi.HybridCache;

public class CacheExpiredMessage
{
    public Guid OriginatorInstance { get; set; }
    public string CacheKey { get; set; }
}

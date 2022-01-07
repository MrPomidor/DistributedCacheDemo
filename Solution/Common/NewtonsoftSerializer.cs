using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Common;

public class NewtonsoftSerializer : ISerializer
{
    private readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings
    {
        DefaultValueHandling = DefaultValueHandling.Ignore,
        NullValueHandling = NullValueHandling.Ignore
    };

    public T Deserialize<T>([NotNull] string serialized)
    {
        return JsonConvert.DeserializeObject<T>(serialized, serializerSettings)!;
    }

    public string Serialize<T>([NotNull] T item)
    {
        return JsonConvert.SerializeObject(item, serializerSettings);
    }
}

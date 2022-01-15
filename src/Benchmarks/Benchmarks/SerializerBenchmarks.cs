using BenchmarkDotNet.Attributes;
using Common;
using Microsoft.Extensions.DependencyInjection;

namespace Benchmarks
{
    public class SerializerBenchmarks
    {
        private ServiceProvider? _serviceProvider;
        private ISerializer? _serializer;

        private DataItem? _dataItem;
        private string? _dataItemSerialized;
        private LargeDataItem? _largeDataItem;
        private string? _largeDataItemSerialized;

        [GlobalSetup]
        public async Task Setup()
        {
            _serviceProvider = new ServiceCollection().AddCommon().BuildServiceProvider();
            var factory = _serviceProvider.GetRequiredService<IDataFactory>();
            _serializer = _serviceProvider.GetRequiredService<ISerializer>();

            _dataItem = await factory.GetDataItemAsync();
            _dataItemSerialized = _serializer.Serialize(_dataItem);
            _largeDataItem = await factory.GetLargeDataItemAsync();
            _largeDataItemSerialized = _serializer.Serialize(_largeDataItem);
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            _dataItem = null;
            _largeDataItem = null;
            _serializer = null;
            _serviceProvider?.Dispose();
            _serviceProvider = null;
        }

        [Benchmark]
        public void Serialize()
        {
            _ = _serializer!.Serialize(_dataItem);
        }

        [Benchmark]
        public void Deserialize()
        {
            _ = _serializer!.Deserialize<DataItem>(_dataItemSerialized!);
        }

        [Benchmark]
        public void SerializeLarge()
        {
            _ = _serializer!.Serialize(_largeDataItem);
        }

        [Benchmark]
        public void DeserializeLarge()
        {
            _ = _serializer!.Deserialize<LargeDataItem>(_largeDataItemSerialized!);
        }
    }
}

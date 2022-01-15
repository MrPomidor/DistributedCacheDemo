using BenchmarkDotNet.Attributes;
using Common;
using Microsoft.Extensions.DependencyInjection;

namespace Benchmarks
{
    public class DataFactoryBenchmarks
    {
        private ServiceProvider? _serviceProvider;
        private IDataFactory? _factory;

        [GlobalSetup]
        public void Setup()
        {
            _serviceProvider = new ServiceCollection().AddCommon().BuildServiceProvider();
            _factory = _serviceProvider.GetRequiredService<IDataFactory>();
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            _factory = null;
            _serviceProvider?.Dispose();
            _serviceProvider = null;
        }

        [Benchmark]
        public async Task CreateDataItem()
        {
            _ = await _factory!.GetDataItemAsync();
        }

        [Benchmark]
        public async Task CreateLargeDataItem()
        {
            _ = await _factory!.GetLargeDataItemAsync();
        }
    }
}

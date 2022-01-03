namespace Common;

public class DataFactory : IDataFactory
{
    public async Task<DataItem> GetDataItemAsync()
    {
        // emulate time for creation of resource
        await Task.Delay(Consts.FactoryDelayMilliseconds);

        return new DataItem { DateNow = DateTime.UtcNow };
    }
}

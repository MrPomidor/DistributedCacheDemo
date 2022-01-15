namespace Common;

public interface IDataFactory
{
    Task<DataItem> GetDataItemAsync();

    Task<LargeDataItem> GetLargeDataItemAsync();
}

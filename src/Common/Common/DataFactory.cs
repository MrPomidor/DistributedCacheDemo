namespace Common;

public class DataFactory : IDataFactory
{
    public async Task<DataItem> GetDataItemAsync()
    {
        // emulate time for creation of resource
        await Task.Delay(Consts.FactoryDelayMilliseconds);

        return new DataItem { DateNow = DateTime.UtcNow };
    }

    public async Task<LargeDataItem> GetLargeDataItemAsync()
    {
        // emulate time for creation of resource
        await Task.Delay(Consts.FactoryDelayMilliseconds);

        LargeDataItem largeDataItem = new();
        for (int i = 0; i < 100; i++)
        {
            var key_1 = i.ToString();
            var value_1 = largeDataItem.Configuration[key_1] = new();

            for (int j = 0; j < 10; j++)
            {
                var key_2 = j.ToString();
                var value_2 = value_1[key_2] = new();

                for (int k = 0; k < 5; k++)
                {
                    var key_3 = k.ToString();
                    var value_3 = Guid.NewGuid().ToString();

                    value_2[key_3] = value_3;
                }
            }
        }

        largeDataItem.DateNow = DateTime.UtcNow;

        return largeDataItem;
    }
}

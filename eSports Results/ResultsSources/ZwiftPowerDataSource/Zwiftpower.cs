using Common.Models;

namespace ZwiftPowerDataSource
{
    public class Zwiftpower : Common.Interfaces.IDataSource
    {
        public Task<RawEventResults> GetRawResultsFromEventAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
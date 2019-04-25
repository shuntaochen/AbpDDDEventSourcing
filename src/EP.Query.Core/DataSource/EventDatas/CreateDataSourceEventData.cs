using Abp.Dependency;
using Abp.Events.Bus;

namespace EP.Query.DataSource
{
    public class CreateDataSourceEventData : EventData
    {

        public CreateDataSourceEventData(DataSource dataSource)
        {
            DataSource = dataSource;
        }

        public DataSource DataSource { get; }
    }
}
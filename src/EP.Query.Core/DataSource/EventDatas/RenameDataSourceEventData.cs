using Abp.Events.Bus;

namespace EP.Query.DataSource
{
    public class RenameDataSourceEventData : EventData
    {

        public RenameDataSourceEventData(DataSource dataSource, string oldName)
        {
            DataSource = dataSource;
            OldName = oldName;
        }

        public DataSource DataSource { get; }
        public string OldName { get; }
    }
}
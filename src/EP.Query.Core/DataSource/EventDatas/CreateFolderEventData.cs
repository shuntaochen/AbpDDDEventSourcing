using Abp.Events.Bus;

namespace EP.Query.DataSource
{
    public class CreateFolderEventData : EventData
    {
        public CreateFolderEventData(DataSourceFolder dataSourceFolder)
        {
            DataSourceFolder = dataSourceFolder;
        }

        public DataSourceFolder DataSourceFolder { get; }
    }
}

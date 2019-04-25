using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Events.Bus.Handlers;
using Abp.Runtime.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Query.DataSource.EventHandlers
{
    public class DatasourceEventHandler : DomainService, IAsyncEventHandler<CreateFolderEventData>, IAsyncEventHandler<CreateDataSourceEventData>, ITransientDependency
    {


        private readonly IRepository<DataSourceFolder> _dataSourceFolderRepository;
        private readonly IRepository<DataSource> _dataSourceRepository;

        public DatasourceEventHandler(IRepository<DataSourceFolder> dataSourceFolderRepository, IRepository<DataSource> dataSourceRepository) : base()
        {
            this._dataSourceFolderRepository = dataSourceFolderRepository;
            this._dataSourceRepository = dataSourceRepository;
        }



        public async Task HandleEventAsync(CreateFolderEventData eventData)
        {
            await Task.FromResult(true);
        }

        public async Task HandleEventAsync(CreateDataSourceEventData eventData)
        {
            var model = eventData.DataSource;
            var folder = await _dataSourceFolderRepository.GetAsync(model.DataSourceFolderId);
            var folders = _dataSourceFolderRepository.GetAllList();
            var count = CountFolderDataSources(folders, folder.Id);
            folder.DataSourceCount = count.Item1;
            var parent = folders.FirstOrDefault(f => f.Id == folder.ParentId);
            while (parent != null)
            {
                folder.Level += 1;
                parent = folders.FirstOrDefault(p => p.Id == parent.ParentId);
            }
            await _dataSourceFolderRepository.UpdateAsync(folder);
        }


        private (int, int) CountFolderDataSources(List<DataSourceFolder> folders, int folderId)
        {
            var level = 0;
            var totaldsCount = 0;
            totaldsCount += _dataSourceRepository.Count(ds => ds.DataSourceFolderId == folderId);
            if (!folders.Any(f => f.ParentId == folderId)) return (totaldsCount, level);
            level += 1;
            folders.Where(fs => fs.ParentId == folderId).ToList().ForEach(folder =>
            {
                totaldsCount += CountFolderDataSources(folders, folder.Id).Item1;
            });
            return (totaldsCount, level);
        }
    }
}

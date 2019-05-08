using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Events.Bus;
using Abp.Extensions;
using EP.Query.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Query.DataSource
{
    public class DataSourceAppService : QueryAppServiceBase, IDataSourceAppService
    {
        private readonly IRepository<DataSourceFolder> _dataSourceFolderRepository;
        private readonly IRepository<DataSource> _dataSourceRepository;
        private readonly IRepository<DataSourceField> _dataSourceFieldRepository;
        private readonly DbSchemaFactory _mysqlSchemaFactory;


        public DataSourceAppService(IRepository<DataSourceFolder> dataSourceFolderRepository, IRepository<DataSource> dataSourceRepository, IRepository<DataSourceField> dataSourceFieldRepository, DbSchemaFactory mysqlSchemaFactory) : base()
        {
            _dataSourceFolderRepository = dataSourceFolderRepository;
            _dataSourceRepository = dataSourceRepository;
            _dataSourceFieldRepository = dataSourceFieldRepository;
            _mysqlSchemaFactory = mysqlSchemaFactory;
        }

        public async Task<CreateFolderOutput> CreateFolder(CreateFolderInput input)
        {
            var model = new DataSourceFolder(input.Name, input.ParentId);
            return new CreateFolderOutput { Id = await _dataSourceFolderRepository.InsertAndGetIdAsync(model) };
        }



        public async Task<GetALlOutput> GetALl(GetALlInput input)
        {
            var dss = _dataSourceRepository.GetAll().Where(ds => ds.DataSourceFolderId == input.FolderId);
            var folders = _dataSourceFolderRepository.GetAll().Where(f => f.ParentId == input.FolderId);
            var totalDs = await dss.CountAsync();
            var totalFolders = await folders.CountAsync();
            var ret = new GetALlOutput() { TotalCount = (totalDs + totalFolders) };
            ret.Folders = folders.Skip(input.SkipCount).Take(input.MaxResultCount).Select(ds => new DataSourceFolderDto { Id = ds.Id, Name = ds.Name, DataSourceCount = ds.DataSourceCount, CreationTime = ds.CreationTime, CreatorUserId = ds.CreatorUserId, LastModificationTime = ds.LastModificationTime, LastModifierUserId = ds.LastModifierUserId }).ToList();
            ret.Items = dss.Skip(input.SkipCount - (folders.Count() - ret.Folders.Count())).Take(input.MaxResultCount - ret.Folders.Count()).Select(ds => new DataSourceDto { Id = ds.Id, Name = ds.Name, DataSourceFolderId = ds.DataSourceFolderId, LastModifierUserId = ds.LastModifierUserId, CreationTime = ds.CreationTime, CreatorUserId = ds.CreatorUserId }).ToList();

            return ret;
        }


        public async Task<RenameOutput> Rename(RenameInput input)
        {
            var model = await _dataSourceRepository.GetAsync(input.Id);
            model.Rename(input.Name);
            return new RenameOutput { Id = await _dataSourceRepository.InsertOrUpdateAndGetIdAsync(model) };
        }

        public virtual async Task<SaveOutput> Save(SaveInput input)
        {
            input.DataSource.DataSourceFields.ForEach(f =>
            {
                if (f.DisplayText.IsNullOrEmpty()) f.DisplayText = f.Name;
            });
            var model = ObjectMapper.Map<DataSource>(input.DataSource);
            var id = await _dataSourceRepository.InsertOrUpdateAndGetIdAsync(model);
            _dataSourceFieldRepository.Delete(df => df.DataSourceId == id);
            model.DataSourceFields.ForEach(dfo => _dataSourceFieldRepository.InsertOrUpdate(dfo.MapTo<DataSourceField>()));
            return new SaveOutput { Id = id };

        }

        [UnitOfWork]
        public virtual async Task Delete(DeleteInput input)
        {
            await _dataSourceRepository.DeleteAsync(del => del.Id == input.Id);
        }


        public async Task<GetSchemasOutput> GetSchemas()
        {
            JArray ret = new JArray();
            using (var mysql = _mysqlSchemaFactory.Create())
            {
                var tables = mysql.GetTableNames();
                var tableWithColumns = tables.Select(name => new { name, fields = mysql.GetTableColumnDefinitions(name) }).ToList();
                ret = JArray.FromObject(tableWithColumns);
            }


            return new GetSchemasOutput { FieldInfos = ret };
        }

        public async Task<DataSourceDto> Get(int id)
        {
            var model = _dataSourceRepository.GetAllIncluding(ds => ds.DataSourceFields).First(d => d.Id == id);

            var ret = model.MapTo<DataSourceDto>();

            return ret;

        }


        public async Task<GetQueryDataOutput> GetQueryData(GetQueryDataInput input)
        {
            var builder = new QueryBuilder();
            builder.AddTableName(input.TableName);
            builder.AddAndConditions(input.AndConditions);
            var sql = builder.Build;
            int totalCount = 0;
            var ret = new List<JObject>();
            //sql = "select * from datasouces";
            using (var mysql = _mysqlSchemaFactory.Create())
            {
                Dictionary<string, string> cols = new Dictionary<string, string>();
                ret = mysql.Query(sql, out cols, out totalCount, (input.SkipCount / input.MaxResultCount + 1), input.MaxResultCount);
            }
            return new GetQueryDataOutput { Items = ret.As<IReadOnlyList<JObject>>(), TotalCount = totalCount };
        }


        public async Task<Dictionary<string, string>> GetQueryColumns(GetQueryColumnsInput input)
        {
            var builder = new QueryBuilder();
            builder.AddTableName(input.TableName);
            builder.AddAndConditions(input.AndConditions);
            var (total, totalCount) = builder.Build;

            Dictionary<string, string> cols = new Dictionary<string, string>();
            //sql = "select * from datasouces";
            using (var mysql = _mysqlSchemaFactory.Create())
            {
                var data = mysql.Query((total, totalCount), out cols, out var count, 1, 1);
            }
            return cols;
        }

    }
}

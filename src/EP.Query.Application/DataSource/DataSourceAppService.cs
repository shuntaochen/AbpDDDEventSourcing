using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Events.Bus;
using Abp.Extensions;
using Abp.UI;
using EP.Query.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EP.Commons.ServiceApi.DynamicForms.Dto;
using EP.Commons.ServiceApi.DynamicForms;
using EP.Commons.ServiceApi;
using Abp.Runtime.Caching;

namespace EP.Query.DataSource
{
    public class DataSourceAppService : QueryAppServiceBase, IDataSourceAppService
    {
        public const string DB_SCHEMA_CACHENAME = "#EP.Query.DataSource.DataSourceAppService.DB_SCHEMA_CACHENAME#";
        private readonly IRepository<DataSourceFolder> _dataSourceFolderRepository;
        private readonly IRepository<DataSource> _dataSourceRepository;
        private readonly IRepository<DataSourceField> _dataSourceFieldRepository;
        private readonly DbSchemaFactory _mysqlSchemaFactory;
        private readonly ICacheManager cacheManager;
        private readonly IDynamicFormsServiceApi dynamicFormsServiceApi;

        public DataSourceAppService(IRepository<DataSourceFolder> dataSourceFolderRepository, IRepository<DataSource> dataSourceRepository, IRepository<DataSourceField> dataSourceFieldRepository, DbSchemaFactory mysqlSchemaFactory, IServiceApiFactory serviceApiFactory, ICacheManager cacheManager) : base()
        {
            _dataSourceFolderRepository = dataSourceFolderRepository;
            _dataSourceRepository = dataSourceRepository;
            _dataSourceFieldRepository = dataSourceFieldRepository;
            _mysqlSchemaFactory = mysqlSchemaFactory;
            this.cacheManager = cacheManager;
            dynamicFormsServiceApi = serviceApiFactory.GetServiceApi<IDynamicFormsServiceApi>().Object;
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
            ret.Folders = folders.Skip(input.SkipCount).Take(input.MaxResultCount).Select(ds => new DataSourceFolderDto
            {
                Id = ds.Id,
                Name = ds.Name,
                DataSourceCount = ds.DataSourceCount,
                CreationTime = ds.CreationTime,
                CreatorUserId = ds.CreatorUserId,
                LastModificationTime = ds.LastModificationTime,
                LastModifierUserId = ds.LastModifierUserId
            }).ToList();
            var num1 = input.SkipCount - (folders.Count() - ret.Folders.Count());
            ret.Items = dss.Skip(num1 > 0 ? num1 : 0).Take(input.MaxResultCount - ret.Folders.Count()).Select(ds => new DataSourceDto
            {
                Id = ds.Id,
                Name = ds.Name,
                DataSourceFolderId = ds.DataSourceFolderId,
                LastModifierUserId = ds.LastModifierUserId,
                CreationTime = ds.CreationTime,
                CreatorUserId = ds.CreatorUserId
            }).ToList();

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
            //if (_dataSourceRepository.GetAll().Any(ds => ds.Name == input.Name)) throw new UserFriendlyException(L("DataSourceNameAlreadyExist"));
            var dto = new DataSourceDto
            {
                DataSourceFolderId = input.DataSourceFolderId,
                Name = input.Name,
                Remark = input.Remark,
                SourceContent = input.SourceContent
            };
            dto.DataSourceFields = input.DataSourceFields.Select(f =>
              new DataSourceFieldDto
              {
                  Type = f.Type,
                  DataSourceId = dto.Id,
                  Filter = f.Filter,
                  Name = f.Name,
                  DisplayText = !string.IsNullOrEmpty(f.DisplayText) ? f.DisplayText : f.Name,

              }).ToList();
            var model = ObjectMapper.Map<DataSource>(dto);
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
            var cached = cacheManager.GetCache(DB_SCHEMA_CACHENAME).Get(DB_SCHEMA_CACHENAME, () =>
            {
                JArray ret = new JArray();
                using (var mysql = _mysqlSchemaFactory.Create())
                {
                    var tables = mysql.GetTableNames();
                    var tableWithColumns = tables.Select(name => new { name, fields = mysql.GetTableColumnDefinitions(name) }).ToList();
                    ret = JArray.FromObject(tableWithColumns);
                }

                return new GetSchemasOutput { FieldInfos = ret };
            });
            return cached;
        }

        public async Task<DataSourceDto> Get(int id)
        {
            var model = _dataSourceRepository.GetAllIncluding(ds => ds.DataSourceFields).First(d => d.Id == id);

            var ret = model.MapTo<DataSourceDto>(); return ret;


        }



        public async Task<GetQueryDataOutput> GetQueryData(int id, bool queryAll = true, int skipCount = 0, int maxResultCount = int.MaxValue)
        {
            var ret = new GetQueryDataOutput();
            var ds = await _dataSourceRepository.GetAsync(id);
            var paths = _dataSourceFieldRepository.GetAll().Where(df => df.DataSourceId == id && !string.IsNullOrEmpty(df.Filter)).AsNoTracking().Select(f => f.Filter).ToList();

            var input1 = new GetFormDataInput()
            {
                SkipCount = skipCount,
                MaxResultCount = maxResultCount,
                Paths = paths,
                RetPaths = new List<string>(),
                QueryAll = queryAll

            };

            switch (ds.Type)
            {
                case DataSourceType.Form:
                    var obj = await dynamicFormsServiceApi.GetAllFormData(input1);
                    ret.TotalCount = int.Parse(obj["totalCount"]?.ToString());
                    ret.Items = obj["items"]?.ToObject<IReadOnlyList<JObject>>();
                    break;
                case DataSourceType.TableOrView:
                case DataSourceType.Sql:
                    var input = new GetQueryDataInput
                    {
                        SkipCount = skipCount,
                        MaxResultCount = maxResultCount,
                        TableName = ds.SourceContent,
                        AndConditions = paths
                    };
                    ret = await GetPreviewData(input);
                    break;
                default:
                    break;
            }
            return ret;
        }
        public async Task<GetQueryDataOutput> GetPreviewData(GetQueryDataInput input)
        {
            var builder = new QueryBuilder();
            builder.AddTableName(input.TableName);
            builder.AddAndConditions(input.AndConditions);
            var sql = builder.Build;
            int totalCount = 0;
            var ret = new List<JObject>();
            using (var mysql = _mysqlSchemaFactory.Create())
            {
                Dictionary<string, string> cols = new Dictionary<string, string>();
                ret = mysql.Query(sql, out cols, out totalCount, (input.SkipCount / input.MaxResultCount + 1), input.MaxResultCount);
            }

            return new GetQueryDataOutput { Items = ret.As<IReadOnlyList<JObject>>(), TotalCount = totalCount };
        }


        public async Task<Dictionary<string, string>> GetQueryColumns(GetQueryColumnsInput input)
        {

            Dictionary<string, string> cols = new Dictionary<string, string>();
            using (var mysql = _mysqlSchemaFactory.Create())
            {
                var data = mysql.Query((input.Sql, ""), out cols, out var count, 1, 1);
            }
            return cols;
        }

        public async Task DeleteFolder(int id)
        {
            if (_dataSourceRepository.GetAll().Any(ds => ds.DataSourceFolderId == id) || _dataSourceFolderRepository.GetAll().Any(df => df.ParentId == id)) throw new UserFriendlyException(L("DataSourceFolderHasContent"));
            await _dataSourceFolderRepository.DeleteAsync(id);
        }
    }
}

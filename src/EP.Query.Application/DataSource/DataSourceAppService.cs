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
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<CreateFolderOutput> CreateFolder(CreateFolderInput input)
        {
            var model = new DataSourceFolder(input.Name, input.ParentId);
            return new CreateFolderOutput { Id = await _dataSourceFolderRepository.InsertAndGetIdAsync(model) };
        }


        /// <summary>
        /// 分页获取指定文件夹下数据源
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetALlOutput> GetALl(GetALlInput input)
        {
            var all = _dataSourceRepository.GetAll();
            var ret = await all.Where(ds => ds.DataSourceFolderId == input.FolderId).Skip(input.SkipCount).Take(input.MaxResultCount).ToListAsync();
            return new GetALlOutput() { Items = ret, TotalCount = await all.CountAsync() };
        }

        /// <summary>
        /// 重命名数据源
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<RenameOutput> Rename(RenameInput input)
        {
            var model = await _dataSourceRepository.GetAsync(input.Id);
            model.Rename(input.Name);
            return new RenameOutput { Id = await _dataSourceRepository.InsertOrUpdateAndGetIdAsync(model) };
        }
        /// <summary>
        /// 添加或修改数据源
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SaveOutput> Save(SaveInput input)
        {
            var model = ObjectMapper.Map<DataSource>(input.DataSource);
            var id = await _dataSourceRepository.InsertOrUpdateAndGetIdAsync(model);
            _dataSourceFieldRepository.Delete(df => df.DataSourceId == id);
            input.DataSourceFields.ForEach(dfo => _dataSourceFieldRepository.Insert(dfo.MapTo<DataSourceField>()));
            return new SaveOutput { Id = id };

        }
        /// <summary>
        /// 删除数据源
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task Delete(DeleteInput input)
        {
            await _dataSourceRepository.DeleteAsync(del => del.Id == input.Id);
        }

        /// <summary>
        /// 获取数据库表架构和字段
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 获取数据源和字段
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<JObject> Get(int id)
        {
            var model = _dataSourceRepository.GetAllIncluding(ds => ds.DataSourceFields).First(d => d.Id == id);

            var x = model.MapTo<DataSourceDto>();

            var ret = JObject.FromObject(model);
            return ret;

        }

        /// <summary>
        /// 获取数据源查询数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JArray> GetQueryData(GetQueryDataInput input)
        {
            var builder = new QueryBuilder();
            builder.AddTableName(input.TableName);
            builder.AddAndConditions(input.AndConditions);
            var sql = builder.Build();

            var ret = new JArray();
            //sql = "select * from datasouces";
            using (var mysql = _mysqlSchemaFactory.Create())
            {
                Dictionary<string, string> cols = new Dictionary<string, string>();
                ret = JArray.FromObject(mysql.Query(sql, out cols));
            }
            return ret;
        }

        /// <summary>
        /// 根据查询生成字段定义
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, string>> GetQueryColumns(GetQueryColumnsInput input)
        {
            var builder = new QueryBuilder();
            builder.AddTableName(input.TableName);
            builder.AddAndConditions(input.AndConditions);
            var sql = builder.Build();

            Dictionary<string, string> cols = new Dictionary<string, string>();
            //sql = "select * from datasouces";
            using (var mysql = _mysqlSchemaFactory.Create())
            {
                var data = mysql.Query("select top 1" + sql.ToLower().RemovePreFix("select"), out cols);
            }
            return cols;
        }

    }
}

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

            return new SaveOutput { Id = await _dataSourceRepository.InsertOrUpdateAndGetIdAsync(model) };

        }
        /// <summary>
        /// 删除数据源
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task Delete(DeleteInput input)
        {
            var model = ObjectMapper.Map<DataSource>(new DataSourceDto { Id = input.Id });
            await _dataSourceRepository.DeleteAsync(model);
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
                var tableWithColumns = tables.Select(name => new { name, fields = mysql.GetColumnDefinitions(name) }).ToList();
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
            var ret = JObject.FromObject(model);
            return ret;

        }

        /// <summary>
        /// 生成数据源对应的视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<JObject> CreateView(int id)
        {
            var ret = new JObject();
            var ds = await _dataSourceRepository.GetAsync(id);
            using (var mysql = _mysqlSchemaFactory.Create())
            {
                mysql.CreateView(ds.SourceContent);
                var cols = mysql.GetColumnDefinitions(ds.SourceContent);
                ret = JObject.FromObject(new { ds.SourceContent, cols });
            }
            return ret;
        }
        /// <summary>
        /// 获取数据源视图数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<JArray> GetViewData(int id)
        {
            var ret = new JArray();
            var ds = await _dataSourceRepository.GetAsync(id);
            using (var mysql = _mysqlSchemaFactory.Create())
            {
                ret = JArray.FromObject(mysql.QueryView(ds.SourceContent));
            }
            return ret;
        }


    }
}

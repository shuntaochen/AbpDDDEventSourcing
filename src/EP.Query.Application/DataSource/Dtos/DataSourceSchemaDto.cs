using System.Collections.Generic;

namespace EP.Query.DataSource
{
    /// <summary>
    /// 数据源架构数据传输对象
    /// </summary>
    public class DataSourceSchemaDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 字段信息列表
        /// </summary>
        public List<DataSourceSchemaItemDto> DataSourceSchemaItemDtos { get; set; }


    }
}

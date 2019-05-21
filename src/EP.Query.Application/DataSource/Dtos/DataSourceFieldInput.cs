using System.ComponentModel.DataAnnotations;

namespace EP.Query.DataSource
{
    /// <summary>
    /// 新建数据源字段时输入请求
    /// </summary>
    public class DataSourceFieldInput
    {

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayText { get; set; }
        /// <summary>
        /// 过滤条件
        /// </summary>
        [RegularExpression(@"^.+[><=!]+\w+$")]
        public string Filter { get; set; }
    }
}

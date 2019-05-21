namespace EP.Query.DataSource
{
    /// <summary>
    /// 数据源字段信息
    /// </summary>
    public class DataSourceSchemaItemDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 过滤条件
        /// </summary>
        public string Condition { get; set; }
    }
}

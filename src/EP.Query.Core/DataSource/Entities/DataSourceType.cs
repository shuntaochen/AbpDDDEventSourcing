namespace EP.Query.DataSource
{
    /// <summary>
    /// 数据源类型
    /// </summary>
    public enum DataSourceType : int
    {
        /// <summary>
        /// 表单
        /// </summary>
        Form = 0,
        /// <summary>
        /// 表或视图
        /// </summary>
        TableOrView = 1,
        /// <summary>
        /// sql
        /// </summary>
        Sql = 2
    }
}

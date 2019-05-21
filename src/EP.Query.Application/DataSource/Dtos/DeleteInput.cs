using System.ComponentModel.DataAnnotations;

namespace EP.Query.DataSource
{
    /// <summary>
    /// 删除数据源输入请求
    /// </summary>
    public class DeleteInput
    {
        /// <summary>
        /// 要删除的数据源Id
        /// </summary>
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
    }
}

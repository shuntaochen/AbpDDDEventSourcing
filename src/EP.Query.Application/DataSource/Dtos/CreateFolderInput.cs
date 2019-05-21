using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace EP.Query.DataSource
{
    /// <summary>
    /// 创建文件夹输入
    /// </summary>
    public class CreateFolderInput
    {
        /// <summary>
        /// 文件夹名称
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 文件夹父Id
        /// </summary>
        public int? ParentId { get; set; }
    }
}

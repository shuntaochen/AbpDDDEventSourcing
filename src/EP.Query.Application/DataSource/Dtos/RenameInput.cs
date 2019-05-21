using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace EP.Query.DataSource
{
    /// <summary>
    /// 重命名文件夹请求
    /// </summary>
    public class RenameInput
    {
        /// <summary>
        /// 要命名的文件夹Id
        /// </summary>
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        /// <summary>
        /// 新名称
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}

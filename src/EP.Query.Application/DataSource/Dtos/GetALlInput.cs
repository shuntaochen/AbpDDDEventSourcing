using Abp.Application.Services.Dto;

namespace EP.Query.DataSource
{
    /// <summary>
    /// 获取文件夹下文件夹和数据源输入
    /// </summary>
    public class GetALlInput : PagedResultRequestDto
    {
        /// <summary>
        /// 文件夹Id
        /// </summary>
        public int FolderId { get; set; }
    }
}

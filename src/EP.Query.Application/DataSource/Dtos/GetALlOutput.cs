using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace EP.Query.DataSource
{
    /// <summary>
    /// 获取到文件夹下文件夹和数据源集合
    /// </summary>
    public class GetALlOutput : PagedResultDto<DataSourceDto>
    {
        /// <summary>
        /// 子文件夹
        /// </summary>
        public List<DataSourceFolderDto> Folders { get; set; } = new List<DataSourceFolderDto>();
    }
}

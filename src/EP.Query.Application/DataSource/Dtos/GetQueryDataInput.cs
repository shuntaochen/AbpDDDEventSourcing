using Abp.Application.Services.Dto;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EP.Query.DataSource
{
    /// <summary>
    /// 获取查询数据请求
    /// </summary>
    public class GetQueryDataInput : PagedResultRequestDto
    {
        /// <summary>
        /// 表名称
        /// </summary>
        [Required]
        public string TableName { get; set; }
        /// <summary>
        /// 查询条件
        /// </summary>
        public List<string> AndConditions { get; set; } = new List<string>();

    }
}

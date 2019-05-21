using Abp.Application.Services.Dto;
using Newtonsoft.Json.Linq;

namespace EP.Query.DataSource
{
    /// <summary>
    /// 查询数据返回结果输出
    /// </summary>
    public class GetQueryDataOutput : PagedResultDto<JObject>
    {

    }
}

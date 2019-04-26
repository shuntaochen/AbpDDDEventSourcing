using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EP.Query.DataSource
{
    public class DataSourceDto : EntityDto, IMayHaveTenant, ICreationAudited, IModificationAudited
    {
        /// <summary>
        /// 文件夹编号
        /// </summary>
        public int DataSourceFolderId { get; set; }

        //[JsonIgnore]
        public List<DataSourceFieldDto> DataSourceFields { get; set; } = new List<DataSourceFieldDto>();

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public DataSourceType Type { get; set; }
        /// <summary>
        /// 数据源总个数
        /// </summary>
        public string SourceContent { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 租户编号
        /// </summary>
        public int? TenantId { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public long? CreatorUserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 最后修改人
        /// </summary>
        public long? LastModifierUserId { get; set; }
        /// <summary>
        /// 上次修改时间
        /// </summary>
        public DateTime? LastModificationTime { get; set; }
    }
}

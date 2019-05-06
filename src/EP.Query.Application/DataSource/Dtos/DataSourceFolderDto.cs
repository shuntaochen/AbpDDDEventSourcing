using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EP.Query.DataSource
{
    public class DataSourceFolderDto : EntityDto, IMayHaveTenant, ICreationAudited, IModificationAudited
    {

        /// <summary>
        /// 父id
        /// </summary>
        public int? ParentId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 层级
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 数据源数
        /// </summary>
        public int DataSourceCount { get; set; }
        /// <summary>
        /// 租户编号
        /// </summary>
        public int? TenantId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public long? CreatorUserId { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public long? LastModifierUserId { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? LastModificationTime { get; set; }

        [JsonIgnore]
        public List<DataSourceDto> DataSources { get; set; } = new List<DataSourceDto>();
    }
}

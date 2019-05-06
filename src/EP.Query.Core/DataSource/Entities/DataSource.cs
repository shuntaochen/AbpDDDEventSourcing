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
    /// <summary>
    /// 数据源
    /// </summary>
    public class DataSource : AggregateRoot, IMayHaveTenant, ICreationAudited, IModificationAudited
    {
        private DataSource()
        {

        }
        /// <summary>
        /// 文件夹编号
        /// </summary>
        public int DataSourceFolderId { get; set; }
        public virtual DataSourceFolder DataSourceFolder { get; set; }

        public virtual List<DataSourceField> DataSourceFields { get; set; } = new List<DataSourceField>();

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public DataSourceType Type { get; set; }
        /// <summary>
        /// 查询内容
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


        public DataSource(string name, int folderId, DataSourceType dataSourceType, string sourceContent, string remark = null)
        {
            Name = name;
            DataSourceFolderId = folderId;
            Type = dataSourceType;
            SourceContent = sourceContent;
            Remark = remark;
            DomainEvents.Add(new CreateDataSourceEventData(this));
        }

        public void Rename(string newName)
        {
            var oldName = Name;
            Name = newName;
            DomainEvents.Add(new RenameDataSourceEventData(this, oldName));
        }

    }
}

using Abp.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EP.Query.DataSource
{
    /// <summary>
    /// 数据源字段
    /// </summary>
    public class DataSourceField : Entity, IMayHaveTenant
    {

        /// <summary>
        /// 数据源id
        /// </summary>
        public int DataSourceId { get; set; }

        [JsonIgnore]
        public DataSource DataSource { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayText { get; set; }
        /// <summary>
        /// 过滤条件
        /// </summary>
        public string Filter { get; set; }
        /// <summary>
        /// 租户id
        /// </summary>
        public int? TenantId { get; set; }
    }
}

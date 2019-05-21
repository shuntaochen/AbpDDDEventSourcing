using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EP.Query.DataSource
{
    /// <summary>
    /// 添加或修改数据源请求
    /// </summary>
    public class SaveInput
    {
        /// <summary>
        /// 文件夹编号
        /// </summary>
        [Range(1, int.MaxValue)]
        [Required]
        public int DataSourceFolderId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
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
        /// 修改时数据源Id
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 数据源下字段
        /// </summary>
        public List<DataSourceFieldInput> DataSourceFields { get; set; }


    }
}

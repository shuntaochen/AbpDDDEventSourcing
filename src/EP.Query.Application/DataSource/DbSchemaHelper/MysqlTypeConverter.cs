using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EP.Query.DataSource
{
    public static class MysqlTypeConverter
    {
        /// <summary>
        /// 统一转换，将后端数据类型转换为前端可理解类型，包括数据库类型和c#编程语言类型
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string ToSysPreDefined(this string src)
        {
            src = src.ToLower();
            var typeNumberFilters = new string[] { "int", "float", "decimal", "double", "byte" };
            var typeStringFilters = new string[] { "text", "char" };
            var typeDatetimeFilters = new string[] { "datetime" };
            var typeBooleanFilters = new string[] { "bool" };
            var ret = SysDataTypes.String;

            if (typeNumberFilters.Any(t => src.Contains(t)))
                ret = SysDataTypes.Number;
            if (typeStringFilters.Any(t => src.Contains(t)))
                ret = SysDataTypes.String;
            if (typeDatetimeFilters.Any(t => src.Contains(t)))
                ret = SysDataTypes.Datetime;
            if (typeBooleanFilters.Any(t => src.Contains(t)))
                ret = SysDataTypes.Boolean;

            return ret.ToString().ToLower();
        }

        public enum SysDataTypes
        {
            Number, String, Datetime, Boolean
        }
    }
}

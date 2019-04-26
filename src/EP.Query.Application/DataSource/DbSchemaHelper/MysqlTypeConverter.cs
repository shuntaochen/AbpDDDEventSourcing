using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EP.Query.DataSource
{
    public static class MysqlTypeConverter
    {
        public static string ToSysPreDefined(this string src)
        {
            var typeIntFilters = new string[] { "int" };
            var typeStringFilters = new string[] { "text", "char", "datetime" };
            var ret = SysDataTypes.String;

            if (typeIntFilters.Any(t => src.Contains(t)))
                ret = SysDataTypes.Int;

            return ret.ToString().ToLower();
        }

        public enum SysDataTypes
        {
            Int, String
        }
    }
}

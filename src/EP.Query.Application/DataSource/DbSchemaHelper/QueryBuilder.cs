using Abp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EP.Query.DataSource
{
    public class QueryBuilder
    {
        public QueryBuilder()
        {
        }


        private string _tableName;
        private List<string> _conditions;
        public void AddTableName(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentException("tableName is null or empty", nameof(tableName));
            }

            _tableName = tableName;
        }

        public (string total, string totalCount) Build => (Select("*"), Select("count(1)"));

        private string Select(string selected)
        {
            return $"select {selected} from {_tableName} where 1=1 { (_conditions.Count > 0 ? "and " : "") }{string.Join(" and ", _conditions.Select(c => c.Contains('=') ? $"{c.Split('=')[0]}='{c.Split('=')[1]}'" : c))}";
        }

        public void AddAndConditions(List<string> andConditions)
        {
            _conditions = andConditions ?? throw new ArgumentNullException(nameof(andConditions));
        }

    }
}